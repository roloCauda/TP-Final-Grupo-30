using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class CompraRealizada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se verifica que haya un usuario en session
            if (Session["usuario"] == null)
                Response.Redirect("Default.aspx");

            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            repResumen.DataSource = carrito.ListaItems;
            repResumen.DataBind();

            lblPrecio.Text = "$" + carrito.total.ToString();
        }
    }
}