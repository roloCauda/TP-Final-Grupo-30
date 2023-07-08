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
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Default.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            lblPrecio.Text = "$" + carrito.total.ToString();

            repFinalizar.DataSource = carrito.ListaItems;
            repFinalizar.DataBind();
        }


        protected void Comprar_Click(object sender, EventArgs e)
        {
 
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

    }
}