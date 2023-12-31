﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class CompraRealizada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pedido pedido = (Pedido)Session["pedido"];

            // Se verifica que haya un usuario en session
            if (Session["usuario"] == null)
                Response.Redirect("Default.aspx");

            if (Session["pedido"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Se verifica que haya un pedido en session

                if (pedido.IdPedido == 0)
                    Response.Redirect("Default.aspx");
            }

            Carrito carrito = (Carrito)Session["ListaItems"];

            repResumen.DataSource = carrito.ListaItems;
            repResumen.DataBind();

            lblPrecio.Text = "$" + carrito.total.ToString();

            if (!IsPostBack)
            {
                // Verificar si los datos de sesión existen antes de asignarlos a los controles.
                if (Session["pedido"] != null)
                {
                    lblNumeroPedido.Text = "Número de Pedido: " + pedido.IdPedido;
                    lblFechaCompra.Text = "Fecha de Compra: " + pedido.Fecha.ToString();
                    lblMetodoEnvio.Text = "Método de Envío: " + pedido.FormaDeEnvio.Descripcion;
                    lblMetodoPago.Text = "Método de Pago: " + pedido.FormaDePago.Descripcion;
                }
                Session.Remove("pedido");
                Session.Remove("ListaItems");
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}