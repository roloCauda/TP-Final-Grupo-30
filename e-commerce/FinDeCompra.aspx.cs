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
            if (!IsPostBack)
            {
                dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

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
            }
        }
        private void MostrarPanel(string opcion)
        {
            pnl_Datos.Visible = (opcion == "Datos");
            pnl_Envio.Visible = (opcion == "Envio" || opcion == "DatosEnvio");
            pnlFormaDeEnvio.Visible = (opcion == "Envio" || opcion == "DatosEnvio");
            pnlFDatosDeEnvio.Visible = (opcion == "DatosEnvio");
            pnl_Pagos.Visible = (opcion == "Pagos");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Button btn_Opcion = (Button)sender;
            string opcion = btn_Opcion.CommandArgument;
            MostrarPanel(opcion);
        }

        protected void rbtnFormaDeEnvio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            string descripcion = radioButton.Text;
            Pedido pedido = (Pedido)Session["pedido"];

            int idFormaDeEnvio = int.Parse(radioButton.Attributes["value"]);

            if (descripcion == "Envio a cargo del vendedor")
            {
                MostrarPanel("DatosEnvio");
            }
            else
            {
                MostrarPanel("Envio");
            }

            pedido.formaDeEnvio.IdFormaDeEnvio = idFormaDeEnvio;
            Session["pedido"] = pedido;
        }

        protected void rbtnFormaDePago_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            Pedido pedido = (Pedido)Session["pedido"];

            int idFormaDeEnvio = int.Parse(radioButton.Attributes["value"]);

            pedido.formaDeEnvio.IdFormaDeEnvio = idFormaDeEnvio;
            Session["pedido"] = pedido;
        }
        protected void btnConfirmar(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];
            Usuario usuario = (Usuario)Session["usuario"];
            Pedido pedido = new Pedido();
            UsuarioNegocio negocioU = new UsuarioNegocio();
            DireccionNegocio negocioD = new DireccionNegocio();
            PedidoNegocio negocioP = new PedidoNegocio();
            ArticulosXPedidoNegocio negocioAP = new ArticulosXPedidoNegocio();

            if (usuario == null) //si no esta logueado
            {
                if(!string.IsNullOrEmpty(usuario.direccion.Calle))
                {
                    usuario.direccion.IdDireccion = negocioD.AgregarDireccion(usuario.direccion);
                }

                pedido.IdCliente = negocioU.AgregarUsuarioSinLoguear(usuario);
            }
            else //si esta logueado
            {
                pedido.IdCliente = usuario.IdUsuario;
            }
            
            pedido.IdPedido = negocioP.agregarPedido(pedido);

            negocioAP.agregarListaApedido(carrito.ListaItems, pedido.IdPedido);

            negocioAP.agregarPedido(pedido);
        }
    }
}