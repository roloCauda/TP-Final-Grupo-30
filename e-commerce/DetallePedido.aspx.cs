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
    public partial class DetallePedido : System.Web.UI.Page
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
                int IdPedido = int.Parse(Request.QueryString["id"]);

                lblNroPedido.Text = "#" + IdPedido.ToString();

                ArticulosXPedidoNegocio negocioAP = new ArticulosXPedidoNegocio();
                dgvArtPorPedido.DataSource = negocioAP.listarConSP(IdPedido);
                dgvArtPorPedido.DataBind();

                PedidoNegocio negocioP = new PedidoNegocio();
                decimal total = negocioP.totalPedido(IdPedido);
                lblTotal.Text = "Total: $ " + total.ToString();
            }
        }

        protected void dgvArtPorPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvArtPorPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArtPorPedido.PageIndex = e.NewPageIndex;
            dgvArtPorPedido.DataBind();
        }
    }
}