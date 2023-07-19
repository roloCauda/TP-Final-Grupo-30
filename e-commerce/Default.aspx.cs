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

        protected void lnkFavoritoClick(object sender, EventArgs e)
        {
            LinkButton lnkFavorito = (LinkButton)sender;
            Usuario user = (Usuario)Session["usuario"];
            int idArticulo = Convert.ToInt32(lnkFavorito.CommandArgument);

            FavoritoNegocio negocioF = new FavoritoNegocio();

            if (user.ListaFavoritos.Any(favorito => favorito.IdArticulo == idArticulo))
            {
                negocioF.QuitarFavorito(user.IdUsuario, idArticulo);
                user.ListaFavoritos.RemoveAll(favorito => favorito.IdArticulo == idArticulo);
                lnkFavorito.CssClass = "bi bi-heart";
            }
            else
            {
                negocioF.AgregarFavorito(user.IdUsuario, idArticulo);
                Favoritos favoritos = new Favoritos();
                favoritos.IdArticulo = idArticulo;
                user.ListaFavoritos.Add(favoritos);
                lnkFavorito.CssClass = "bi bi-heart-fill";
            }
        }

        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkFavorito = (LinkButton)e.Item.FindControl("lnkFavorito");

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Session["usuario"] == null)
                {
                    lnkFavorito.Visible = false;
                }
                else
                {
                    Usuario user = (Usuario)Session["usuario"];
                    int idArticulo = (int)DataBinder.Eval(e.Item.DataItem, "IdArticulo");

                    if (user.ListaFavoritos.Any(favorito => favorito.IdArticulo == idArticulo))
                    {
                        lnkFavorito.CssClass = "bi bi-heart-fill";
                    }
                    else
                    {
                        lnkFavorito.CssClass = "bi bi-heart";
                    }
                }
            }
        }
    }
}