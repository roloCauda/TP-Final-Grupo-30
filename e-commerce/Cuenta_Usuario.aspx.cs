using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class Cuenta_Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarPanel("Perfil");
                //txtEmail.Enabled = false;
            }
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
            pnl_Contrasena.Visible = (opcion == "Contraseña");
            pnl_Favoritos.Visible = (opcion == "Favoritos");
            pnl_Pedidos.Visible = (opcion == "Pedidos");
        }

        protected void lnk_Salir_Click(object sender, EventArgs e)
        {

        }
    }
}