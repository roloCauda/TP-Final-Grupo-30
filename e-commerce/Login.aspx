<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="e_commerce.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 10px;">
        <div class="col-md-6" style="padding-right: 70px; padding-left: 70px; background-color: #F8F5F4; color: black; border-radius: 50px; border: 30px solid white;">
            <div id="Login" style="padding: 35px;">
                <!-- Grupo: DNI -->
                <div>
                    <h2 style="text-align: center;" class="formulario__label">Login
                    </h2>
                </div>
                <div>
                    <label class="formulario__label">DNI</label>
                    <asp:TextBox ID="txtDNI" type="text" runat="server" class="formulario__input" OnTextChanged="TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txtDNI"
                        ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                        Style="position: absolute;"
                        ValidationGroup="validacionGrupoLOGIN"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI"
                        ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                        CssClass="text-danger" ValidationGroup="validacionGrupoLOGIN"></asp:RegularExpressionValidator>
                </div>
                <!-- Grupo: Contraseña -->
                <div>
                    <label class="formulario__label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" type="password" runat="server" class="formulario__input" OnTextChanged="TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPasswor" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="*El campo Contraseña es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupoLOGIN"></asp:RequiredFieldValidator>
                </div>
                <!-- Grupo: boton Ingresar -->
                <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 20px;">
                    <asp:Button ID="btn_Ingresar" runat="server" Text="INGRESAR" class="formulario__btn"
                        CausesValidation="true" ValidationGroup="validacionGrupoLOGIN" OnClick="btn_Ingresar_Click" />
                    <asp:Label ID="lblErrorLogin" runat="server" Text="*DNI/Contraseña incorrectos" CssClass="text-danger"></asp:Label>
                </div>
                <!-- Grupo: boton Recuperar Contraseña -->
                <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 40px;">
                    <asp:Button ID="btnRecuperarContraseña" runat="server" Text="Recuperar Contraseña" class="formulario__btn"
                        CausesValidation="true" ValidationGroup="validacionGrupoRecuperarContraseña" OnClick="btnRecuperarContraseña_Click" />
                    <asp:RequiredFieldValidator ID="rfvDNIRecuperarContraseña" runat="server" ControlToValidate="txtDNI"
                        ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupoRecuperarContraseña"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="col-md-6" style="padding-right: 70px; padding-left: 70px; background-color: #F8F5F4; color: black; border-radius: 50px; border: 30px solid white;">
            <div id="Registrarse" style="padding: 35px;">
                <div>
                    <h2 style="text-align: center;" class="formulario__label">Registrarse
                    </h2>
                </div>
                <!-- Grupo: DNI -->
                <div>
                    <label class="formulario__label">DNI</label>
                    <asp:TextBox ID="TextBox1" type="text" runat="server" class="formulario__input" OnTextChanged="TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                        Style="position: absolute;"
                        ValidationGroup="validacionGrupoCrearCuenta"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1"
                        ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                        CssClass="text-danger" ValidationGroup="validacionGrupoCrearCuenta"></asp:RegularExpressionValidator>
                </div>
                <!-- Grupo: Crear Cuenta -->
                <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 20px;">
                    <asp:Button ID="Button1" runat="server" Text="CREAR CUENTA" class="formulario__btn"
                        CausesValidation="true" ValidationGroup="validacionGrupoCrearCuenta" OnClick="btn_CrearCuenta_Click" />
                    <asp:Label ID="lblCuenta" runat="server" Text="*DNI/Contraseña incorrectos" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
