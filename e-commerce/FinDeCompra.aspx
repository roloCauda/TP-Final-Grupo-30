<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FinDeCompra.aspx.cs" Inherits="e_commerce.Pag_Cliente.FinDeCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding: 25px; justify-content: center;">
        <div class="row" style="display: flex; justify-content: center;">

            <div class="col-md-7" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; margin-right: 15px; padding:15px;">
                <h2 style="text-align: left;">Datos Personales
                </h2>
                <div class="row " style="margin-top:15px;">

                    <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left;">
                        <label for="lblNombre">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>

                    </div>

                    <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left;">
                        <label for="lblApellido">Apellido:</label>
                        <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div style="display: flex; flex-direction: column; text-align: left; margin-top:25px;">
                    <label for="lblEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>

                <div class="row " style="margin-top:25px;">

                    <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left;">
                        <label for="lblDNI">DNI:</label>
                        <asp:TextBox ID="txtDNI" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-6">
                        <div class="row">

                            <div class="col-md-3" style="text-align:left;">
                                <label for="lblCodigoArea">Área</label>
                                <asp:TextBox ID="txtCodigoArea" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="text-align:left;">
                                <label for="lblTelefono">Teléfono:</label>
                                <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                            </div>

                        </div>
                    </div>

                </div>
            </div>

            <div class="col-md-4" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; padding-top: 15px;">
                <div>
                    <h2>Resumen de la Compra</h2>
                </div>
                <div>
                    <asp:Repeater ID="repFinalizar" runat="server">
                        <ItemTemplate>
                            <div class="row" style="justify-content: space-around; align-items: center; padding-left: 15px; padding-right: 15px;">

                                <div class="col-md-6" style="text-align: left; margin-top: 20px;">
                                    <asp:Label ID="lblNombreArticulo" runat="server" Text=""><%#Eval("Articulo.Nombre") %></asp:Label>
                                    <asp:Label ID="lblCantArtEnCarrito" runat="server" Text="" Style="color: #3b71ca;">(<%#Eval("Cantidad") %>)</asp:Label>
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
                    <div style="display: flex; justify-content: center; margin-bottom: 10px;">
                        <asp:Button ID="btnComprar" runat="server" Text="Confirmar" OnClick="Comprar_Click" class="btn btn-primary btn-block" />
                    </div>
                    <div style="display: flex; justify-content: center; margin-bottom: 10px;">
                        <a href="Carrito.aspx" class="btn btn-primary btn-block">Modificar Compra</a>
                    </div>
                </div>
            </div>



        </div>
    </div>
</asp:Content>
