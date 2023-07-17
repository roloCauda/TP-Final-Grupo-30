<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerPedidos.aspx.cs" Inherits="e_commerce.VerPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Pedidos</h3>

    <asp:GridView ID="dgvPedidos" runat="server" DataKeyNames="IdPedido"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvPedidos_RowCommand"
        OnPageIndexChanging="dgvPedidos_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="N° de Pedido" DataField="IDPedido" />
            <asp:BoundField HeaderText="Forma de Pago" DataField="FormaDePago.Descripcion" />
            <asp:BoundField HeaderText="Fecha" DataField="Fecha" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkVer" Text="🔍" CommandName="VerPedido" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
