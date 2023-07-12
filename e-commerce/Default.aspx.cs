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
        public List<Marca> ListaMarca { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                carrito = (Carrito)Session["ListaItems"];

                Pedido pedido = new Pedido();
                Session["pedido"] = pedido;

                MarcaNegocio marcaNegocio = new MarcaNegocio();

                ListaArticulo = negocio.listarConSP();

                ListaMarca = marcaNegocio.listar();

                // Limitar la cantidad de elementos a mostrar
                List<Articulo> primerosTresArticulos = ListaArticulo.Take(3).ToList();

                repRepetidor.DataSource = primerosTresArticulos;
                repRepetidor.DataBind();

                repRepetidorMarca.DataSource = ListaMarca;
                repRepetidorMarca.DataBind();


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