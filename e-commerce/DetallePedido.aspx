<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="e_commerce.DetallePedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblNroPedido" runat="server"></asp:Label>

    <asp:GridView ID="dgvArtPorPedido" runat="server"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvArtPorPedido_RowCommand"
        OnPageIndexChanging="dgvArtPorPedido_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
            <asp:BoundField HeaderText="Precio Unit." DataField="PrecioUnitario" />
            <asp:BoundField HeaderText="Precio Total." DataField="PrecioTotal" />
            <asp:ImageField HeaderText="Imagen" DataImageUrlField="ImagenURL" ControlStyle-Width="100" ControlStyle-Height="100" />
        </Columns>
    </asp:GridView>
     <asp:Label ID="lblTotal" runat="server"></asp:Label>
</asp:Content>
