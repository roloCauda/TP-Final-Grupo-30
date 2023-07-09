<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FinDeCompra.aspx.cs" Inherits="e_commerce.Pag_Cliente.FinDeCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .text-danger {
            position: absolute;
            top: 100%;
            left: 0;
        }
    </style>

    <div class="container" style="padding: 25px; justify-content: center;">
        <asp:UpdatePanel ID="UpdatePanelPasos" runat="server">
            <ContentTemplate>

                <div class="row" style="display: flex; justify-content: center;">
                    <div class="col-md-7" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; margin-right: 15px; padding: 15px;">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="nav nav-pills">
                                    <li class="nav-item">
                                        <asp:Label ID="lblDatos" runat="server" CssClass="nav-link" Text="1.Datos Personales"></asp:Label>
                                    </li>
                                    <li class="nav-item">
                                        <asp:Label ID="lblEnvio" runat="server" CssClass="nav-link" Text="2.Datos de Envío"></asp:Label>
                                    </li>
                                    <li class="nav-item">
                                        <asp:Label ID="lblPagos" runat="server" CssClass="nav-link" Text="3.Formas de Pago"></asp:Label>
                                    </li>
                                </ul>
                            </div>

                        </div>
                        <!-- Datos Personales -->
                        <asp:Panel ID="pnl_Datos" runat="server">
                            <div class="row" style="margin-top: 15px;">
                                <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left; position: relative;">
                                    <label class="formulario__label">Nombre:</label>
                                    <asp:TextBox ID="txtNombre" runat="server" class="formulario__input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*El campo Nombre es obligatorio"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre" ValidationExpression="^[A-Za-z\s]+$" ErrorMessage="*El campo Nombre no puede contener números"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left; position: relative;">
                                    <label class="formulario__label">Apellido:</label>
                                    <asp:TextBox ID="txtApellido" runat="server" class="formulario__input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="*El campo Apellido es obligatorio"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtApellido" ValidationExpression="^[A-Za-z\s]+$" ErrorMessage="*El campo Apellido no puede contener números"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div style="display: flex; flex-direction: column; text-align: left; margin-top: 25px; position: relative;">
                                <label class="formulario__label">Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server" class="formulario__input"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*El campo Email es obligatorio"
                                    ValidationGroup="validacionGrupoDatos" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*Ingrese un email válido"
                                    ValidationGroup="validacionGrupoDatos" CssClass="text-danger"></asp:RegularExpressionValidator>
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left; position: relative;">
                                    <label class="formulario__label">DNI:</label>
                                    <asp:TextBox ID="txtDNI" runat="server" class="formulario__input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txtDNI" ErrorMessage="*El campo DNI es obligatorio"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI" ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6" style="display: flex; flex-direction: column; text-align: left; position: relative;">
                                    <label class="formulario__label">Teléfono:</label>
                                    <asp:TextBox ID="txtTelefono" runat="server" class="formulario__input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="*El campo Teléfono es obligatorio"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono" ValidationExpression="^[0-9]+$" ErrorMessage="*El campo Teléfono solo puede contener números"
                                        ValidationGroup="validacionGrupoDatos" CssClass="text-danger" Style="padding-left: 12px;"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnContinuarDatos" runat="server" Text="Continuar" OnClick="btnContinuar_Click" CommandArgument="Envio" Style="margin-top: 70px;" class="formulario__btn"
                                    CausesValidation="true" ValidationGroup="validacionGrupoDatos" />
                            </div>

                        </asp:Panel>
                        <!-- Datos de Envio -->
                        <asp:Panel ID="pnl_Envio" runat="server">
                            <h2>Envios</h2>
                            <div class="row" style="margin-top: 70px;">
                                <div class="col-md-6">
                                    <asp:Button ID="btnVolverEnvio" runat="server" Text="Volver" OnClick="btnContinuar_Click" CommandArgument="Datos" CssClass="formulario__btn" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnContinuarEnvio" runat="server" Text="Continuar" OnClick="btnContinuar_Click" CommandArgument="Pagos" CssClass="formulario__btn"/>
                                </div>
                            </div>
                        </asp:Panel>

                        <!-- Formas de Pago -->
                        <asp:Panel ID="pnl_Pagos" runat="server">
                            <h2>Pagos</h2>
                            <div>
                                <asp:Button ID="btnVolverPagos" runat="server" Text="Volver" OnClick="btnContinuar_Click" CommandArgument="Envio" Style="margin-top: 70px;" class="formulario__btn"/>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-4" style="display: flex; flex-direction: column; border: 3px solid #3b71ca; border-radius: 15px; height: 100vh; text-align: center; padding-top: 15px;">
                        <div>
                            <h2>Resumen de la Compra</h2>
                        </div>
                        <div>
                            <asp:Repeater ID="repFinalizar" runat="server">
                                <ItemTemplate>
                                    <div class="row" style="justify-content: space-around; align-items: center; padding-left: 15px; padding-right: 15px;">

                                        <div class="col-md-6" style="text-align: left; margin-top: 20px;">
                                            <asp:Label ID="lblNombreArticulo" runat="server" Text=""><%#Eval("Articulo.Nombre") %></asp:Label>
                                            <asp:Label ID="lblCantArtEnCarrito" runat="server" Text="" Style="color: #3b71ca;">(<%#Eval("Cantidad") %>)</asp:Label>
                                        </div>

                                        <div class="col-md-6" style="text-align: right; margin-top: 20px;">
                                            <asp:Label ID="lblPrcioArticulo" runat="server" Text="">$<%#Convert.ToDecimal(Eval("Articulo.Precio")) * Convert.ToInt32(Eval("Cantidad")) %></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div style="margin-top: auto; display: flex; flex-direction: column;">

                            <div style="display: flex; justify-content: space-between; padding-bottom: 25px;">
                                <asp:Label ID="lblTextoPrecioTotal" runat="server" Text="TOTAL"></asp:Label>
                                <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="display: flex; justify-content: center; margin-bottom: 10px;">
                                <a href="Carrito.aspx" class="btn btn-primary btn-block">Modificar Compra</a>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
