<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="e_commerce.DetallePedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblNroPedido" runat="server"></asp:Label>

    <asp:GridView ID="dgvArtPorPedido" runat="server"
        CssClass="table" AutoGenerateColumns="false"
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
    <div>
        <div>
            <label>Forma de Envio:</label>
            <asp:DropDownList ID="ddlFormaDeEnvio" runat="server"></asp:DropDownList>
        </div>
        <div>
            <label>Código de seguimiento</label>
            <asp:TextBox ID="txtCodSeguimientio" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Forma de Pago</label>
            <asp:DropDownList ID="ddlFormaDePago" runat="server"></asp:DropDownList>
            <asp:TextBox ID="txtCódigoDeTransacción" runat="server"></asp:TextBox>
        </div>
        <div class="checkbox-list-container">
            <label>Estado del Pedido</label>
            <asp:CheckBoxList ID="ckblEstadoPedido" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ckblEstadoPedido_SelectedIndexChanged" ></asp:CheckBoxList>
        </div>
        <div>
            <label>Observaciones</label>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtObservaciones" CssClass="form-control" />
        </div>
        <div>
            <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" type="submit" class="btn btn-primary btn-lg" OnClick="btnGuardarCambios_Click" ValidationGroup="validacionGrupo"/>
            <asp:Button ID="btnVolver" runat="server" Text="Volver" type="submit" class="btn btn-primary btn-lg" OnClick="btnVolver_Click"/>
        </div>
        <div>
            <asp:Label ID="lblGuardarCambiosConExito" runat="server" Text="Cambios guardados con éxito" Visible="false" ></asp:Label>
        </div>
    </div>
</asp:Content>
