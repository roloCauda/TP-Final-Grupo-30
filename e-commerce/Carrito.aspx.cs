using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace e_commerce.Pag_Cliente
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Productos.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            /* Le pasa la lista de items del carrito al DataSource, para que el front pueda acceder a la informacion */
            repCarrito.DataSource = carrito.ListaItems;
            repCarrito.DataBind();
            repResumen.DataSource = carrito.ListaItems;
            repResumen.DataBind();

            lblPrecio.Text = "$" + carrito.total.ToString();
        }
        protected void btnAgregar_click(object sender, EventArgs e)
        {
            /* Le asigno al botonbtnAgregar lo que me trae el boton agregar del front(id), lo casteo y lo guardo en un int */
            Button btnAgregar = (Button)sender;
            int idArticulo = Convert.ToInt32(btnAgregar.CommandArgument);

            /* Le asigna a carrito lo que esta guardado en Session*/
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            /*  Busca el art en la lista y le suma una unidad */
            foreach (ItemCarrito item in carrito.ListaItems)
            {
                if (item.Articulo.IdArticulo == idArticulo)
                {
                    item.Cantidad += 1;
                    carrito.total += item.Articulo.Precio;
                    break;
                }
            }

            /*  Actualiza las Label de la Master */
            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            Label lblPrecio = Master.FindControl("lblPrecio") as Label;
            lblPrecio.Text = "$" + carrito.total.ToString();

            repCarrito.DataSource = carrito.ListaItems;
            repCarrito.DataBind();
            repResumen.DataSource = carrito.ListaItems;
            repResumen.DataBind();

            Session["ListaItems"] = carrito;
            Response.Redirect("Carrito.aspx");
        }
        protected void btnQuitar_click(object sender, EventArgs e)
        {
            /* Le asigno al botonbtnAgregar lo que me trae el boton agregar del front(id), lo casteo y lo guardo en un int */
            Button btnQuitar = (Button)sender;
            int idArticulo = Convert.ToInt32(btnQuitar.CommandArgument);

            /* Le asigna a carrito lo que esta guardado en Session["ListaItems"] */
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            foreach (ItemCarrito item in carrito.ListaItems)
            {
                if (item.Articulo.IdArticulo == idArticulo)
                {
                    if (item.Cantidad > 1)
                    {
                        item.Cantidad -= 1;
                        carrito.total -= item.Articulo.Precio;
                    }
                    else
                    {
                        carrito.total -= item.Articulo.Precio;
                        carrito.ListaItems.Remove(item);
                    }

                    /*  Actualiza las Label de la Master */
                    Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                    lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                    Label lblPrecio = Master.FindControl("lblPrecio") as Label;
                    lblPrecio.Text = "$" + carrito.total.ToString();

                    break;
                }
            }

            repCarrito.DataSource = carrito.ListaItems;
            repCarrito.DataBind();
            repResumen.DataSource = carrito.ListaItems;
            repResumen.DataBind();

            Session["ListaItems"] = carrito;
            Response.Redirect("Carrito.aspx");
        }
        protected void btnBorrar_click(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];


            Button btnBorrar = (Button)sender;
            int idArticulo = Convert.ToInt32(btnBorrar.CommandArgument);

            for (int x = carrito.ListaItems.Count - 1; x >= 0; x--)
            {
                ItemCarrito item = carrito.ListaItems[x];
                if (item.Articulo.IdArticulo == idArticulo)
                {
                    carrito.total -= (item.Articulo.Precio * item.Cantidad);
                    carrito.ListaItems.RemoveAt(x);
                    break;
                }
            }

            Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            repCarrito.DataSource = carrito.ListaItems;
            repCarrito.DataBind();
            repResumen.DataSource = carrito.ListaItems;
            repResumen.DataBind();

            Response.Redirect("Carrito.aspx");
        }
        protected void btnVaciarCarrito_click(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            carrito.ListaItems.Clear();

            carrito.total = 0;
            Session["ListaItems"] = carrito;

            repCarrito.DataSource = carrito.ListaItems;
            repCarrito.DataBind();

            Response.Redirect("Carrito.aspx");
        }    
        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            if (Session["usuario"] != null && carrito.ListaItems.Count != 0)
            {
                Response.Redirect("FinDeCompra.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

    }
}