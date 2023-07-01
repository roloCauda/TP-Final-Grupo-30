<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="e_commerce.Pag_Cliente.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding: 25px; justify-content: center;">
        <div class="row" style="display: flex; justify-content: center;">

            <div class="col-md-7" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; margin-right: 15px;">
                <div class="row" style="justify-content: space-around; align-items: center; text-align: center; padding-top: 25px;">

                    <div class="col-lg-2 col-md-12">
                    </div>

                    <div class="col-lg-2 col-md-6">
                        <h4>Artículo</h4>
                    </div>

                    <div class="col-lg-2 col-md-6">
                        <h4>Cantidad</h4>
                    </div>

                    <div class="col-lg-2 col-md-6">
                        <h4>Precio</h4>
                    </div>

                    <div class="col-lg-2 col-md-6">
                    </div>

                </div>
                <asp:Repeater ID="repCarrito" runat="server">
                    <ItemTemplate>
                        <div class="row" style="justify-content: space-around; align-items: center; text-align: center;">
                            <div class="col-lg-2 col-md-2 custom-img">
                                <img src="<%#Eval("Articulo.ListaImagenes[0].ImagenURL") %>" class="card-img-top" alt="..." style="width: 100%; max-height: 100px;">
                            </div>
                            <div class="col-lg-2 col-md-2">
                                <asp:Label ID="lblNombreArticulo" runat="server" Text=""><%#Eval("Articulo.Nombre") %></asp:Label>
                            </div>
                            <div class="col-lg-2 col-md-2">
                                <div class="row" style="margin: 0px; justify-content: left; align-items: center;">
                                    <div class="col-lg-3 col-md-3">
                                        <asp:Button ID="btnQuitar" runat="server" Text="-" type="button" Style="font-size: 14px; width: 25px;" OnClick="btnQuitar_click" CommandName="Quitar" CommandArgument='<%# Eval("Articulo.IdArticulo") %>' />
                                    </div>
                                    <div class="col-lg-3 col-md-3" style="margin-right: 5px; margin-left: 5px;">
                                        <button type="button" style="font-size: 14px; width: 25px">
                                            <asp:Label ID="lblCantArtEnCarrito" runat="server" Text=""><%#Eval("Cantidad") %></asp:Label>
                                        </button>
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <asp:Button ID="btnAgregar" runat="server" Text="+" type="button" Style="font-size: 14px; width: 25px;" OnClick="btnAgregar_click" CommandName="Agregar" CommandArgument='<%# Eval("Articulo.IdArticulo") %>' />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2">
                                <asp:Label ID="lblPrcioArticulo" runat="server" Text="">$<%#Convert.ToDecimal(Eval("Articulo.Precio")) * Convert.ToInt32(Eval("Cantidad")) %></asp:Label>
                            </div>
                            <div class="col-lg-2 col-md-2">
                                <asp:Button ID="btnBorrar" runat="server" Text="Borrar" type="button" class="btn btn-primary" OnClick="btnBorrar_click" CommandName="Borrar" CommandArgument='<%# Eval("Articulo.IdArticulo") %>' />

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="display: flex; margin-top:auto; justify-content: right; text-align: center; align-items: center; padding-bottom: 25px;">
                    <asp:Label ID="lblVaciarCarrito" runat="server" Text="VACIAR CARRITO" Style="margin-right: 10px;"></asp:Label>
                    <asp:LinkButton ID="btnVaciarCarrito" runat="server" OnClick="btnVaciarCarrito_click" CssClass="btn btn-primary btn-sm">
                                        <span class="material-icons">delete</span>
                    </asp:LinkButton>
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
                                    <asp:Label ID="lblCantArtEnCarrito" runat="server" style="color:#3b71ca;" Text="">(<%#Eval("Cantidad") %>)</asp:Label>
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
                        <a href="FinalizarCompra.aspx" class="btn btn-primary btn-block">Finalizar Compra</a>
                    </div>
                    <div style="display: flex; justify-content: center; margin-bottom: 10px;">
                        <a href="Default.aspx" class="btn btn-primary btn-block">Elegir más artículos</a>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
