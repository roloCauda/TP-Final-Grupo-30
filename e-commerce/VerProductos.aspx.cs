using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace e_commerce
{
    public partial class VerProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            dgvArticulo.DataSource = negocio.listarConSP();
            dgvArticulo.DataBind(); /*para que enlace los datos, que los escriba en la grilla*/
        }

        protected void dgvArticulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulo.PageIndex = e.NewPageIndex;
            dgvArticulo.DataBind();
        }

        protected void dgvArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver" || e.CommandName == "Modificar" || e.CommandName == "Eliminar")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string idArticulo = dgvArticulo.DataKeys[row.RowIndex].Value.ToString();

                // Acciones según el comando seleccionado
                if (e.CommandName == "Ver")
                {
                    // Acción cuando se presiona el botón "Ver"
                    Response.Redirect("AgregarProducto.aspx?id=" + idArticulo);
                }
                else if (e.CommandName == "Modificar")
                {
                    // Acción cuando se presiona el botón "Modificar"
                    Response.Redirect("AgregarProducto.aspx?id=" + idArticulo);
                }
                else if (e.CommandName == "Eliminar")
                {
                    // Acción cuando se presiona el botón "Eliminar"
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(int.Parse(idArticulo));
                    Response.Redirect("VerProductos.aspx");
                }
            }
        }
    }
}