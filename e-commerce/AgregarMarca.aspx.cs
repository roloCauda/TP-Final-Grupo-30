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
    public partial class AgregarMarca : System.Web.UI.Page
    {
        MarcaNegocio negocio = new MarcaNegocio();
        Marca marca = new Marca();
        string imagenVacia = "https://laboratoriodesuenos.com/wp-content/uploads/2020/02/default.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;

            try
            {
                // Primera vez que carga la página.
                if (!IsPostBack)
                {
                    Session.Remove("imgMarca");

                    btnAgregar.Visible = true;
                    btnModificar.Visible = false;

                    /*  PreCarga el articulo */
                    if (Request.QueryString["id"] != null)
                    {
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;

                        negocio = new MarcaNegocio();
                        marca = new Marca();

                        int idMarca = Convert.ToInt32(Request.QueryString["id"]);

                        marca = negocio.cargarMarca(idMarca);
                        txtId.Text = marca.IdMarca.ToString();
                        txtDescripcion.Text = marca.Descripcion;
                    }

                    if (marca.ImagenURL != null)
                    {
                        imgMarca.ImageUrl = marca.ImagenURL;
                        txtURLIMAGEN.Enabled = false;
                        btnAgregarImagen.Visible = false;
                        btnEliminarImagen.Visible = true;
                        Session["imgMarca"] = marca.ImagenURL;
                    }
                    else
                    {
                        imgMarca.ImageUrl = imagenVacia;
                        txtURLIMAGEN.Enabled = true;
                        btnAgregarImagen.Visible = true;
                        btnEliminarImagen.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //DEBERIA SER SESSION ERROR???//
                throw ex;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            negocio = new MarcaNegocio();
            marca = new Marca();

            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                marca.Descripcion = txtDescripcion.Text;

                //SI EL CAMPO txtURLIMAGEN tiene algo, deberia decir un cartel si quiere agregar la imagen, porque no apretarone el boton + para agregar

                marca.ImagenURL = ((string)Session["imgMarca"]) != null ? (string)Session["imgMarca"] : null;

                negocio.agregar(marca);
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
            Session.Remove("imgMarca");
            Response.Redirect("VerMarcas.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            negocio = new MarcaNegocio();
            marca = new Marca();

            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                marca.IdMarca = Convert.ToInt32(Request.QueryString["id"]);
                marca.Descripcion = txtDescripcion.Text;
                marca.ImagenURL = ((string)Session["imgMarca"]) != null ? (string)Session["imgMarca"] : null;

                negocio.modificar(marca);
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
                Session["imgMarca"] = txtURLIMAGEN.Text;
                imgMarca.ImageUrl = txtURLIMAGEN.Text;

                txtURLIMAGEN.Enabled = false;
                btnAgregarImagen.Visible = false;
                btnEliminarImagen.Visible = true;
                txtURLIMAGEN.Text = "";
            }
        }

        protected void txtURLIMAGEN_TextChanged(object sender, EventArgs e)
        {
            imgMarca.ImageUrl = txtURLIMAGEN.Text;
        }

        protected void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            Session.Remove("imgMarca");
            imgMarca.ImageUrl = imagenVacia;

            txtURLIMAGEN.Enabled = true;
            btnAgregarImagen.Visible = true;
            btnEliminarImagen.Visible = false;
        }
    }
}