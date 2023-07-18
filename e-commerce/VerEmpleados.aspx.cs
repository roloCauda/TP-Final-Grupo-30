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
    public partial class VerEmpelados : System.Web.UI.Page
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
                rblOpciones.SelectedValue = "1";

                UsuarioNegocio negocio = new UsuarioNegocio();
                dgvEmpleados.DataSource = negocio.listarSegunAcceso(2, -1);
                dgvEmpleados.DataBind();
            }
        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            dgvEmpleados.DataBind();
        }

        protected void dgvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver" || e.CommandName == "Baja" || e.CommandName == "Alta")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string idUsuario = dgvEmpleados.DataKeys[row.RowIndex].Value.ToString();
                UsuarioNegocio negocio;

                // Acciones según el comando seleccionado
                if (e.CommandName == "Ver")
                {
                    // Acción cuando se presiona el botón "Ver"
                    Response.Redirect("DetalleCliente.aspx?id=" + idUsuario);
                }
                else if (e.CommandName == "Baja")
                {
                    negocio = new UsuarioNegocio();
                    negocio.CambiarEstadoActivo(idUsuario, 0);
                }
                else
                {
                    negocio = new UsuarioNegocio();
                    negocio.CambiarEstadoActivo(idUsuario, 1);
                }
            }
        }

        protected void dgvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Usuario user = (Usuario)Session["usuario"];

                //obtengo el objeto usuario de la fila
                Usuario usuario = (Usuario)e.Row.DataItem;

                // Buscar los controles LinkButton en la fila
                LinkButton lnkBaja = (LinkButton)e.Row.FindControl("lnkBaja");
                LinkButton lnkAlta = (LinkButton)e.Row.FindControl("lnkAlta");

                if (user.TipoUsuario != TipoUsuario.ADMIN)
                {
                    lnkAlta.Visible = false;
                    lnkBaja.Visible = false;
                }
                else
                {
                    if (usuario.Activo)
                    {
                        // Si el usuario está activo, ocultar el botón lnkAlta
                        lnkAlta.Visible = false;
                    }
                    else
                    {
                        // Si el usuario está inactivo, ocultar el botón lnkBaja
                        lnkBaja.Visible = false;
                    }
                }
            }
        }

        protected void rblOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = rblOpciones.SelectedItem.Text;
            UsuarioNegocio negocioU = new UsuarioNegocio();

            if (filtro == "Todos")
            {
                dgvEmpleados.DataSource = negocioU.listarSegunAcceso(2, -1);
            }
            else if (filtro == "Activos")
            {
                dgvEmpleados.DataSource = negocioU.listarSegunAcceso(2, 1);
            }
            else
            {
                dgvEmpleados.DataSource = negocioU.listarSegunAcceso(2, 0);
            }
            dgvEmpleados.DataBind();
        }
    }
}