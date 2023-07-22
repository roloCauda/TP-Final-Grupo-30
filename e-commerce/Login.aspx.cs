using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace e_commerce
{
    public partial class Login : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public dominio.Carrito carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAviso.Visible = false;
            
            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            if (Session["usuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }

                lblErrorLogin.Visible = false;
                lblErrorRegistrarse.Visible = false;
                lblErrorRegistrado.Visible = false;

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri == "https://localhost:44330/Carrito.aspx")
                {
                    lblAviso.Visible = true;
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                carrito = (dominio.Carrito)Session["ListaItems"];

                /*  Actualiza las Label de la Master */
                Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                Label lblPrecio = Master.FindControl("lblPrecio") as Label;
                lblPrecio.Text = "$" + carrito.total.ToString();
            }
        }

        protected void btn_Ingresar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario = new Usuario(int.Parse(txtDNI.Text), txtPassword.Text, 0);
                if (negocio.Loguear(usuario))
                {
                    DireccionNegocio negocioD = new DireccionNegocio();
                    usuario.direccion = negocioD.CargarDireccion(usuario.direccion);

                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    lblErrorLogin.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnRecuperarContraseña_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio uNegocio = new UsuarioNegocio();

            try
            {
                if (uNegocio.SiEstaRegistrado(int.Parse(txtDNI.Text)))
                {
                    usuario = uNegocio.CargarUsuarioxDNI(int.Parse(txtDNI.Text));
                    string nuevaPass = RandomStringGenerator.GenerateRandomString(8);

                    uNegocio.cambiarContraseña(usuario, nuevaPass);

                    EmailService emailService = new EmailService();
                    string cuerpoMail = emailService.obtenerCuerpoMailConNuevaContraseña(nuevaPass);
                    emailService.armarCorreo(usuario.Email, "Recuperación de Contraseña", cuerpoMail);

                    try
                    {
                        emailService.enviarCorreo();
                        Response.Redirect("RecuperarPass.aspx");
                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", ex);
                    }
                }
                else
                {
                    lblErrorRegistrado.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            

        }

        protected void TextChanged(object sender, EventArgs e)
        {
            lblErrorLogin.Visible = false;
        }


        protected void btn_CrearCuenta_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (!(negocio.SiEstaRegistrado(int.Parse(txtDNICrearCuenta.Text))))
                {
                    Session["dni"] = int.Parse(txtDNICrearCuenta.Text);

                    Response.Redirect("Registrarse.aspx", false);
                }
                else
                {
                    lblErrorRegistrarse.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}