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
            <img src=""https://i.imgur.com/Rqj3HHN.png"" alt=""Logo"">
        </div>
        <div class=""message"">
            <p class=""thank-you"">¡Gracias por tu compra, {NombreCliente}!</p>
            <p class=""thank-you"">Orden #{NumeroPedido}</p>
            <p>Hemos recibido tu pedido y estamos procesándolo.</p>
        </div>
        <div class=""order-details"">
            <h2>Detalles del pedido:</h2>
            <ul>
                {DetallesProductos}
            </ul>
            <p>Total del pedido: <strong>${TotalCompra}</strong></p>
        </div>
        <div class='shipping-details'>
            <h2>Información de tu compra:</h2>
            <p>Método de envío: {MetodoEnvio}</p>
            <p>Estado del pedido: {EstadoPedido}</p>
            <p>Método de pago: {MetodoPago}</p>
            <p>Provincia: {ProvinciaPedido}</p>
            <p>Domicilio: {DomicilioPedido}</p>
        </div>
        <div>
            <h2>Datos de contacto:</h2>
            <p>Email: glafot@gmail.com</p>
            <p>Número: +54 9 1187654321</p>
        </div>
        <div class=""footer"">
            <p class=""thank-you"">¡Contactanos para finalizar tu compra!</p>
        </div>
    </div>
</body>
</html>
            ";

            // Reemplazar los marcadores de posición con los datos del cliente y la compra
            emailBody = emailBody.Replace("{NombreCliente}", usuario.Nombres.ToString());
            emailBody = emailBody.Replace("{MetodoEnvio}", pedido.FormaDeEnvio.Descripcion);
            emailBody = emailBody.Replace("{EstadoPedido}", pedido.EstadoPedido);
            emailBody = emailBody.Replace("{MetodoPago}", pedido.FormaDePago.Descripcion);
            emailBody = emailBody.Replace("{ProvinciaPedido}", pedido.Direccion.Provincia.Descripcion);
            emailBody = emailBody.Replace("{DomicilioPedido}", pedido.Direccion.Calle+ " " + pedido.Direccion.Numero + ", " + pedido.Direccion.Localidad.Descripcion + ", CP" + pedido.Direccion.CodPostal);
            emailBody = emailBody.Replace("{NumeroPedido}", pedido.IdPedido.ToString());

            decimal PrecioTotalPedido = 0;

            // Sumar los valores de la lista a PrecioTotal
            foreach (ArticulosXPedido articuloxPedido in pedido.ListaArtXPedido)
            {
                PrecioTotalPedido += articuloxPedido.PrecioTotal;
            }

            emailBody = emailBody.Replace("{TotalCompra}", PrecioTotalPedido.ToString());

            // Generar los detalles de la compra en formato HTML
            string detallesProductos = "";
            foreach (var articuloXPedido in pedido.ListaArtXPedido)
            {
                // Agregar el contenedor de cada artículo
                detallesProductos += "<div style='display: inline-block; margin-right: 20px;'>";

                // Agregar la imagen del artículo
                detallesProductos += $"<img src='{articuloXPedido.ImagenURL}' alt='{articuloXPedido.Nombre}' width='100' height='100' /><br><br>";

                // Agregar el nombre, la descripción, precio unitario y el precio total del artículo
                detallesProductos += $"<strong>{articuloXPedido.Nombre}</strong><br>";
                detallesProductos += $"{articuloXPedido.Descripcion}<br>";
                detallesProductos += $"<span>Cantidad: {articuloXPedido.Cantidad}<br>";
                detallesProductos += $"<span>Precio Unitario: ${articuloXPedido.PrecioUnitario}</span><br>";
                detallesProductos += $"<span>Precio Total: ${articuloXPedido.PrecioTotal}</span><br><br>";

                // Cerrar el contenedor del artículo
                detallesProductos += "</div>";
            }

            // Reemplazar {DetallesProductos} en el emailBody
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
                            <img src=""https://i.imgur.com/Rqj3HHN.png"" alt=""Logo"">
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
        public string obtenerCuerpoMailConDatosDeVenta(Pedido pedido, Usuario usuario)
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
            <img src=""https://i.imgur.com/Rqj3HHN.png"" alt=""Logo"">
        </div>
        <div class=""message"">
            <p class=""thank-you"">¡Tienes una nueva venta de: {NombreCliente}!</p>
            <p class=""thank-you"">Orden #{NumeroPedido}</p>
        </div>
        <div class=""order-details"">
            <h2>Detalles del pedido:</h2>
            <ul>
                {DetallesProductos}
            </ul>
            <p>Total del pedido: <strong>${TotalCompra}</strong></p>
        </div>
        <div class='shipping-details'>
            <h2>Información de la venta:</h2>
            <p>Método de envío: {MetodoEnvio}</p>
            <p>Estado del pedido: {EstadoPedido}</p>
            <p>Método de pago: {MetodoPago}</p>
            <p>Provincia: {ProvinciaPedido}</p>
            <p>Domicilio: {DomicilioPedido}</p>
        </div>
        <div class='client-details'>
            <h2>Información del comprador:</h2>
            <p>Nombre: {NombreCliente}</p>
            <p>Apellido: {Apellido}</p>
            <p>DNI: {DNI}</p>
            <p>Email: {Email}</p>
            <p>Teléfono: {Telefono}</p>
        </div>
        <div class=""footer"">
            <p>¡Podrás modificar el pedido desde tu cuenta de administrador!</p>
        </div>
    </div>
</body>
</html>
            ";
            // Reemplazar los marcadores de posición con los datos del cliente y la compra
            emailBody = emailBody.Replace("{NombreCliente}", usuario.Nombres);
            emailBody = emailBody.Replace("{MetodoEnvio}", pedido.FormaDeEnvio.Descripcion);
            emailBody = emailBody.Replace("{EstadoPedido}", pedido.EstadoPedido);
            emailBody = emailBody.Replace("{MetodoPago}", pedido.FormaDePago.Descripcion);
            emailBody = emailBody.Replace("{ProvinciaPedido}", pedido.Direccion.Provincia.Descripcion);
            emailBody = emailBody.Replace("{DomicilioPedido}", pedido.Direccion.Calle + " " + pedido.Direccion.Numero + ", " + pedido.Direccion.Localidad.Descripcion + ", CP" + pedido.Direccion.CodPostal);
            emailBody = emailBody.Replace("{NumeroPedido}", pedido.IdPedido.ToString());
            emailBody = emailBody.Replace("{Apellido}", usuario.Apellidos);
            emailBody = emailBody.Replace("{DNI}", usuario.DNI.ToString());
            emailBody = emailBody.Replace("{Telefono}", usuario.Telefono);
            emailBody = emailBody.Replace("{Email}", usuario.Email);

            decimal PrecioTotalPedido = 0;

            // Sumar los valores de la lista a PrecioTotal
            foreach (ArticulosXPedido articuloxPedido in pedido.ListaArtXPedido)
            {
                PrecioTotalPedido += articuloxPedido.PrecioTotal;
            }

            emailBody = emailBody.Replace("{TotalCompra}", PrecioTotalPedido.ToString());

            // Generar los detalles de la compra en formato HTML
            string detallesProductos = "";
            foreach (var articuloXPedido in pedido.ListaArtXPedido)
            {
                // Agregar el contenedor de cada artículo
                detallesProductos += "<div style='display: inline-block; margin-right: 20px;'>";

                // Agregar la imagen del artículo
                detallesProductos += $"<img src='{articuloXPedido.ImagenURL}' alt='{articuloXPedido.Nombre}' width='100' height='100' /><br><br>";

                // Agregar el nombre, la descripción, precio unitario y el precio total del artículo
                detallesProductos += $"<strong>{articuloXPedido.Nombre}</strong><br>";
                detallesProductos += $"{articuloXPedido.Descripcion}<br>";
                detallesProductos += $"<span>Cantidad: {articuloXPedido.Cantidad}<br>";
                detallesProductos += $"<span>Precio Unitario: ${articuloXPedido.PrecioUnitario}</span><br>";
                detallesProductos += $"<span>Precio Total: ${articuloXPedido.PrecioTotal}</span><br><br>";

                // Cerrar el contenedor del artículo
                detallesProductos += "</div>";
            }

            // Reemplazar {DetallesProductos} en el emailBody
            emailBody = emailBody.Replace("{DetallesProductos}", detallesProductos);

            return emailBody;
        }
    }
}
