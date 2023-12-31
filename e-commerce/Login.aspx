﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="e_commerce.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="formulario__label" style="text-align: center;" padding-right: 70px; padding-left: 70px;>
            <asp:Label ID="lblAviso" runat="server" style="font-size: 20px; color: red;">Necesitas registarte o loguearte para continuar con tu compra</asp:Label>
        </div>
        <div class="col-md-5" style="padding-right: 70px; padding-left: 70px; background-color: #F8F5F4; color: black; border-radius: 50px; border: 3px solid #3b71ca;">
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
                    <asp:Button ID="btn_Ingresar" runat="server" Text="INGRESAR" class="formulario__btn" Style="width: 200px;"
                        CausesValidation="true" ValidationGroup="validacionGrupoLOGIN" OnClick="btn_Ingresar_Click" />
                    <asp:Label ID="lblErrorLogin" runat="server" Text="*DNI/Contraseña incorrectos" CssClass="text-danger"></asp:Label>
                </div>
                <!-- Grupo: boton Recuperar Contraseña -->
                <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 40px;">
                    <asp:Button ID="btnRecuperarContraseña" runat="server" Text="Recuperar Contraseña" class="formulario__btn" Style="width: 200px;"
                        CausesValidation="true" ValidationGroup="validacionGrupoRecuperarContraseña" OnClick="btnRecuperarContraseña_Click" />
                    <asp:RequiredFieldValidator ID="rfvDNIRecuperarContraseña" runat="server" ControlToValidate="txtDNI"
                        ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupoRecuperarContraseña"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblErrorRegistrado" runat="server" Text="*el DNI no se encuentra registrado" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-2">
        </div>
        <div class="col-md-5" style="padding-right: 70px; padding-left: 70px; background-color: #F8F5F4; color: black; border-radius: 50px; border: 3px solid #3b71ca;">
            <div id="Registrarse" style="padding: 35px;">
                <div>
                    <h2 style="text-align: center;" class="formulario__label">Registrarse
                    </h2>
                </div>
                <!-- Grupo: DNI -->
                <div>
                    <label class="formulario__label">DNI</label>
                    <asp:TextBox ID="txtDNICrearCuenta" type="text" runat="server" class="formulario__input" OnTextChanged="TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDNICrearCuenta"
                        ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                        Style="position: absolute;"
                        ValidationGroup="validacionGrupoCrearCuenta"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDNICrearCuenta"
                        ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                        CssClass="text-danger" ValidationGroup="validacionGrupoCrearCuenta"></asp:RegularExpressionValidator>
                </div>
                <!-- Grupo: Crear Cuenta -->
                <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 20px;">
                    <asp:Button ID="Button1" runat="server" Text="CREAR CUENTA" class="formulario__btn" Style="width: 200px;"
                        CausesValidation="true" ValidationGroup="validacionGrupoCrearCuenta" OnClick="btn_CrearCuenta_Click" />
                    <asp:Label ID="lblErrorRegistrarse" runat="server" Text="*El DNI ya se encuentra registrado" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
