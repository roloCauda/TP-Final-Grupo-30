<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="e_commerce.Registrarse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-6">

        <h3>Perfil</h3>
        <asp:Panel ID="pnl_Dni_Email" runat="server">
            <div class="formulario">
                <!-- Grupo: DNI -->
                <div>
                    <label class="formulario__label">DNI</label>
                    <asp:TextBox ID="txtDNI" type="text" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txtDNI"
                        ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionSeguir"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI"
                        ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                        CssClass="text-danger" ValidationGroup="validacionSeguir"></asp:RegularExpressionValidator>
                </div>
                <!-- Grupo: Seguir -->
                <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 50px;">
                    <asp:Button ID="btnSeguir" runat="server" Text="SEGUIR ->" class="formulario__btn"
                        CausesValidation="true" ValidationGroup="validacionSeguir" OnClick="btnSeguir_Click" />
                    <asp:Label ID="lblErrorRegistro" runat="server" Text="*DNI ya se encuentra registrado" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnl_Perfil_Direccion" runat="server">
            <div class="formulario">
                <!-- Nombres -->
                <div style="position: relative;">
                    <label class="formulario__label">Nombre</label>
                    <asp:TextBox ID="txtNombres" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ControlToValidate="txtNombres"
                        ErrorMessage="*El campo Nombres es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNombres" runat="server" ControlToValidate="txtNombres"
                        ValidationExpression="^[a-zA-Z\s\-']+$" ErrorMessage="*El campo Nombre no puede tener caracteres especiales" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Apellidos -->
                <div style="position: relative;">
                    <label class="formulario__label">Apellido</label>
                    <asp:TextBox ID="txtApellidos" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ControlToValidate="txtApellidos"
                        ErrorMessage="*El campo Apellidos es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revApellidos" runat="server" ControlToValidate="txtApellidos"
                        ValidationExpression="^[a-zA-Z\s\-']+$" ErrorMessage="*El campo Apellido no puede tener caracteres especiales" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Correo Electronico -->
                <div style="position: relative">
                    <label class="formulario__label">Correo Electrónico</label>
                    <asp:TextBox ID="txtEmail" type="email" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*El campo Email es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*Ingrese un email válido" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Teléfono -->
                <div style="position: relative">
                    <label class="formulario__label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" type="text" runat="server" class="formulario__input" placeholder></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                        ValidationExpression="^[0-9]+$" ErrorMessage="*El campo Teléfono solo puede contener números" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Calle -->
                <div style="position: relative;">
                    <label class="formulario__label">Calle</label>
                    <asp:TextBox ID="txtCalle" type="text" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCalle" runat="server" ControlToValidate="txtCalle"
                        ErrorMessage="*El campo Calle es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCalle" runat="server" ControlToValidate="txtCalle"
                        ValidationExpression="^[a-zA-Z0-9\s\-\']{1,100}$" ErrorMessage="*El campo Calle no puede tener caracteres especiales" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Numero -->
                <div style="position: relative;">
                    <label class="formulario__label">Número</label>
                    <asp:TextBox ID="txtNumeracion" type="text" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNumeracion" runat="server" ControlToValidate="txtNumeracion"
                        ErrorMessage="*El campo Número es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNumeracion" runat="server" ControlToValidate="txtNumeracion"
                        ValidationExpression="^[0-9\s]+$" ErrorMessage="*El campo Número solo admite números" CssClass="text-danger"
                        ValidationGroup="v"></asp:RegularExpressionValidator>
                </div>
                <!-- Piso -->
                <div style="position: relative;">
                    <label class="formulario__label">Piso</label>
                    <asp:TextBox ID="txtPiso" type="text" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revPiso" runat="server" ControlToValidate="txtPiso"
                        ValidationExpression="^[0-9\s]+$" ErrorMessage="*El campo Piso solo admite números" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Depto -->
                <div style="position: relative;">
                    <label class="formulario__label">Departamento</label>
                    <asp:TextBox ID="txtDepartamento" type="text" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revDepartamento" runat="server" ControlToValidate="txtDepartamento"
                        ValidationExpression="^[a-zA-Z0-9\s]+$" ErrorMessage="*El campo Departamento solo admite letras y números" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Codigo Postal -->
                <div style="position: relative;">
                    <label class="formulario__label">CP</label>
                    <asp:TextBox ID="txtCP" type="text" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCP" runat="server" ControlToValidate="txtCP"
                        ErrorMessage="*El campo CP es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCP" runat="server" ControlToValidate="txtCP"
                        ValidationExpression="^[a-zA-Z0-9\s]+$" ErrorMessage="*El campo CP solo admite letras y números" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                </div>
                <!-- Localidad -->
                <div style="position: relative;">
                    <label class="formulario__label" class="form-label">Localidad</label>
                    <asp:DropDownList ID="ddlLocalidad" CssClass="form-select" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDdlLocalidad" runat="server" ControlToValidate="ddlLocalidad"
                        ErrorMessage="*El campo Localidad es es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                </div>
                <!-- Provincia -->
                <div style="position: relative;">
                    <label class="formulario__label" class="form-label">Provincia</label>
                    <asp:DropDownList ID="ddlProvincia" CssClass="form-select" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDdlProvincia" runat="server" ControlToValidate="ddlProvincia"
                        ErrorMessage="*El campo Provincia es es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                </div>
                <!-- Contraseña-->
                <div>
                    <label class="formulario__label">Contraseña</label>
                    <asp:TextBox ID="txtContraseña" type="password" runat="server" class="formulario__input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPasswor" runat="server" ControlToValidate="txtContraseña"
                        ErrorMessage="*El campo Contraseña es obligatorio" CssClass="text-danger"
                        ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                </div>
            </div>
            <!-- Grupo: boton Registrarse -->
            <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 50px;">
                <asp:Button ID="btnRegistrarse" runat="server" Text="Guardar Cambios" class="formulario__btn"
                    CausesValidation="true" ValidationGroup="validacionGrupo" OnClick="btnRegistrarse_Click" />
            </div>

        </asp:Panel>
    </div>

</asp:Content>
