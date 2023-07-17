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
    public partial class VerCategorias : System.Web.UI.Page
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
                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvCategoria.DataSource = negocio.listar();
                dgvCategoria.DataBind(); /*para que enlace los datos, que los escriba en la grilla*/

                foreach (GridViewRow row in dgvCategoria.Rows)
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

        protected void dgvCategoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategoria.PageIndex = e.NewPageIndex;
            dgvCategoria.DataBind();
        }

        protected void dgvCategoria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver" || e.CommandName == "Modificar" || e.CommandName == "Eliminar")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string IdCategoria = dgvCategoria.DataKeys[row.RowIndex].Value.ToString();

                // Acciones según el comando seleccionado
                if (e.CommandName == "Ver")
                {
                    // Acción cuando se presiona el botón "Ver"
                    Response.Redirect("AgregarCategoria.aspx?id=" + IdCategoria);
                }
                else if (e.CommandName == "Modificar")
                {
                    // Acción cuando se presiona el botón "Modificar"
                    Response.Redirect("AgregarCategoria.aspx?id=" + IdCategoria);
                }
                else if (e.CommandName == "Eliminar")
                {
                    // Acción cuando se presiona el botón "Eliminar"
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.eliminar(int.Parse(IdCategoria));
                    Response.Redirect("VerCategorias.aspx");
                }
            }
        }
    }
}