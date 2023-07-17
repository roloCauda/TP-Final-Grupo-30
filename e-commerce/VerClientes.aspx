<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerClientes.aspx.cs" Inherits="e_commerce.VerClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>CLIENTES</h1>

    <asp:GridView ID="dgvClientes" runat="server" DataKeyNames="IdUsuario"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvClientes_RowCommand"
        OnPageIndexChanging="dgvClientes_PageIndexChanging"
        AllowPaging="true" PageSize="5">

        <Columns>
            <asp:BoundField HeaderText="DNI" DataField="DNI" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="Telefono" DataField="Telefono" />
            <asp:BoundField HeaderText="Tipo de Acceso" DataField="TipoUsuario" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkEliminar" Text="❌" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkCmbiarAcceso" Text="✏️" CommandName="ModificarAcceso" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
</asp:Content>
