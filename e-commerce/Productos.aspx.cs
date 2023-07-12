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

            /* Trae el contenido de la textBox que esta en la Master */
            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (!IsPostBack) /* Primera vez que carga la pagina (incluye si viene de otra pagina) */
            {
                /* Trae el texto del filtro de las otras paginas */
                string filtro = Request.QueryString["txtFiltro"];

                /* si el filtro viene de Productos */
                if (filtro != null && !string.IsNullOrEmpty(filtro))
                {
                    ListaArticulo = negocio.listarConSP(filtro);
                    repRepetidor.DataSource = ListaArticulo;
                    repRepetidor.DataBind();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["marca"]))
                    {
                        string idMarca = Request.QueryString["marca"];
                        ListaArticulo = negocio.listarPorMarca(idMarca);
                        repRepetidor.DataSource = ListaArticulo;
                        repRepetidor.DataBind();
                    }
                    else
                    {
                        ListaArticulo = negocio.listarConSP();
                        repRepetidor.DataSource = ListaArticulo;
                        repRepetidor.DataBind();
                    }
                }
            }
            else
            {
                if (txtFiltro.Text != null && !string.IsNullOrEmpty(txtFiltro.Text)) /* si el filtro viene de otra pagina */
                {
                    ListaArticulo = negocio.listarConSP(txtFiltro.Text);
                    repRepetidor.DataSource = ListaArticulo;
                    repRepetidor.DataBind();
                }
                else
                {
                    
                }
            }



            /*  Actualiza las Label de la Master */
            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            Label lblPrecio = Master.FindControl("lblPrecio") as Label;
            lblPrecio.Text = "$" + carrito.total.ToString();
        }
    }
}