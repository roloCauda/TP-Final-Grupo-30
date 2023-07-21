using dominio;
using e_commerce.Pag_Cliente;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class Cuenta_Usuario : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public dominio.Carrito carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                carrito = (dominio.Carrito)Session["ListaItems"];

                TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

                if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
                {
                    Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
                }

                MostrarPanel("Perfil");

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

                /*  Actualiza las Label de la Master */
                Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                Label lblPrecio = Master.FindControl("lblPrecio") as Label;
                lblPrecio.Text = "$" + carrito.total.ToString();

                //Cargar datos en campos
                Usuario user = (Usuario)Session["usuario"];

                txtDNI.Text = user.DNI.ToString();
                txtDNI.Enabled = false;

                txtNombres.Text = user.Nombres.ToString();
                txtApellidos.Text = user.Apellidos.ToString();
                txtEmail.Text = user.Email.ToString();

                if (!string.IsNullOrEmpty(user.Telefono))
                    txtTelefono.Text = user.Telefono.ToString();

                txtCalle.Text = user.direccion.Calle.ToString();
                txtNumeracion.Text = user.direccion.Numero.ToString();

                if (!string.IsNullOrEmpty(user.direccion.Piso.ToString()))
                    txtPiso.Text = user.direccion.Piso.ToString();

                if (!string.IsNullOrEmpty(user.direccion.Departamento))
                    txtDepartamento.Text = user.direccion.Departamento.ToString();

                txtCP.Text = user.direccion.CodPostal.ToString();
                ddlLocalidad.SelectedValue = user.direccion.Localidad.Id.ToString();
                ddlProvincia.SelectedValue = user.direccion.Provincia.Id.ToString();

                PedidoNegocio negocioU = new PedidoNegocio();
                dgvPedidosCliente.DataSource = negocioU.listarPedidosPorCliente(user.IdUsuario);
                dgvPedidosCliente.DataBind();

                FavoritoNegocio negocioF = new FavoritoNegocio();
                dgvArticuloFavoritos.DataSource = negocioF.listarFavoritosPorCliente(user.IdUsuario);
                dgvArticuloFavoritos.DataBind();

                dgvPedidosCliente.PageIndexChanging += dgvPedidosCliente_PageIndexChanging;
            }

            lblUsuarioGuardadoConExito.Visible = false;
            lblDireccionGuardadoConExito.Visible = false;
            lblContraseñaGuardadaConExito.Visible = false;
            lblErrorContraseñaIncorrecta.Visible = false;
        }

        protected void lnk_Opcion_Click(object sender, EventArgs e)
        {
            LinkButton lnk_Opcion = (LinkButton)sender;
            string opcion = lnk_Opcion.CommandArgument;
            MostrarPanel(opcion);
        }

        private void MostrarPanel(string opcion)
        {
            pnl_Perfil.Visible = (opcion == "Perfil");
            pnl_Direccion.Visible = (opcion == "Direccion");
            pnl_Contraseña.Visible = (opcion == "Contraseña");
            pnl_Favoritos.Visible = (opcion == "Favoritos");
            pnl_Pedidos.Visible = (opcion == "Pedidos");
            pnl_ArtPorPedido.Visible = (opcion == "PedidosXArt");
        }

        protected void lnk_Salir_Click(object sender, EventArgs e)
        {
            Session.Remove("usuario");
            Response.Redirect("Default.aspx");
        }

        protected void btn_GuardarCambiosPerfil_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario user = (Usuario)Session["usuario"];

            try
            {
                user.Nombres = txtNombres.Text;
                user.Apellidos = txtApellidos.Text;
                user.Email = txtEmail.Text;
                user.Telefono = txtTelefono.Text != "" ? txtTelefono.Text : null;

                negocio.actualizarUsuario(user);
                MostrarMsj("UsuarioExito");
                Session["usuario"] = user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            DireccionNegocio negocio = new DireccionNegocio();
            Usuario user = (Usuario)Session["usuario"];

            try
            {
                user.direccion.Calle = txtCalle.Text;
                user.direccion.Numero = int.Parse(txtNumeracion.Text);
                user.direccion.Piso = txtPiso.Text != "" ? int.Parse(txtPiso.Text) : (int?)null; //Piso es int nuleable
                user.direccion.Departamento = txtDepartamento.Text != "" ? txtDepartamento.Text : null;
                user.direccion.CodPostal = txtCP.Text;
                user.direccion.Provincia.Id = int.Parse(ddlProvincia.SelectedValue);
                user.direccion.Localidad.Id = int.Parse(ddlLocalidad.SelectedValue);
                negocio.actualizarDireccion(user);
                MostrarMsj("DireccionExito");
                Session["usuario"] = user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btn_GuardarContraseña_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario user = (Usuario)Session["usuario"];

            try
            {
                if (txtPasswordActual.Text == user.Contraseña.ToString())
                {
                    user.Contraseña = txtPasswordNueva.Text;

                    negocio.actualizarContraseña(user);
                    MostrarMsj("ContraseñaExito");
                    Session["usuario"] = user;
                }
                else
                {
                    MostrarMsj("ErrorContraseña");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void MostrarMsj(string opcion)
        {
            lblUsuarioGuardadoConExito.Visible = (opcion == "UsuarioExito");
            lblDireccionGuardadoConExito.Visible = (opcion == "DireccionExito");
            lblContraseñaGuardadaConExito.Visible = (opcion == "ContraseñaExito");
            lblErrorContraseñaIncorrecta.Visible = (opcion == "ErrorContraseña");
        }

        protected void dgvPedidosCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerPedido")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int IdPedido = Convert.ToInt32(dgvPedidosCliente.DataKeys[rowIndex].Value);

                lblNroPedido.Text = "#" + IdPedido.ToString();

                ArticulosXPedidoNegocio negocioAP = new ArticulosXPedidoNegocio();
                dgvArtPorPedido.DataSource = negocioAP.listarConSP(IdPedido);
                dgvArtPorPedido.DataBind();

                PedidoNegocio negocioP = new PedidoNegocio();
                decimal total = negocioP.totalPedido(IdPedido);
                lblTotal.Text = "Total: $ " + total.ToString();
                MostrarPanel("PedidosXArt");
            }
        }

        protected void dgvPedidosCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPedidosCliente.PageIndex = e.NewPageIndex;
            Usuario user = (Usuario)Session["usuario"];
            PedidoNegocio negocioU = new PedidoNegocio();
            dgvPedidosCliente.DataSource = negocioU.listarPedidosPorCliente(user.IdUsuario);
            dgvPedidosCliente.DataBind();
        }

        protected void dgvArtPorPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvArtPorPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArtPorPedido.PageIndex = e.NewPageIndex;
            dgvArtPorPedido.DataBind();
        }

        protected void dgvArticuloFavoritos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticuloFavoritos.PageIndex = e.NewPageIndex;
            dgvArticuloFavoritos.DataBind();
        }

        protected void dgvArticuloFavoritos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver" || e.CommandName == "Quitar")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int IdArticulo = int.Parse(dgvArticuloFavoritos.DataKeys[row.RowIndex].Value.ToString());
                Usuario user = (Usuario)Session["usuario"];

                // Acciones según el comando seleccionado
                if (e.CommandName == "Ver")
                {
                    Response.Redirect("Detalle.aspx?id=" + IdArticulo);
                }
                else if (e.CommandName == "Quitar")
                {
                    FavoritoNegocio negocioF = new FavoritoNegocio();
                    negocioF.QuitarFavorito(user.IdUsuario, IdArticulo);
                    user.ListaFavoritos.RemoveAll(favorito => favorito.IdArticulo == IdArticulo);

                    dgvArticuloFavoritos.DataSource = negocioF.listarFavoritosPorCliente(user.IdUsuario);
                    dgvArticuloFavoritos.DataBind();
                }
            }
        }
    }
}