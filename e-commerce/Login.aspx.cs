using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblErrorLogin.Visible = false;
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
                    Session.Add("usuario", usuario);
                    Response.Redirect("Cuenta_Usuario.aspx", false);

                    //deberia traer la Direccion, Favoritos y Pedidos de Usuario
                }
                else
                {
                    lblErrorLogin.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void btnRecuperarContraseña_Click(object sender, EventArgs e)
        {

        }

        protected void TextChanged(object sender, EventArgs e)
        {
            lblErrorLogin.Visible = false;
        }
    }
}