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
            if (!IsPostBack)
            {
                lblErrorRegistro.Visible = false;
                pnl_Dni_Email.Visible = true;
                pnl_Perfil_Direccion.Visible = false;

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
            direccion.Provincia = new Provincia();
            direccion.Localidad = new Localidad();

            user.DNI = int.Parse(txtDNI.Text);
            user.Contraseña = txtContraseña.Text;

            direccion.Calle = txtCalle.Text;
            direccion.Numero = int.Parse(txtNumeracion.Text);
            direccion.Piso = txtPiso.Text;
            direccion.Departamento = txtDepartamento.Text;
            direccion.CodPostal = txtCP.Text;
            direccion.Provincia.Id = int.Parse(ddlLocalidad.SelectedValue);
            direccion.Localidad.Id = int.Parse(ddlProvincia.SelectedValue);

            int IdDireccion = negocioD.AgregarDireccion(direccion);

            user.Nombres = txtNombres.Text;
            user.Apellidos = txtApellidos.Text;
            user.Email = txtEmail.Text;
            user.Telefono = txtTelefono.Text;
            user.direccion.IdDireccion = IdDireccion;

            negocioU.AgregarUsuario(user);

            Response.Redirect("Default.aspx");
        }

        protected void btnSeguir_Click(object sender, EventArgs e)
        {
            user = new Usuario();
            negocioU = new UsuarioNegocio();
            direccion = new Direccion();
            negocioD = new DireccionNegocio();

            if (negocioU.SiEstaRegistrado(int.Parse(txtDNI.Text)))
            {
                lblErrorRegistro.Visible = true;
            }
            else
            {
                lblErrorRegistro.Visible = false;
                pnl_Dni_Email.Visible = false;
                pnl_Perfil_Direccion.Visible = true;
            }
        }
    }
}