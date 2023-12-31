﻿using dominio;
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
                    else if(!string.IsNullOrEmpty(Request.QueryString["categoria"]))
                    {
                        string idCategoria = Request.QueryString["categoria"];
                        ListaArticulo = negocio.listarPorCategoria(idCategoria);
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
            }

            /*  Actualiza las Label de la Master */
            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            Label lblPrecio = Master.FindControl("lblPrecio") as Label;
            lblPrecio.Text = "$" + carrito.total.ToString();
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

        protected void lnkFavorito_Click(object sender, EventArgs e)
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
    }
}