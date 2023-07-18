<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="VerEmpleados.aspx.cs" Inherits="e_commerce.VerEmpelados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>EMPLEADOS</h1>
    <div>
        <asp:RadioButtonList ID="rblOpciones" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblOpciones_SelectedIndexChanged"  >
            <asp:ListItem Text="Todos" Value="1"></asp:ListItem>
            <asp:ListItem Text="Activos" Value="2"></asp:ListItem>
            <asp:ListItem Text="Inactivos" Value="3"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <asp:GridView ID="dgvEmpleados" runat="server" DataKeyNames="IdUsuario"
        CssClass="table" AutoGenerateColumns="false"
        OnRowCommand="dgvEmpleados_RowCommand"
        OnPageIndexChanging="dgvEmpleados_PageIndexChanging"
        OnRowDataBound="dgvEmpleados_RowDataBound"
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
                    <asp:LinkButton runat="server" ID="lnkVer" Text="🔍" CommandName="Ver" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                   <asp:LinkButton runat="server" ID="lnkBaja" Text="❌" CommandName="Baja" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkAlta" Text="✅" CommandName="Alta" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>


</asp:Content>
