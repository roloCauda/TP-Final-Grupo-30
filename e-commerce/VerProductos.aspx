<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerProductos.aspx.cs" Inherits="e_commerce.VerProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>PRODUCTOS</h1>

    <asp:GridView ID="dgvArticulo" runat="server" DataKeyNames="IdArticulo"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvArticulo_RowCommand"
        RowDataBound="dgvArticulo_RowDataBound"
        OnPageIndexChanging="dgvArticulo_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marcas" DataField="IdMarca.Descripcion" />
            <asp:BoundField HeaderText="Categorias" DataField="IdCategoria.Descripcion" />
            <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkVer" Text="🔍" CommandName="Ver" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lnkModificar" Text="✏️" CommandName="Modificar" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lnkEliminar" Text="❌" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
