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

                FormaDeEnvioNegocio negopcioE = new FormaDeEnvioNegocio();

                ddlFormaDeEnvio.DataSource = negopcioE.listar();
                ddlFormaDeEnvio.DataValueField = "IdFormaDeEnvio";
                ddlFormaDeEnvio.DataTextField = "Descripcion";
                ddlFormaDeEnvio.DataBind();

                ddlFormaDeEnvio.SelectedValue = negocioP.consultaIdEnvio(IdPedido).ToString();

                FormaDePagoNegocio negopcioPA = new FormaDePagoNegocio();

                ddlFormaDePago.DataSource = negopcioPA.listar();
                ddlFormaDePago.DataValueField = "IdFormaDePago";
                ddlFormaDePago.DataTextField = "Descripcion";
                ddlFormaDePago.DataBind();

                ddlFormaDePago.SelectedValue = negocioP.consultaIdPago(IdPedido).ToString();

                List<string> opciones = new List<string>
                {
                    "Pendiente",
                    "En proceso",
                    "Enviado",
                    "Entregado",
                    "Cancelado"
                };

                ckblEstadoPedido.DataSource = opciones;
                ckblEstadoPedido.DataBind();
            }
        }

        protected void dgvArtPorPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArtPorPedido.PageIndex = e.NewPageIndex;
            dgvArtPorPedido.DataBind();
        }

        protected void ckblEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ckblEstadoPedido.Items.Count; i++)
            {
                // Si la casilla de verificación está seleccionada
                if (ckblEstadoPedido.Items[i].Selected)
                {
                    // Desactivar la casilla de verificación para que no se pueda deseleccionar
                    ckblEstadoPedido.Items[i].Enabled = false;
                }
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string estadoPedido = ObtenerUltimoEstadoSeleccionado();
        }
        private string ObtenerUltimoEstadoSeleccionado()
        {
            string ultimoEstado = "";

            foreach (ListItem item in ckblEstadoPedido.Items)
            {
                if (item.Selected)
                {
                    ultimoEstado = item.Text;
                }
            }

            return ultimoEstado;
        }
    }
}