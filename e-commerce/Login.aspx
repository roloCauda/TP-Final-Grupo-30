<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="e_commerce.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>LOGIN</h3>

    <div class="formulario" id="formulario">
        <!-- Grupo: DNI -->
        <div>
            <label class="formulario__label">DNI</label>
            <asp:TextBox ID="txtDNI" type="text" runat="server" class="formulario__input" OnTextChanged="TextChanged" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txtDNI"
                ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                ValidationGroup="validacionGrupoLOGIN"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI"
                ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                CssClass="text-danger" ValidationGroup="validacionGrupoLOGIN"></asp:RegularExpressionValidator>
        </div>
        <!-- Grupo: Contraseña Actual -->
        <div>
            <label class="formulario__label">Contraseña</label>
            <asp:TextBox ID="txtPassword" type="password" runat="server" class="formulario__input" OnTextChanged="TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPasswor" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="*El campo Contraseña es obligatorio" CssClass="text-danger"
                ValidationGroup="validacionGrupoLOGIN"></asp:RequiredFieldValidator>
        </div>
        <!-- Grupo: boton Ingresar -->
        <div class="formulario__grupo formulario__grupo-btn-enviar">
            <asp:Button ID="btn_Ingresar" runat="server" Text="INGRESAR" class="formulario__btn"
                CausesValidation="true" ValidationGroup="validacionGrupoLOGIN" OnClick="btn_Ingresar_Click"/>
            <asp:Label ID="lblErrorLogin" runat="server" Text="*DNI/Contraseña incorrectos" CssClass="text-danger"></asp:Label>
        </div>
        <!-- Grupo: boton Recuperar Contraseña -->
        <div class="formulario__grupo formulario__grupo-btn-enviar">
            <asp:Button ID="btnRecuperarContraseña" runat="server" Text="Recuperar Contraseña" class="formulario__btn"
                CausesValidation="true" ValidationGroup="validacionGrupoRecuperarContraseña" OnClick="btnRecuperarContraseña_Click" />
            <asp:RequiredFieldValidator ID="rfvDNIRecuperarContraseña" runat="server" ControlToValidate="txtDNI"
                ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                ValidationGroup="validacionGrupoRecuperarContraseña"></asp:RequiredFieldValidator>
        </div>
    </div>

</asp:Content>
