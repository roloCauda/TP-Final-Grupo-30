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
            pnl_Perfil.Visible = true;
            pnl_Direccion.Visible = false;
            pnl_Contrasena.Visible = false;
            pnl_Favoritos.Visible = false;
            pnl_Pedidos.Visible = false;
            pnl_Salir.Visible = false;

        }

        protected void lnk_Perfil_Click(object sender, EventArgs e)
        {
            pnl_Perfil.Visible = true;
            pnl_Direccion.Visible = false;
            pnl_Contrasena.Visible = false;
            pnl_Favoritos.Visible = false;
            pnl_Pedidos.Visible = false;
            pnl_Salir.Visible = false;

        }

        protected void lnk_Direccion_Click(object sender, EventArgs e)
        {
            pnl_Perfil.Visible = false;
            pnl_Direccion.Visible = true;
            pnl_Contrasena.Visible = false;
            pnl_Favoritos.Visible = false;
            pnl_Pedidos.Visible = false;
            pnl_Salir.Visible = false;

        }

        protected void lnk_Contraseña_Click(object sender, EventArgs e)
        {
            pnl_Perfil.Visible = false;
            pnl_Direccion.Visible = false;
            pnl_Contrasena.Visible = true;
            pnl_Favoritos.Visible = false;
            pnl_Pedidos.Visible = false;
            pnl_Salir.Visible = false;

        }

        protected void lnk_Favoritos_Click(object sender, EventArgs e)
        {
            pnl_Perfil.Visible = false;
            pnl_Direccion.Visible = false;
            pnl_Contrasena.Visible = false;
            pnl_Favoritos.Visible = true;
            pnl_Pedidos.Visible = false;
            pnl_Salir.Visible = false;
        }

        protected void lnk_Pedidos_Click(object sender, EventArgs e)
        {
            pnl_Perfil.Visible = false;
            pnl_Direccion.Visible = false;
            pnl_Contrasena.Visible = false;
            pnl_Favoritos.Visible = false;
            pnl_Pedidos.Visible = true;
            pnl_Salir.Visible = false;
        }

        protected void lnk_Salir_Click(object sender, EventArgs e)
        {
            pnl_Perfil.Visible = false;
            pnl_Direccion.Visible = false;
            pnl_Contrasena.Visible = false;
            pnl_Favoritos.Visible = false;
            pnl_Pedidos.Visible = false;
            pnl_Salir.Visible = true;
        }
    }
}