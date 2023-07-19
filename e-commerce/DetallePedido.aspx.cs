using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

            List<string> opciones = new List<string>
                {
                    "Pendiente",
                    "En proceso",
                    "Enviado",
                    "Entregado",
                    "Cancelado"
                };

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

                FormaDePagoNegocio negopcioPA = new FormaDePagoNegocio();

                ddlFormaDePago.DataSource = negopcioPA.listar();
                ddlFormaDePago.DataValueField = "IdFormaDePago";
                ddlFormaDePago.DataTextField = "Descripcion";
                ddlFormaDePago.DataBind();

                Pedido pedido = negocioP.cargarPedido(IdPedido);

                //cargar id de Pago y de Envio
                ddlFormaDeEnvio.SelectedValue = pedido.formaDeEnvio.IdFormaDeEnvio.ToString();
                ddlFormaDePago.SelectedValue = pedido.formaDePago.IdFormaDePago.ToString();

                //cargar opcion de EstadoPedido
                if (pedido != null && !string.IsNullOrEmpty(pedido.EstadoPedido))
                {
                    bool estadoOk = false;
                    for (int i = 0; i < opciones.Count; i++)
                    {
                        ckblEstadoPedido.Items.Add(new ListItem(opciones[i]));

                        if ((pedido.EstadoPedido != opciones[i] && !estadoOk) || (pedido.EstadoPedido == opciones[i]))
                        {
                            ckblEstadoPedido.Items[i].Selected = true;
                            ckblEstadoPedido.Items[i].Enabled = false;
                        }

                        if (pedido.EstadoPedido == opciones[i])
                            estadoOk = true;
                    }
                }

                //cargar cod de seguimiento
                if (pedido != null && !string.IsNullOrEmpty(pedido.CodSeguimiento))
                    txtCodSeguimientio.Text = pedido.CodSeguimiento.ToString();
                //cargar cod de transaccion
                if (pedido != null && !string.IsNullOrEmpty(pedido.CodTransaccion))
                    txtCódigoDeTransacción.Text = pedido.CodTransaccion.ToString();
                //cargar observaciones
                if (pedido != null && !string.IsNullOrEmpty(pedido.Observaciones))
                    txtObservaciones.Text = pedido.Observaciones.ToString();
            }
            else
            {
                bool estadoOk = false;
                for (int i = 0; i < opciones.Count; i++)
                {
                    if ((ObtenerUltimoEstadoSeleccionado() != opciones[i] && !estadoOk) || (ObtenerUltimoEstadoSeleccionado() == opciones[i]))
                    {
                        ckblEstadoPedido.Items[i].Selected = true;
                        ckblEstadoPedido.Items[i].Enabled = false;
                    }

                    if (ObtenerUltimoEstadoSeleccionado() == opciones[i])
                        estadoOk = true;
                }
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
            Pedido pedido = new Pedido();
            PedidoNegocio negocioP = new PedidoNegocio();
            int IdPedido = int.Parse(Request.QueryString["id"]);

            pedido.IdPedido = IdPedido;
            pedido.formaDePago.IdFormaDePago = int.Parse(ddlFormaDePago.SelectedValue);
            pedido.formaDeEnvio.IdFormaDeEnvio = int.Parse(ddlFormaDeEnvio.SelectedValue);
            pedido.CodTransaccion = txtCódigoDeTransacción.Text;
            pedido.CodSeguimiento = txtCodSeguimientio.Text;
            pedido.Observaciones = txtObservaciones.Text;

            pedido.EstadoPedido = ObtenerUltimoEstadoSeleccionado();

            if (negocioP.actualizarPedido(pedido))
                lblGuardarCambiosConExito.Visible = true;
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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerPedidos.aspx");
        }
    }
}