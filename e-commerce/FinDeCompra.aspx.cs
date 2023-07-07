using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce.Pag_Cliente
{
    public partial class FinDeCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            TextBox txtFiltro = Master.FindControl("txtFiltro") as TextBox;

            if (txtFiltro != null && !string.IsNullOrEmpty(txtFiltro.Text))
            {
                Response.Redirect("Default.aspx?txtFiltro=" + Server.UrlEncode(txtFiltro.Text));
            }

            lblPrecio.Text = "$" + carrito.total.ToString();

            repFinalizar.DataSource = carrito.ListaItems;
            repFinalizar.DataBind();
        }


        protected void Comprar_Click(object sender, EventArgs e)
        {
            dominio.Carrito carrito = (dominio.Carrito)Session["ListaItems"];

            // Validar que los campos necesarios esten completos
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                // mensaje de error
                string errorScript = @"<script>alert('Por favor, completa todos los campos.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "MostrarError", errorScript);
                return;
            }

            // Valida que el total sea mayor a 0
            if (carrito.total <= 0)
            {
                // mensaje de error
                string errorScript = @"<script>alert('El carrito está vacío.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "MostrarError", errorScript);
                return;
            }

            string nombre = txtNombre.Text + ' ' + txtApellido.Text;
            string nroPedido = "Número de pedido: 15874";
            string totalPedido = "Monto $" + carrito.total;
            string envioEmail = "Hemos enviado la confirmación a tu correo electrónico: " + txtEmail.Text;

            string script = $@"
            <script>
                var mensaje = 'Gracias por tu compra, {nombre}\n{nroPedido}\n{totalPedido}\n{envioEmail}';
                if (confirm(mensaje)) {{
                    window.location.href = 'Default.aspx';
                }}
            </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "MostrarMensaje", script);

            carrito.ListaItems.Clear();
            carrito.total = 0;
            Session["ListaItems"] = carrito;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

    }
}