<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerCategorias.aspx.cs" Inherits="e_commerce.VerCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>CATEGORIAS</h1>
    
    <asp:GridView ID="dgvCategoria" runat="server" DataKeyNames="IdCategoria"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvCategoria_RowCommand"
        OnPageIndexChanging="dgvCategoria_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
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
