<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta_Usuario.aspx.cs" Inherits="e_commerce.Cuenta_Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <label>Hola Pipu!</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-2">
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Perfil" runat="server" OnClick="lnk_Perfil_Click">Perfil</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Direccion" runat="server" OnClick="lnk_Direccion_Click">Direccion</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Contraseña" runat="server" OnClick="lnk_Contraseña_Click">Contraseña</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Favoritos" runat="server" OnClick="lnk_Favoritos_Click">Favoritos</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Pedidos" runat="server" OnClick="lnk_Pedidos_Click">Pedidos</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Salir" runat="server" OnClick="lnk_Salir_Click">Salir</asp:LinkButton>
                        </li>
                    </ul>
                </div>

                <div class="col-md-10">
                    <!-- Contenido del perfil -->
                    <asp:Panel ID="pnl_Perfil" runat="server">
                        <h3>Perfil</h3>
                        <p>Este es el contenido del perfil.</p>
                    </asp:Panel>

                    <!-- Contenido de la dirección -->
                    <asp:Panel ID="pnl_Direccion" runat="server">
                        <h3>Dirección</h3>
                        <p>Este es el contenido de la dirección.</p>
                    </asp:Panel>

                    <!-- Contenido de la contraseña -->
                    <asp:Panel ID="pnl_Contrasena" runat="server">
                        <h3>Contraseña</h3>
                        <p>Este es el contenido de la contraseña.</p>
                    </asp:Panel>

                    <!-- Contenido de la favoritos -->
                    <asp:Panel ID="pnl_Favoritos" runat="server">
                        <h3>Favoritos</h3>
                        <p>Este es el contenido de la favoritos.</p>
                    </asp:Panel>

                    <!-- Contenido de la pedidos -->
                    <asp:Panel ID="pnl_Pedidos" runat="server">
                        <h3>Pedidos</h3>
                        <p>Este es el contenido de la pedidos.</p>
                    </asp:Panel>

                    <!-- Contenido de la salir -->
                    <asp:Panel ID="pnl_Salir" runat="server">
                        <h3>Salir</h3>
                        <p>Este es el contenido de la salir.</p>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
