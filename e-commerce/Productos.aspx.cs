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
    public partial class Productos : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public Carrito carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            carrito = (Carrito)Session["ListaItems"];

            ListaArticulo = negocio.listarConSP();
            repRepetidor.DataSource = ListaArticulo;
            repRepetidor.DataBind();

            /*  Actualiza las Label de la Master */
            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            Label lblPrecio = Master.FindControl("lblPrecio") as Label;
            lblPrecio.Text = "$" + carrito.total.ToString();
        }
    }
}