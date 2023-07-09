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

            MostrarPanel("Datos");

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Default.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            lblPrecio.Text = "$" + carrito.total.ToString();

            repFinalizar.DataSource = carrito.ListaItems;
            repFinalizar.DataBind();
        }
        protected void lnk_Opcion_Click(object sender, EventArgs e)
        {
            LinkButton lnk_Opcion = (LinkButton)sender;
            string opcion = lnk_Opcion.CommandArgument;
            MostrarPanel(opcion);
        }
        private void MostrarPanel(string opcion)
        {
            pnl_Datos.Visible = (opcion == "Datos");
            pnl_Envio.Visible = (opcion == "Envio");
            pnl_Pagos.Visible = (opcion == "Pagos");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Button btn_Opcion = (Button)sender;
            string opcion = btn_Opcion.CommandArgument;
            MostrarPanel(opcion);
        }

    }
}