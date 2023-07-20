<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CompraRealizada.aspx.cs" Inherits="e_commerce.CompraRealizada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding: 25px; justify-content: center;">
        <div class="row" style="display: flex; justify-content: center;">

            <div class="col-md-7" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; margin-right: 15px;">
                <div style="margin-top: 50px;">
                    <img src="https://i.imgur.com/Rqj3HHN.png" alt="Logo" style="width: 150px; height: 150px;" />
                </div>
                <div style="margin-top: 30px;">
                    <h2>¡Compra Realizada con Éxito!</h2>
                    <p>Gracias por tu compra. Hemos recibido tu pedido y estaremos procesándolo pronto.</p>
                    <p>Revisá tu casilla de correo para más información!</p>
                </div>
                <div style="margin-top: auto; display: flex; flex-direction: column; margin-bottom: 30px;">
                    <div>
                        <h4>Detalles de la Compra:</h4>
                    </div>
                    <div>
                        <asp:Label ID="lblNumeroPedido" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblFechaCompra" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblMetodoEnvio" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblMetodoPago" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div style="padding-bottom: 100px;">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver a la Página Principal" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
                </div>

            </div>

            <div class="col-md-4" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; padding-top: 15px;">
                <div>
                    <h2>Resumen de la Compra</h2>
                </div>
                <div>
                    <asp:Repeater ID="repResumen" runat="server">
                        <ItemTemplate>
                            <div class="row" style="justify-content: space-around; align-items: center; padding-left: 15px; padding-right: 15px;">

                                <div class="col-md-6" style="text-align: left; margin-top: 20px;">
                                    <asp:Label ID="lblNombreArticulo" runat="server" Text=""><%#Eval("Articulo.Nombre") %></asp:Label>
                                    <asp:Label ID="lblCantArtEnCarrito" runat="server" Style="color: #3b71ca;" Text="">(<%#Eval("Cantidad") %>)</asp:Label>
                                </div>

                                <div class="col-md-6" style="text-align: right; margin-top: 20px;">
                                    <asp:Label ID="lblPrcioArticulo" runat="server" Text="">$<%#Convert.ToDecimal(Eval("Articulo.Precio")) * Convert.ToInt32(Eval("Cantidad")) %></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div style="margin-top: auto; display: flex; flex-direction: column;">

                    <div style="display: flex; justify-content: space-between; padding-bottom: 25px;">
                        <asp:Label ID="lblTextoPrecioTotal" runat="server" Text="TOTAL"></asp:Label>
                        <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
