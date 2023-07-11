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
        ImagenNegocio negocioIMG = null;
        private Articulo articulo = null;
        private List<Imagen> ListaImagenes = null;
        string imagenVacia = "https://laboratoriodesuenos.com/wp-content/uploads/2020/02/default.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];

            if (Session["usuario"] == null || user.TipoUsuario != TipoUsuario.ADMIN)
            {
                Response.Redirect("Default.aspx");
            }

            txtId.Enabled = false;

            try
            {
                // Primera vez que carga la página.
                if (!IsPostBack)
                {
                    Session.Remove("ListaImagenes");

                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarca = negocioMarca.listar();

                    CategoriaNegocio negocioCat = new CategoriaNegocio();
                    List<Categoria> listaCategoria = negocioCat.listar();

                    ListaImagenes = new List<Imagen>();
                    Session["ListaImagenes"] = ListaImagenes;

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

                        negocioIMG = new ImagenNegocio();
                        ListaImagenes = negocioIMG.listar(idArticulo);
                    }
                    else
                    {
                        btnAgregar.Visible = true;
                        btnModificar.Visible = false;
                    }

                    /*  Carga las imagenes del articulo seleccionado */
                    if (ListaImagenes.Count > 0)
                    {
                        dgvImagenes.DataSource = ListaImagenes;
                        dgvImagenes.DataBind();
                    }
                    else
                    {
                        Imagen imagen = new Imagen { ImagenURL = imagenVacia };
                        ListaImagenes.Add(imagen);

                        dgvImagenes.DataSource = ListaImagenes;
                        dgvImagenes.DataBind();
                    }
                    Session["ListaImagenes"] = ListaImagenes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            articulo = new Articulo();
            negocioIMG = new ImagenNegocio();
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

                int nuevoId = negocio.agregar(articulo);

                List<Imagen> ListaImagenes = (List<Imagen>)Session["ListaImagenes"];

                ListaImagenes.RemoveAll(imagenBorrar => imagenBorrar.ImagenURL == imagenVacia);
                negocioIMG.agregar(ListaImagenes, nuevoId);
            }
            else
            {
                //MessageBox.Show("Completar campos obligatorios: Código, Nombre y Precio");
                return;
            }

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("ListaImagenes");
            Response.Redirect("VerProductos.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            articulo = new Articulo();
            articulo.IdCategoria = new Categoria();
            articulo.IdMarca = new Marca();

            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(ddlMarca.SelectedValue) && !string.IsNullOrEmpty(ddlCategoria.SelectedValue))
            {
                articulo.IdArticulo = Convert.ToInt32(Request.QueryString["id"]);
                articulo.IdMarca.IdMarca = int.Parse(ddlMarca.SelectedValue);
                articulo.IdCategoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                articulo.Nombre = txtNombre.Text;
                articulo.Codigo = (!string.IsNullOrEmpty(txtCodigo.Text)) ? txtCodigo.Text : "Vacio";
                articulo.Descripcion = (!string.IsNullOrEmpty(txtDescripcion.Text)) ? txtDescripcion.Text : "Vacio";
                articulo.Precio = (decimal.TryParse(txtPrecio.Text, out decimal precio)) ? articulo.Precio = precio : articulo.Precio = 0;

                negocio.modificar(articulo);

                ImagenNegocio negocioIMG = new ImagenNegocio();
                ListaImagenes = (List<Imagen>)Session["ListaImagenes"];
                ListaImagenes.RemoveAll(imagenBorrar => imagenBorrar.ImagenURL == imagenVacia);
                negocioIMG.modificar(ListaImagenes, articulo.IdArticulo);
            }
            else
            {
                //MessageBox.Show("Completar campos obligatorios: Código, Nombre y Precio");
                return;
            }
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            //Verifica que txtURLIMAGEN no este vavio
            if (!string.IsNullOrEmpty(txtURLIMAGEN.Text))
            {
                List<Imagen> ListaImagenes = (List<Imagen>)Session["ListaImagenes"];

                //Elimina de la lista la imagen de VACIO
                ListaImagenes.RemoveAll(imagenBorrar => imagenBorrar.ImagenURL == imagenVacia);

                //Verifica que la URL a ingresar no este en la lista
                if (!ListaImagenes.Any(imagenAux => imagenAux.ImagenURL == txtURLIMAGEN.Text))
                {
                    Imagen imagen = new Imagen { ImagenURL = txtURLIMAGEN.Text };

                    ListaImagenes.Add(imagen);

                    dgvImagenes.DataSource = ListaImagenes;
                    dgvImagenes.DataBind();

                    txtURLIMAGEN.Text = "";
                    Session["ListaImagenes"] = ListaImagenes;
                }
                else
                {
                    //DEBERIA MOSTRAR UN CARTEL QUE DIGA QUE LA IMAGEN LA EXISTE PARA ESE ARTICULO
                    txtURLIMAGEN.Text = "";
                }
            }
        }

        protected void dgvImagenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //traigo el ID de imagen de la grilla
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            int idImagen = int.Parse(dgvImagenes.DataKeys[row.RowIndex].Value.ToString());

            List<Imagen> ListaImagenes = (List<Imagen>)Session["ListaImagenes"];

            //Elimino la imagen de la lista, segun el ID
            ListaImagenes.RemoveAll(imagen => imagen.IdImagen == idImagen);

            //si la lista no tiene imagenes, creo una imagen, le cargo la imagen de VACIO y se la agrego a la lista
            if(ListaImagenes.Count == 0)
            {
                Imagen imagen = new Imagen { ImagenURL = imagenVacia };
                ListaImagenes.Add(imagen);
            }

            // Actualiza la lista en la sesión
            Session["ListaImagenes"] = ListaImagenes;

            dgvImagenes.DataSource = ListaImagenes;
            dgvImagenes.DataBind();
        }
    }
}