using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using dominio;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("mailsprueba123@gmail.com", "elsxvxpgqimpkplt");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("prueba@ecommerce.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }
        public void enviarCorreo()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string obtenerCuerpoMailConDatosDePedido(Pedido pedido, Usuario usuario)
        {

            // Plantilla HTML del correo electrónico
            string emailBody = @"
             <!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Confirmación de Compra</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
        .logo {
            text-align: center;
        }
        .logo img {
            max-width: 150px;
        }
        .message {
            margin-top: 20px;
        }
        .thank-you {
            text-align: center;
            font-size: 20px;
            color: #007bff;
        }
        .order-details {
            margin-top: 20px;
        }
        .product {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }
        .product img {
            max-width: 80px;
            margin-right: 10px;
        }
        .product-info {
            flex: 1;
        }
        .product-name {
            font-weight: bold;
        }
        .product-price {
            color: #007bff;
        }
        .footer {
            margin-top: 30px;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""logo"">
            <img src=""https://i.imgur.com/Rqj3HHN.png"" alt=""Logo de la tienda"">
        </div>
        <div class=""message"">
            <p class=""thank-you"">¡Gracias por tu compra, {NombreCliente}!</p>
            <p>Hemos recibido tu pedido y estamos procesándolo.</p>
        </div>
        <div class=""order-details"">
            <h2>Detalles del pedido:</h2>
            <ul>
                {DetallesProductos}
            </ul>
            <p>Total del pedido: <strong>${TotalCompra}</strong></p>
        </div>
        <div class=""footer"">
            <p>Si tienes alguna pregunta sobre tu pedido, por favor contactanos en glafot@gmail.com</p>
        </div>
    </div>
</body>
</html>
            ";

            // Reemplazar los marcadores de posición con los datos del cliente y la compra
            emailBody = emailBody.Replace("{NombreCliente}", usuario.Nombres); // Reemplazar por el nombre real del cliente
            emailBody = emailBody.Replace("{TotalCompra}", "10000"); // Reemplazar por el total de la compra real

            // Generar los detalles de la compra en formato HTML
            string detallesProductos = "";
            foreach (var articuloXPedido in pedido.ListaArtXPedido)
            {
                detallesProductos += $"<li>{articuloXPedido.Nombre} - ${articuloXPedido.PrecioTotal}</li>";
            }
            emailBody = emailBody.Replace("{DetallesProductos}", detallesProductos);

            return emailBody;
        }
        public string obtenerCuerpoMailConNuevaContraseña(string nuevaPass)
        {
            // Plantilla HTML del correo electrónico
            string emailBody = @"
             <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>Recuperación de Contraseña</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                        }
                        .container {
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            border: 1px solid #ccc;
                            border-radius: 5px;
                            background-color: #f9f9f9;
                        }
                        .logo {
                            text-align: center;
                        }
                        .logo img {
                            max-width: 150px;
                        }
                        .message {
                            text-align: center;
                            margin-top: 20px;
                        }
                        .thank-you {
                            text-align: center;
                            font-size: 20px;
                            color: #007bff;
                        }
                        .footer {
                            margin-top: 30px;
                            text-align: center;
                        }
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""logo"">
                            <img src=""https://i.imgur.com/Rqj3HHN.png"" alt=""Logo de la tienda"">
                        </div>
                        <div class=""message"">
                            <p class=""thank-you"">Aquí tienes tu nueva contraseña: <strong>{Contraseña}</strong></p>
                            <p>Hemos generado una nueva contraseña para que puedas ingresar.</p>
                            <p>Una vez hayas iniciado sesión vas a poder modificarla en tu perfil.</p>
                        </div>
                        <div class=""footer"">
                            <p>Si tienes alguna duda, por favor contactanos en glafot@gmail.com</p>
                        </div>
                    </div>
                </body>
                </html>
            ";

            emailBody = emailBody.Replace("{Contraseña}", nuevaPass);

            return emailBody;
        }
    }
}
