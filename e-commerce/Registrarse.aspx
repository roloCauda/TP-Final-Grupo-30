<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="e_commerce.Registrarse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-6">

        <h3>Perfil</h3>

        <div class="formulario" id="formularioPerfil">

            <!-- Grupo: DNI -->
            <div>
                <label class="formulario__label">DNI</label>
                <asp:TextBox ID="txtDNI" type="text" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txtDNI"
                    ErrorMessage="*El campo DNI es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoLOGIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI"
                    ValidationExpression="^[0-9]+$" ErrorMessage="*El campo DNI solo puede contener números"
                    CssClass="text-danger" ValidationGroup="validacionGrupoLOGIN"></asp:RegularExpressionValidator>
            </div>

            <!-- Grupo: Nombres -->
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

            <!-- Grupo: Apellidos -->
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

            <!-- Grupo: Correo Electronico -->
            <div style="position: relative">
                <label class="formulario__label">Correo Electrónico</label>
                <asp:TextBox ID="txtEmail" type="email" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*El campo Email es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*Ingrese un email válido" CssClass="text-danger"
                    ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
            </div>

            <!-- Grupo: Teléfono -->
            <div style="position: relative">
                <label class="formulario__label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" type="text" runat="server" class="formulario__input" placeholder></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="*El campo Teléfono es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                    ValidationExpression="^[0-9]+$" ErrorMessage="*El campo Teléfono solo puede contener números" CssClass="text-danger"
                    ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
            </div>

            <!-- Grupo: Contraseña-->
            <div>
                <label class="formulario__label">Contraseña</label>
                <asp:TextBox ID="txtPassword" type="password" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPasswor" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="*El campo Contraseña es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoLOGIN"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="formulario" id="formularioDireccion">

            <!-- Calle -->
            <div style="position: relative;">
                <label class="formulario__label">Calle</label>
                <asp:TextBox ID="txtCalle" type="text" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCalle" runat="server" ControlToValidate="txtCalle"
                    ErrorMessage="*El campo Calle es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCalle" runat="server" ControlToValidate="txtCalle"
                    ValidationExpression="^[a-zA-Z0-9\s\-\']{1,100}$" ErrorMessage="*El campo Calle no puede tener caracteres especiales" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RegularExpressionValidator>
            </div>
            <!-- Numero -->
            <div style="position: relative;">
                <label class="formulario__label">Número</label>
                <asp:TextBox ID="txtNumeracion" type="text" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumeracion" runat="server" ControlToValidate="txtNumeracion"
                    ErrorMessage="*El campo Número es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNumeracion" runat="server" ControlToValidate="txtNumeracion"
                    ValidationExpression="^[0-9\s]+$" ErrorMessage="*El campo Número solo admite números" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RegularExpressionValidator>
            </div>
            <!-- Piso -->
            <div style="position: relative;">
                <label class="formulario__label">Piso</label>
                <asp:TextBox ID="txtPiso" type="text" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revPiso" runat="server" ControlToValidate="txtPiso"
                    ValidationExpression="^[0-9\s]+$" ErrorMessage="*El campo Piso solo admite números" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RegularExpressionValidator>
            </div>
            <!-- Depto -->
            <div style="position: relative;">
                <label class="formulario__label">Departamento</label>
                <asp:TextBox ID="txtDepartamento" type="text" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revDepartamento" runat="server" ControlToValidate="txtDepartamento"
                    ValidationExpression="^[a-zA-Z0-9\s]+$" ErrorMessage="*El campo Departamento solo admite letras y números" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RegularExpressionValidator>
            </div>
            <!-- Codigo Postal -->
            <div style="position: relative;">
                <label class="formulario__label">CP</label>
                <asp:TextBox ID="txtCP" type="text" runat="server" class="formulario__input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCP" runat="server" ControlToValidate="txtCP"
                    ErrorMessage="*El campo CP es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCP" runat="server" ControlToValidate="txtCP"
                    ValidationExpression="^[a-zA-Z0-9\s]+$" ErrorMessage="*El campo CP solo admite letras y números" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RegularExpressionValidator>
            </div>
            <!-- Localidad -->
            <div style="position: relative;">
                <label class="formulario__label" class="form-label">Localidad</label>
                <asp:DropDownList ID="ddlLocalidad" CssClass="form-select" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDdlLocalidad" runat="server" ControlToValidate="ddlLocalidad"
                    ErrorMessage="*El campo Localidad es es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RequiredFieldValidator>
            </div>
            <!-- Provincia -->
            <div style="position: relative;">
                <label class="formulario__label" class="form-label">Provincia</label>
                <asp:DropDownList ID="ddlProvincia" CssClass="form-select" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDdlProvincia" runat="server" ControlToValidate="ddlProvincia"
                    ErrorMessage="*El campo Provincia es es obligatorio" CssClass="text-danger"
                    ValidationGroup="validacionGrupoDireccion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <!-- Grupo: boton Guardar Cambios -->
        <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top: 50px;">
            <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" class="formulario__btn"
                CausesValidation="true" ValidationGroup="validacionGrupoDireccion" OnClick="btnGuardarCambios_Click" />
        </div>
    </div>

</asp:Content>
