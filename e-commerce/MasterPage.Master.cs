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
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public Carrito carrito { get; set; }
        public List<ItemCarrito> ListaItems { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ListaItems"] != null)
                {
                    carrito = (Carrito)Session["ListaItems"];
                    Articulo artSeleccionado = (Articulo)Session["ArticuloSeleccionado"];
                }
                else
                {
                    carrito = new Carrito();
                    Session["ListaItems"] = carrito;
                }

                MarcaNegocio negocioM = new MarcaNegocio();
                List<Marca> listaMarcas = negocioM.listar();

                repeaterMarcas.DataSource = listaMarcas;
                repeaterMarcas.DataBind();

                ddlMarcas.DataSource = listaMarcas;
                ddlMarcas.DataValueField = "IdMarca";
                ddlMarcas.DataTextField = "Descripcion";
                ddlMarcas.DataBind();

                repInfoCarrito.DataSource = carrito.ListaItems;
                repInfoCarrito.DataBind();
            }

            if (Request.Form["__EVENTTARGET"] == "carritoCerrado")
            {
                UpdatePanelBoton.Update();
            }

            // Oculta el botón carrito en las páginas que quiera

            if (Request.Url.AbsolutePath.Contains("Carrito") || Request.Url.AbsolutePath.Contains("FinDeCompra"))
            {
                btnCarrito.Visible = false;
            }
            else
            {
                btnCarrito.Visible = true;
            }

            // Lógica para mostrar botones según el tipo de perfil

            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                // Caso Admin o Empleado
                if (user.TipoUsuario == TipoUsuario.ADMIN || user.TipoUsuario == TipoUsuario.EMPLEADO)
                {
                    btnConfig.Visible = true;
                    btnMiPerfil.Visible = true;
                    btnIngresar.Visible = false;
                    btnRegistrarme.Visible = false;
                }
                else
                // Caso de Cliente
                {
                    btnMiPerfil.Visible = true;
                    btnIngresar.Visible = false;
                    btnRegistrarme.Visible = false;
                    btnConfig.Visible = false;
                }
            }
            else
            {
                btnMiPerfil.Visible = false;
                btnIngresar.Visible = true;
                btnRegistrarme.Visible = true;
                btnConfig.Visible = false;
            }

            
        }

        protected void btnAgregar_click(object sender, EventArgs e)
        {
            /* Le asigno al botonbtnAgregar lo que me trae el boton agregar del front(id), lo casteo y lo guardo en un int */
            Button btnAgregar = (Button)sender;
            int idArticulo = Convert.ToInt32(btnAgregar.CommandArgument);

            /* Le asigna a carrito lo que esta guardado en Session*/
            Carrito carrito = (Carrito)Session["ListaItems"];

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

            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

            lblPrecio.Text = "$" + carrito.total.ToString();

            repInfoCarrito.DataSource = carrito.ListaItems;
            repInfoCarrito.DataBind();

            Session["ListaItems"] = carrito;

            UpdatePanel1.Update(); // Actualizar el contenido del UpdatePanel
            UpdatePanelPrecioTotal.Update();
        }

        protected void btnQuitar_click(object sender, EventArgs e)
        {
            /* Le asigno al botonbtnAgregar lo que me trae el boton agregar del front(id), lo casteo y lo guardo en un int */
            Button btnQuitar = (Button)sender;
            int idArticulo = Convert.ToInt32(btnQuitar.CommandArgument);

            /* Le asigna a carrito lo que esta guardado en Session["ListaItems"] */
            Carrito carrito = (Carrito)Session["ListaItems"];


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

                    lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                    lblPrecio.Text = "$" + carrito.total.ToString();

                    break;
                }
            }

            repInfoCarrito.DataSource = carrito.ListaItems;
            repInfoCarrito.DataBind();

            Session["ListaItems"] = carrito;

            UpdatePanel1.Update(); // Actualizar el contenido del UpdatePanel
            UpdatePanelPrecioTotal.Update();
        }

        protected void btnBorrar_click(object sender, EventArgs e)
        {
            Carrito carrito = (Carrito)Session["ListaItems"];

            /*  Me trae el ID del articulo en esa linea  */
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

            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();
            lblPrecio.Text = "$" + carrito.total.ToString();

            repInfoCarrito.DataSource = carrito.ListaItems;
            repInfoCarrito.DataBind();

            UpdatePanel1.Update(); // Actualizar el contenido del UpdatePanel
            UpdatePanelPrecioTotal.Update();
        }

        protected void btnVaciarCarrito_click(object sender, EventArgs e)
        {
            Carrito carrito = (Carrito)Session["ListaItems"];

            carrito.ListaItems.Clear();

            carrito.total = 0;
            Session["ListaItems"] = carrito;

            lblCantCarrito.Text = carrito.ListaItems.Count.ToString();
            lblPrecio.Text = "$" + carrito.total.ToString();


            repInfoCarrito.DataSource = carrito.ListaItems;
            repInfoCarrito.DataBind();
            UpdatePanelVaciar.Update();
            UpdatePanel1.Update();
            UpdatePanelPrecioTotal.Update();

        }
        protected void btnCerrarCarrito_Click(object sender, EventArgs e)
        {
            // Generar el evento de cierre del carrito
            ScriptManager.RegisterStartupScript(this, typeof(Page), "carritoCerrado", "carritoCerrado();", true);

            // Realizar un postback en la página para procesar el evento en el servidor
            ScriptManager.RegisterStartupScript(this, typeof(Page), "postback", Page.ClientScript.GetPostBackEventReference(this, string.Empty), true);
        }
        protected void btnMiPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cuenta_Usuario.aspx");
        }
        protected void btnConfig_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarProductos.aspx");
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        protected void btnRegistrarme_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registrarse.aspx");
        }

    }
}