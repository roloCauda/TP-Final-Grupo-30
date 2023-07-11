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
    public partial class FinDeCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

                TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

                MostrarPanel("Datos");

                LocalidadNegocio negocioLocalidad = new LocalidadNegocio();
                List<Localidad> listaLocalidad = negocioLocalidad.listar();

                ProvinciaNegocio negocioProvincia = new ProvinciaNegocio();
                List<Provincia> listaProvincia = negocioProvincia.listar();

                ddlLocalidad.DataSource = listaLocalidad;
                ddlLocalidad.DataValueField = "Id";
                ddlLocalidad.DataTextField = "Descripcion";
                ddlLocalidad.DataBind();

                ddlProvincia.DataSource = listaProvincia;
                ddlProvincia.DataValueField = "Id";
                ddlProvincia.DataTextField = "Descripcion";
                ddlProvincia.DataBind();

                if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
                {
                    Response.Redirect("Default.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
                }

                lblPrecio.Text = "$" + carrito.total.ToString();

                repFinalizar.DataSource = carrito.ListaItems;
                repFinalizar.DataBind();
            }



        }
        private void MostrarPanel(string opcion)
        {
            pnl_Datos.Visible = (opcion == "Datos");
            pnl_Envio.Visible = (opcion == "Envio");
            pnl_Pagos.Visible = (opcion == "Pagos");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Button btn_Opcion = (Button)sender;
            string opcion = btn_Opcion.CommandArgument;
            MostrarPanel(opcion);
        }
        protected void btnConfirmar(object sender, EventArgs e)
        {

        }
    }
}