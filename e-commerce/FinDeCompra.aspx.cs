using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce.Pag_Cliente
{
    public partial class FinDeCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se verifica que haya un usuario en session
            if (Session["usuario"] == null)
                Response.Redirect("Default.aspx");

            if (!IsPostBack)
            {

                dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];
                Pedido pedido = (Pedido)Session["pedido"];

                TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

                if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
                {
                    Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
                }

                MostrarPanel("Datos");

                LocalidadNegocio negocioLocalidad = new LocalidadNegocio();
                List<Localidad> listaLocalidad = negocioLocalidad.listar();

                ProvinciaNegocio negocioProvincia = new ProvinciaNegocio();
                List<Provincia> listaProvincia = negocioProvincia.listar();

                ddlLocalidad.DataSource = listaLocalidad;
                ddlLocalidad.DataValueField = "Id";
                ddlLocalidad.DataTextField = "Descripcion";
                ddlLocalidad.DataBind();

                ddlProvincia.DataSource = listaProvincia;
                ddlProvincia.DataValueField = "Id";
                ddlProvincia.DataTextField = "Descripcion";
                ddlProvincia.DataBind();

                //si esta en MI CUENTA no es necesario
                ddlLocalidad.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione --", ""));

                FormaDeEnvioNegocio negocioE = new FormaDeEnvioNegocio();

                rptFormaDeEnvio.DataSource = negocioE.listar();
                rptFormaDeEnvio.DataBind();

                FormaDePagoNegocio negocioF = new FormaDePagoNegocio();

                rptFormaDePago.DataSource = negocioF.listar();
                rptFormaDePago.DataBind();

                lblPrecio.Text = "$" + carrito.total.ToString();

                repFinalizar.DataSource = carrito.ListaItems;
                repFinalizar.DataBind();

                // Seleccionam automáticamente el primer radiobutton de Envíos 
                if (rptFormaDeEnvio.Items.Count > 0)
                {
                    var firstItem = rptFormaDeEnvio.Items[0];
                    var rbtnFormaDeEnvio = (RadioButton)firstItem.FindControl("rbtnFormaDeEnvio");
                    rbtnFormaDeEnvio.Checked = true;
                    pedido.FormaDeEnvio.IdFormaDeEnvio = 1;
                    Session["pedido"] = pedido;
                }
                // Selecciona automáticamente el primer radiobutton de Pagos
                if (rptFormaDePago.Items.Count > 0)
                {
                    var firstItem = rptFormaDePago.Items[0];
                    var rbtnFormaDePago = (RadioButton)firstItem.FindControl("rbtnFormaDePago");
                    rbtnFormaDePago.Checked = true;
                    pedido.FormaDePago.IdFormaDePago = 1;
                    Session["pedido"] = pedido;
                }

                //Cargar datos personales
                Usuario user = (Usuario)Session["usuario"];

                txtNombre.Text = user.Nombres.ToString();
                txtApellido.Text = user.Apellidos.ToString();
                txtDNI.Text = user.DNI.ToString();
                txtEmail.Text = user.Email.ToString();
                txtTelefono.Text = user.Telefono.ToString();

                //Inhabilita los campos de datos personales
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDNI.Enabled = false;
                txtEmail.Enabled = false;
                txtTelefono.Enabled = false;

                //Carga Forma de Envio = Acordar con el vendedor
                txtCalle.Text = user.direccion.Calle;
                txtNumeracion.Text = user.direccion.Numero.ToString();
                txtPiso.Text = user.direccion.Piso.ToString();
                txtDepartamento.Text = user.direccion.Departamento;
                txtCP.Text = user.direccion.CodPostal;
                ddlLocalidad.SelectedValue = user.direccion.Localidad.Id.ToString();
                ddlProvincia.SelectedValue = user.direccion.Provincia.Id.ToString();
            }
        }
        private void MostrarPanel(string opcion)
        {
            pnl_Datos.Visible = (opcion == "Datos");
            pnl_Envio.Visible = (opcion == "Envio" || opcion == "DatosEnvio");
            pnlFormaDeEnvio.Visible = (opcion == "Envio" || opcion == "DatosEnvio");
            pnlDatosDeEnvio.Visible = (opcion == "DatosEnvio");
            pnl_Pagos.Visible = (opcion == "Pagos");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {

            Button btn_Opcion = (Button)sender;
            string opcion = btn_Opcion.CommandArgument;
            Pedido pedido = (Pedido)Session["pedido"];

            int IDEnvio = pedido.FormaDeEnvio.IdFormaDeEnvio;

            if (IDEnvio == 2 && opcion == "Envio")
            {
                MostrarPanel("DatosEnvio");
            }
            else
            {
                MostrarPanel(opcion);
            }

        }

        protected void rbtnFormaDeEnvio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            string descripcion = radioButton.Text;
            Pedido pedido = (Pedido)Session["pedido"];

            int idFormaDeEnvio = int.Parse(radioButton.Attributes["value"]);

            // Deseleccionar los demás RadioButtons
            foreach (RepeaterItem item in rptFormaDeEnvio.Items)
            {
                if (item.FindControl("rbtnFormaDeEnvio") is RadioButton rbtn)
                {
                    if (rbtn != radioButton)
                    {
                        rbtn.Checked = false;
                    }
                }
            }

            if (descripcion == "Envio a cargo del vendedor")
            {
                MostrarPanel("DatosEnvio");
            }
            else
            {
                MostrarPanel("Envio");
            }

            pedido.FormaDeEnvio.IdFormaDeEnvio = idFormaDeEnvio;
            pedido.FormaDeEnvio.Descripcion = descripcion;
            Session["pedido"] = pedido;
        }

        protected void rbtnFormaDePago_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            string descripcion = radioButton.Text;
            Pedido pedido = (Pedido)Session["pedido"];

            // Deseleccionar los demás RadioButtons
            foreach (RepeaterItem item in rptFormaDePago.Items)
            {
                if (item.FindControl("rbtnFormaDePago") is RadioButton rbtn)
                {
                    if (rbtn != radioButton)
                    {
                        rbtn.Checked = false;
                    }
                }
            }

            int idFormaDePago = int.Parse(radioButton.Attributes["value"]);

            pedido.FormaDePago.IdFormaDePago = idFormaDePago;
            pedido.FormaDePago.Descripcion = descripcion;
            Session["pedido"] = pedido;
        }
        protected void btnConfirmar(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];
            Usuario usuario = (Usuario)Session["usuario"];
            Pedido pedido = (Pedido)Session["pedido"];
            UsuarioNegocio negocioU = new UsuarioNegocio();
            DireccionNegocio negocioD = new DireccionNegocio();
            PedidoNegocio negocioP = new PedidoNegocio();
            ArticulosXPedidoNegocio negocioAP = new ArticulosXPedidoNegocio();
            StockNegocio negocioS = new StockNegocio();

            pedido.IdCliente = usuario.IdUsuario;
            pedido.Fecha = DateTime.Now;

            if (pedido.FormaDeEnvio.IdFormaDeEnvio == 2)
            {

                pedido.Direccion.Calle = txtCalle.Text;
                pedido.Direccion.Numero = int.Parse(txtNumeracion.Text);
                if (!string.IsNullOrEmpty(txtPiso.Text))
                    pedido.Direccion.Piso = int.Parse(txtPiso.Text);
                if (!string.IsNullOrEmpty(txtDepartamento.Text))
                    pedido.Direccion.Departamento = txtDepartamento.Text;
                pedido.Direccion.CodPostal = txtCP.Text;
                pedido.Direccion.Provincia.Id = int.Parse(ddlProvincia.SelectedValue);
                pedido.Direccion.Provincia.Descripcion = ddlProvincia.SelectedItem.ToString();
                pedido.Direccion.Localidad.Id = int.Parse(ddlLocalidad.SelectedValue);
                pedido.Direccion.Localidad.Descripcion = ddlLocalidad.SelectedItem.ToString();
                pedido.Direccion.IdDireccion = negocioD.AgregarDireccion(pedido.Direccion);
                pedido.FormaDeEnvio.Descripcion = "Envio a cargo del vendedor";
            }
            else
            {
                pedido.FormaDeEnvio.Descripcion = "Retiro";
            }

            if(pedido.FormaDePago.IdFormaDePago == 1)
            {
                pedido.FormaDePago.Descripcion = "Tarjeta de crédito";
            }

            pedido.EstadoPedido = "Pendiente";

            pedido.IdPedido = negocioP.agregarPedido(pedido);

            pedido.ListaArtXPedido = negocioAP.agregarListaApedido(carrito.ListaItems);

            negocioAP.cargarEnBDlistaArticulos(pedido);

            negocioS.descontarStock(pedido.ListaArtXPedido);

            Session["pedido"] = pedido;

            EmailService emailService = new EmailService();
            EmailService emailService2 = new EmailService();
            string cuerpoMailCompra = emailService.obtenerCuerpoMailConDatosDePedido(pedido, usuario);
            string cuerpoMailVenta = emailService2.obtenerCuerpoMailConDatosDeVenta(pedido, usuario);
            emailService.armarCorreo(usuario.Email, "Compra Realizada", cuerpoMailCompra);
            emailService2.armarCorreo(usuario.Email, "Nueva Venta", cuerpoMailVenta); // Debería llevar el mail del vendedor
            try
            {
                //emailService.enviarCorreo();
                //emailService2.enviarCorreo();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }

            Response.Redirect("CompraRealizada.aspx");
        }

    }
}