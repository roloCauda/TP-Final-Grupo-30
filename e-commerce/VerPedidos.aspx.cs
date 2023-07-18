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
    public partial class VerPedidos : System.Web.UI.Page
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

                PedidoNegocio negocioU = new PedidoNegocio();
                dgvPedidos.DataSource = negocioU.listarPedidos();
                dgvPedidos.DataBind();
            }
        }

        protected void dgvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string IdPedido = dgvPedidos.DataKeys[row.RowIndex].Value.ToString();

            // Acciones según el comando seleccionado
            if (e.CommandName == "VerPedido")
            {
                Response.Redirect("DetallePedido.aspx?id=" + IdPedido);
            }


        }

        protected void dgvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPedidos.PageIndex = e.NewPageIndex;
            dgvPedidos.DataBind();
        }

        protected void rblOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = rblOpciones.SelectedItem.Text;
            PedidoNegocio negocioU = new PedidoNegocio();

            if(filtro == "Todos")
            {
                dgvPedidos.DataSource = negocioU.listarPedidos();
                dgvPedidos.DataBind();
            }
            else
            {
                dgvPedidos.DataSource = negocioU.listarPedidos(filtro);
                dgvPedidos.DataBind();
            }
        }
    }
}