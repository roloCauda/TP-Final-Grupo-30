using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace e_commerce
{
    public partial class VerProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];

            if (Session["usuario"] == null || user.TipoUsuario == TipoUsuario.CLIENTE)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulo.DataSource = negocio.listarConSP();
                dgvArticulo.DataBind(); /*para que enlace los datos, que los escriba en la grilla*/

                foreach (GridViewRow row in dgvArticulo.Rows)
                {
                    LinkButton lnkModificar = (LinkButton)row.FindControl("lnkModificar");
                    LinkButton lnkEliminar = (LinkButton)row.FindControl("lnkEliminar");

                    if (user.TipoUsuario != TipoUsuario.ADMIN)
                    {
                        lnkModificar.Visible = false;
                        lnkEliminar.Visible = false;
                    }
                }
            }

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