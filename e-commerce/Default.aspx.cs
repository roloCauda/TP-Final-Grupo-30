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
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public Carrito carrito { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            carrito = (Carrito)Session["ListaItems"];

            if (!IsPostBack) /* Primera vez que carga la pagina (incluye si viene de otra pagina) */
            {
                /* si no hay carrito le asigna 0 */
                if (Session["TotalCarrito"] == null)
                {
                    Session["TotalCarrito"] = 0;
                }

                if (carrito == null)
                {
                    carrito = new Carrito();
                    Session["ListaItems"] = carrito;
                }

                Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                lblCantCarrito.Text = carrito.ListaItems.Count.ToString();
                Label lblPrecio = Master.FindControl("lblPrecio") as Label;
                lblPrecio.Text = "$" + carrito.total.ToString();
            }
        }
    }
}