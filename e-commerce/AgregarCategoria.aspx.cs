using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce.Pag_Admin
{
    public partial class AgregarCategoria : System.Web.UI.Page
    {
        CategoriaNegocio negocio = new CategoriaNegocio();
        Categoria cat = new Categoria();
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
                    Session.Remove("imgCategoria");

                    btnAgregar.Visible = true;
                    btnModificar.Visible = false;

                    /*  PreCarga el articulo */
                    if (Request.QueryString["id"] != null)
                    {
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;

                        negocio = new CategoriaNegocio();
                        cat = new Categoria();

                        int idCategoria = Convert.ToInt32(Request.QueryString["id"]);

                        cat = negocio.cargarCategoria(idCategoria);
                        txtId.Text = cat.IdCategoria.ToString();
                        txtDescripcion.Text = cat.Descripcion;
                    }

                    if (cat.ImagenURL != null)
                    {
                        imgCategoria.ImageUrl = cat.ImagenURL; //imgCtegoria es el ID en el html
                        txtURLIMAGEN.Enabled = false;
                        btnAgregarImagen.Visible = false;
                        btnEliminarImagen.Visible = true;
                        Session["imgCategoria"] = cat.ImagenURL;
                    }
                    else
                    {
                        imgCategoria.ImageUrl = imagenVacia;
                        txtURLIMAGEN.Enabled = true;
                        btnAgregarImagen.Visible = true;
                        btnEliminarImagen.Visible= false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            negocio = new CategoriaNegocio();
            cat = new Categoria();

            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                cat.Descripcion = txtDescripcion.Text;

                //SI EL CAMPO txtURLIMAGEN tiene algo, deberia decir un cartel si quiere agregar la imagen, porque no apretarone el boton + para agregar
                
                cat.ImagenURL = ((string)Session["imgCategoria"]) != null ? (string)Session["imgCategoria"] : null;

                negocio.agregar(cat);
            }
            else
            {
                //MessageBox.Show("Completar campos obligatorios: Descripcion");
                return;
            }

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("imgCategoria");
            Response.Redirect("VerCategorias.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            negocio = new CategoriaNegocio();
            cat = new Categoria();

            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                cat.IdCategoria = Convert.ToInt32(Request.QueryString["id"]);
                cat.Descripcion = txtDescripcion.Text;
                cat.ImagenURL = ((string)Session["imgCategoria"]) != null ? (string)Session["imgCategoria"] : null;

                negocio.modificar(cat);
            }
            else
            {
                //MessageBox.Show("Completar campos obligatorios: Descripcion");
                return;
            }

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURLIMAGEN.Text))
            {
                Session["imgCategoria"] = txtURLIMAGEN.Text;
                imgCategoria.ImageUrl = txtURLIMAGEN.Text;

                txtURLIMAGEN.Enabled = false;
                btnAgregarImagen.Visible = false;
                btnEliminarImagen.Visible = true;
                txtURLIMAGEN.Text = "";
            }
        }

        protected void txtURLIMAGEN_TextChanged(object sender, EventArgs e)
        {
            imgCategoria.ImageUrl = txtURLIMAGEN.Text;
        }

        protected void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            Session.Remove("imgCategoria");
            imgCategoria.ImageUrl = imagenVacia;

            txtURLIMAGEN.Enabled = true;
            btnAgregarImagen.Visible = true;
            btnEliminarImagen.Visible = false;
        }
    }
}