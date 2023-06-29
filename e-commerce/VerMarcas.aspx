<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerMarcas.aspx.cs" Inherits="e_commerce.VerMarcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="dgvMarca" runat="server" DataKeyNames="IdMarca"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvMarca_RowCommand"
        OnPageIndexChanging="dgvMarca_PageIndexChanging"
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
