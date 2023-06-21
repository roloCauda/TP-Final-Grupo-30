using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace e_commerce.Pag_Admin
{
    public partial class AgregarProductos : System.Web.UI.Page
    {
        private ArticuloNegocio negocio;
        private Articulo articulo = null;
        private List<Imagen> ListaImagenes = null;
        private List<string> ListaStringImagenes = new List<string>();
        private int IndiceImagen = -1;
        private int IndiceImagenBorrar = -1;
        private List<string> ListaStringImagenesBorrar = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;

            try
            {
                // Primera vez que carga la página.
                if (!IsPostBack)
                {
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarca = negocioMarca.listar();

                    CategoriaNegocio negocioCat = new CategoriaNegocio();
                    List<Categoria> listaCategoria = negocioCat.listar();

                    btnAgregar.Visible = true;
                    btnModificar.Visible = false;

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "IdMarca";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = listaCategoria;
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    ddlMarca.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                    ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                }

                /*  PreCarga el articulo */
                if (Request.QueryString["id"] != null)
                {
                    btnAgregar.Visible = false;
                    btnModificar.Visible = true;

                    negocio = new ArticuloNegocio();
                    articulo = new Articulo();

                    int idArticulo = Convert.ToInt32(Request.QueryString["id"]);

                    articulo = negocio.cargarArticulo(idArticulo);
                    txtId.Text = articulo.IdArticulo.ToString();
                    txtCodigo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;

                    ddlMarca.SelectedValue = articulo.IdMarca.IdMarca.ToString();
                    ddlCategoria.SelectedValue = articulo.IdCategoria.IdCategoria.ToString();

                    txtPrecio.Text = articulo.Precio.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            articulo = new Articulo();
            articulo.IdCategoria = new Categoria();
            articulo.IdMarca = new Marca();

            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(ddlMarca.SelectedValue) && !string.IsNullOrEmpty(ddlCategoria.SelectedValue))
            {
                articulo.IdCategoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                articulo.IdMarca.IdMarca = int.Parse(ddlMarca.SelectedValue);
                articulo.Nombre = txtNombre.Text;
                articulo.Codigo = (!string.IsNullOrEmpty(txtCodigo.Text)) ? txtCodigo.Text : "Sin codigo";
                articulo.Descripcion = (!string.IsNullOrEmpty(txtDescripcion.Text)) ? txtDescripcion.Text : "Sin descripcion";
                articulo.Precio = (decimal.TryParse(txtPrecio.Text, out decimal precio)) ? articulo.Precio = precio : articulo.Precio = 0;

                negocio.agregar(articulo);
            }
            else
            {
                //MessageBox.Show("Completar campos obligatorios: Código, Nombre y Precio");
                return;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerProductos.aspx?id=");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            articulo.IdCategoria = new Categoria();
            articulo.IdMarca = new Marca();

            articulo.IdCategoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
            articulo.IdMarca.IdMarca = int.Parse(ddlMarca.SelectedValue);
            articulo.Nombre = txtNombre.Text;
            articulo.Codigo = txtCodigo.Text;
            articulo.Descripcion = txtDescripcion.Text;
            articulo.Precio = decimal.Parse(txtPrecio.Text);
            articulo.IdArticulo = int.Parse(txtId.Text);

            negocio.modificar(articulo);
        }
    }
}