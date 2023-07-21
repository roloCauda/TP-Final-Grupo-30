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
    public partial class VerMarcas : System.Web.UI.Page
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
                MarcaNegocio negocio = new MarcaNegocio();
                dgvMarca.DataSource = negocio.listar();
                dgvMarca.DataBind(); /*para que enlace los datos, que los escriba en la grilla*/

                foreach (GridViewRow row in dgvMarca.Rows)
                {
                    LinkButton lnkModificar = (LinkButton)row.FindControl("lnkModificar");
                    LinkButton lnkEliminar = (LinkButton)row.FindControl("lnkEliminar");

                    if (user.TipoUsuario != TipoUsuario.ADMIN)
                    {
                        lnkModificar.Visible = false;
                        lnkEliminar.Visible = false;
                    }
                }
                dgvMarca.PageIndexChanging += dgvMarca_PageIndexChanging;
            }
        }

        protected void dgvMarca_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarca.PageIndex = e.NewPageIndex;
            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarca.DataSource = negocio.listar();
            dgvMarca.DataBind();
        }

        protected void dgvMarca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver" || e.CommandName == "Modificar" || e.CommandName == "Eliminar")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string idMarca = dgvMarca.DataKeys[row.RowIndex].Value.ToString();

                // Acciones según el comando seleccionado
                if (e.CommandName == "Ver")
                {
                    // Acción cuando se presiona el botón "Ver"
                    Response.Redirect("AgregarMarca.aspx?id=" + idMarca);
                }
                else if (e.CommandName == "Modificar")
                {
                    // Acción cuando se presiona el botón "Modificar"
                    Response.Redirect("AgregarMarca.aspx?id=" + idMarca);
                }
                else if (e.CommandName == "Eliminar")
                {
                    // Acción cuando se presiona el botón "Eliminar"
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.eliminar(int.Parse(idMarca));
                    Response.Redirect("VerMarcas.aspx");
                }
            }
        }
    }
}