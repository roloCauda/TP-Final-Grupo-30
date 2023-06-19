using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce.Pag_Cliente
{
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            Carrito carrito = (Carrito)Session["ListaItems"];

            try
            {
                /*  Trae el ID de Default */
                string idArticulo = Request.QueryString["id"].ToString();

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
                    Session["ArticuloSeleccionado"] = art;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            /*  Carga las imagenes del articulo seleccionado */
            rptItems.DataSource = art.ListaImagenes;
            rptItems.DataBind();
        }

        protected void btnAgregar_click(object sender, EventArgs e)
        {
            
        }

        protected void btnQuitar_click(object sender, EventArgs e)
        {
            
        }
    }
}