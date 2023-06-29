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

        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;

            try
            {
                // Primera vez que carga la página.
                if (!IsPostBack)
                {
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

                        if (cat.ImagenURL != null)
                        {
                            imgCategoria.ImageUrl = cat.ImagenURL;
                            txtURLIMAGEN.Enabled = false;
                            Session["imgCategoria"] = cat.ImagenURL;
                        }
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
                cat.ImagenURL = ((string)Session["imgCategoria"]) !=null ? (string)Session["imgCategoria"] : null;

                negocio.agregar(cat);
            }
            else
            {
                //MessageBox.Show("Completar campos obligatorios: Código, Nombre y Precio");
                return;
            }

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
            Session.Remove("ListaImagenes");

            Session.Remove("ListaImagenes");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("ListaImagenes");
            Response.Redirect("VerCategorias.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            






        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURLIMAGEN.Text))
            {
                string imagenURL = txtURLIMAGEN.Text;

                Session["imgCategoria"] = txtURLIMAGEN.Text;

                txtURLIMAGEN.Enabled = false;
                txtURLIMAGEN.Text = "";
            }
        }

        protected void txtURLIMAGEN_TextChanged(object sender, EventArgs e)
        {
            imgCategoria.ImageUrl = txtURLIMAGEN.Text;
        }
    }
}