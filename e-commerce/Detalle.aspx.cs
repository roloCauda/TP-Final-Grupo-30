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
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            Carrito carrito = (Carrito)Session["ListaItems"];

            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            /*  Actualiza las Label de la Master */
            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            Label lblPrecio = Master.FindControl("lblPrecio") as Label;
            lblPrecio.Text = "$" + carrito.total.ToString();

            /*  Trae el ID de Default */
            string idArticulo = Request.QueryString["id"].ToString();
            try
            {

                if (!string.IsNullOrEmpty(idArticulo))
                {
                    /*  Carga el articulo */
                    int id = int.Parse(idArticulo);
                    art = negocio.cargarArticulo(id);
                    lblNombre.Text = art.Nombre;
                    lblDescripcion.Text = art.Descripcion;
                    lblMarca.Text = art.IdMarca.Descripcion;
                    lblCategoria.Text = art.IdCategoria.Descripcion;
                    lblPrecioArt.Text = art.Precio.ToString();
                    lblStock.Text = art.stock.ToString();
                    Session["ArticuloSeleccionado"] = art;

                    if (art.stock == 0)
                    {
                        btnAgregarAlCarrito.Enabled = false;
                        lblStock.Text = "SIN STOCK";
                    }

                    // Validar si la cantidad en el carrito es igual a la cantidad en stock
                    if (carrito.ListaItems.Any(item => item.Articulo.IdArticulo == art.IdArticulo && item.Cantidad == art.stock))
                    {
                        // Si la cantidad en el carrito es igual a la cantidad en stock, deshabilitar el botón de agregar
                        btnAgregarAlCarrito.Enabled = false;
                    }
                    else
                    {
                        btnAgregarAlCarrito.Enabled = true;
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            /*  Carga las imagenes del articulo seleccionado */
            rptItems.DataSource = art.ListaImagenes;
            rptItems.DataBind();

            if (Session["usuario"] == null)
            {
                lnkFavorito.Visible = false;
            }
            else
            {
                Usuario user = (Usuario)Session["usuario"];

                if (user.ListaFavoritos.Any(favorito => favorito.IdArticulo == int.Parse(idArticulo)))
                {
                    lnkFavorito.CssClass = "bi bi-heart-fill";
                }
                else
                {
                    lnkFavorito.CssClass = "bi bi-heart";
                }
            }


        }

        protected void btnAgregar_click(object sender, EventArgs e)
        {
            Carrito carrito = (Carrito)Session["ListaItems"];
            Articulo artSeleccionado = (Articulo)Session["ArticuloSeleccionado"];

            bool articuloYaExiste = false;

            /*  Busca el art en la lista y le suma una unidad */
            foreach (ItemCarrito item in carrito.ListaItems)
            {


                if (item.Articulo.IdArticulo == artSeleccionado.IdArticulo)
                {
                    item.Cantidad += 1;
                    articuloYaExiste = true;
                    break;
                }
            }

            /*  Busca el art en la lista, si no lo encuentra, lo crea y le suma una unidad */
            if (!articuloYaExiste)
            {
                ItemCarrito nuevoItem = new ItemCarrito
                {
                    Articulo = artSeleccionado,
                    Cantidad = 1
                };

                carrito.ListaItems.Add(nuevoItem);
            }

            /*  Suma al total del carrio el monto del articulo */
            carrito.total += artSeleccionado.Precio;

            /*  Actualiza las Label de la Master */
            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            Label lblPrecio = Master.FindControl("lblPrecio") as Label;
            lblPrecio.Text = "$" + carrito.total.ToString();

            /*  Hace que se actualice la pagina default y actualice el carrito de la master */
            Response.Redirect("Detalle.aspx?id=" + artSeleccionado.IdArticulo);
        }

        protected void btnQuitar_click(object sender, EventArgs e)
        {
            Carrito carrito = (Carrito)Session["ListaItems"];
            Articulo artSeleccionado = (Articulo)Session["ArticuloSeleccionado"];

            ItemCarrito itemExistente = null;

            foreach (ItemCarrito item in carrito.ListaItems)
            {
                if (item.Articulo.IdArticulo == artSeleccionado.IdArticulo)
                {
                    itemExistente = item;
                    break;
                }
            }

            if (itemExistente != null)
            {
                if (itemExistente.Cantidad > 1)
                {
                    itemExistente.Cantidad -= 1;
                }
                else
                {
                    carrito.ListaItems.Remove(itemExistente);
                }

                carrito.total -= artSeleccionado.Precio;

                /*  Actualiza las Label de la Master */
                Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                /*  Hace que se actualice la pagina default y actualice el carrito de la master */
                Response.Redirect("Detalle.aspx?id=" + artSeleccionado.IdArticulo);
            }
        }

        protected void lnkFavorito_Click(object sender, EventArgs e)
        {
            LinkButton lnkFavorito = (LinkButton)sender;
            Usuario user = (Usuario)Session["usuario"];
            int idArticulo = int.Parse(Request.QueryString["id"]);

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
    }
}