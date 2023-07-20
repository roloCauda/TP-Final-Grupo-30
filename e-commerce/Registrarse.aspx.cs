using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace e_commerce
{
    public partial class Registrarse : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public dominio.Carrito carrito { get; set; }
        Usuario user;
        UsuarioNegocio negocioU;
        Direccion direccion;
        DireccionNegocio negocioD;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            if (Session["usuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)

            {

                ArticuloNegocio negocio = new ArticuloNegocio();
                carrito = (dominio.Carrito)Session["ListaItems"];

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

                /*  Actualiza las Label de la Master */
                Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                Label lblPrecio = Master.FindControl("lblPrecio") as Label;
                lblPrecio.Text = "$" + carrito.total.ToString();

                txtTelefono.Attributes["placeholder"] = "(Opcional)";
                txtPiso.Attributes["placeholder"] = "(Opcional)";
                txtDepartamento.Attributes["placeholder"] = "(Opcional)";
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            user = new Usuario();
            negocioU = new UsuarioNegocio();
            direccion = new Direccion();
            negocioD = new DireccionNegocio();

            user.DNI = (int)Session["dni"];
            user.Contraseña = txtContraseña.Text;

            user.direccion.Calle = txtCalle.Text;
            user.direccion.Numero = int.Parse(txtNumeracion.Text);
            if (!string.IsNullOrEmpty(txtPiso.Text))
                user.direccion.Piso = int.Parse(txtPiso.Text);
            if (!string.IsNullOrEmpty(txtDepartamento.Text))
                user.direccion.Departamento = txtDepartamento.Text;
            user.direccion.CodPostal = txtCP.Text;
            user.direccion.Provincia.Id = int.Parse(ddlProvincia.SelectedValue);
            user.direccion.Localidad.Id = int.Parse(ddlLocalidad.SelectedValue);

            int IdDireccion = negocioD.AgregarDireccion(user.direccion);

            user.Nombres = txtNombres.Text;
            user.Apellidos = txtApellidos.Text;
            user.Email = txtEmail.Text;
            if (!string.IsNullOrEmpty(txtTelefono.Text))
                user.Telefono = txtTelefono.Text;
            user.direccion.IdDireccion = IdDireccion;

            user.IdUsuario = negocioU.AgregarUsuario(user);

            Session.Add("usuario", user);
            Response.Redirect("Default.aspx");
        }
    }
}