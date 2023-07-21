using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class VerClientes : System.Web.UI.Page
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
                dgvClientes.DataSource = negocio.listarSegunAcceso(3, -1);
                dgvClientes.DataBind();

                dgvClientes.PageIndexChanging += dgvClientes_PageIndexChanging;
            }
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvClientes.DataSource = negocio.listarSegunAcceso(3, -1);
            dgvClientes.DataBind();
        }

        protected void dgvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver" || e.CommandName == "Baja" || e.CommandName == "Alta")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string idUsuario = dgvClientes.DataKeys[row.RowIndex].Value.ToString();
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Acciones según el comando seleccionado
                if (e.CommandName == "Ver")
                {
                    // Acción cuando se presiona el botón "Ver"
                    Response.Redirect("DetalleCliente.aspx?id=" + idUsuario);
                }
                else if(e.CommandName == "Baja")
                {
                    negocio.CambiarEstadoActivo(idUsuario, 0);
                }
                else
                {
                    negocio.CambiarEstadoActivo(idUsuario, 1);
                }

                rblOpciones.SelectedValue = "1";
                dgvClientes.DataSource = negocio.listarSegunAcceso(3, -1);
                dgvClientes.DataBind();
            }
        }

        protected void rblOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = rblOpciones.SelectedItem.Text;
            UsuarioNegocio negocioU = new UsuarioNegocio();

            if (filtro == "Todos")
            {
                dgvClientes.DataSource = negocioU.listarSegunAcceso(3,-1);
            }
            else if(filtro == "Activos")
            {
                dgvClientes.DataSource = negocioU.listarSegunAcceso(3,1);
            }
            else
            {
                dgvClientes.DataSource = negocioU.listarSegunAcceso(3, 0);
            }
                dgvClientes.DataBind();
        }

        protected void dgvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        { //Este evento se dispara para cada fila generada en el control GridView
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Usuario user = (Usuario)Session["usuario"];
                Usuario usuario = (Usuario)e.Row.DataItem;

                // Buscar los controles LinkButton en la fila
                LinkButton lnkBaja = (LinkButton)e.Row.FindControl("lnkBaja");
                LinkButton lnkAlta = (LinkButton)e.Row.FindControl("lnkAlta");

                if(user.TipoUsuario != TipoUsuario.ADMIN)
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
    }
}
