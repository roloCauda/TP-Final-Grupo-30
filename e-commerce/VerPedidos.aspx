<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerPedidos.aspx.cs" Inherits="e_commerce.VerPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Pedidos</h3>
    <div>
        <asp:RadioButtonList ID="rblOpciones" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblOpciones_SelectedIndexChanged">
            <asp:ListItem Text="Todos" Value="1"></asp:ListItem>
            <asp:ListItem Text="Pendiente" Value="2"></asp:ListItem>
            <asp:ListItem Text="En proceso" Value="3"></asp:ListItem>
            <asp:ListItem Text="Enviado" Value="4"></asp:ListItem>
            <asp:ListItem Text="Entregado" Value="5"></asp:ListItem>
            <asp:ListItem Text="Cancelado" Value="6"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
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
    </div>

</asp:Content>
