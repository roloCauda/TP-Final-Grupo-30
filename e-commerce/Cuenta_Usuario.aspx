<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta_Usuario.aspx.cs" Inherits="e_commerce.Cuenta_Usuario" %>

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
    <div>
        <label>Hola Pipu!</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-2">
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Perfil" runat="server" OnClick="lnk_Opcion_Click" CommandArgument="Perfil">Perfil</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Direccion" runat="server" OnClick="lnk_Opcion_Click" CommandArgument="Direccion">Direccion</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Contraseña" runat="server" OnClick="lnk_Opcion_Click" CommandArgument="Contraseña">Contraseña</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Favoritos" runat="server" OnClick="lnk_Opcion_Click" CommandArgument="Favoritos">Favoritos</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Pedidos" runat="server" OnClick="lnk_Opcion_Click" CommandArgument="Pedidos">Pedidos</asp:LinkButton>
                        </li>
                        <li class="list-group-item">
                            <asp:LinkButton ID="lnk_Salir" runat="server" OnClick="lnk_Salir_Click" CommandArgument="Salir">Salir</asp:LinkButton>
                        </li>
                    </ul>
                </div>

                <div class="col-md-6">

                    <!-- Contenido del perfil -->
                    <asp:Panel ID="pnl_Perfil" runat="server">

                        <h3>Perfil</h3>

                        <div class="formulario" id="formulario">

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
                            <div style="position: relative; margin-top:50px;">
                                <label class="formulario__label">Correo Electrónico</label>
                                <asp:TextBox ID="txtEmail" type="email" runat="server" class="formulario__input"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*El campo Email es obligatorio" CssClass="text-danger"
                                    ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*Ingrese un email válido" CssClass="text-danger"
                                    ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                            </div>


                            <!-- Grupo: Teléfono -->
                            <div style="position: relative; margin-top:50px;">
                                <label class="formulario__label">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" type="text" runat="server" class="formulario__input"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="*El campo Teléfono solo puede contener números" CssClass="text-danger"
                                    ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                            </div>

                            <!-- Grupo: boton Guardar Cambios -->
                            <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top:50px;"">
                                <asp:Button ID="btn_GuardarCambiosPerfil" runat="server" Text="Guardar Cambios" class="formulario__btn"
                                    CausesValidation="true" ValidationGroup="validacionGrupo" OnClick="btn_GuardarCambiosPerfil_Click" />
                            </div>
                        </div>

                    </asp:Panel>

                    <!-- Contenido de dirección -->
                    <asp:Panel ID="pnl_Direccion" runat="server">
                        <h3>Dirección</h3>

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
                                <asp:DropDownList ID="ddlProvincia" CssClass="form-select" runat="server" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDdlProvincia" runat="server" ControlToValidate="ddlProvincia"
                                    ErrorMessage="*El campo Provincia es es obligatorio" CssClass="text-danger"
                                    ValidationGroup="validacionGrupoDireccion"></asp:RequiredFieldValidator>
                            </div>
                            <!-- Grupo: boton Guardar Cambios -->
                            <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top:50px;">
                                <asp:Button ID="btnGuardarDireccion" runat="server" Text="Guardar Cambios" class="formulario__btn"
                                    CausesValidation="true" ValidationGroup="validacionGrupoDireccion" OnClick="btnGuardarDireccion_Click" />
                            </div>
                        </div>

                    </asp:Panel>

                    <!-- Contenido de Contraseña -->
                    <asp:Panel ID="pnl_Contraseña" runat="server">
                        <h3>Contraseña</h3>

                        <!-- Grupo: Contraseña Actual -->
                        <div style="position: relative;">
                            <label class="formulario__label">Contraseña Actual</label>
                            <asp:TextBox ID="txtPasswordActual" type="password" runat="server" class="formulario__input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPasswor" runat="server" ControlToValidate="txtPasswordActual"
                                ErrorMessage="*El campo Contraseña es obligatorio" CssClass="text-danger"
                                ValidationGroup="validacionGrupoContraseña"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Grupo: Contraseña Nueva -->
                        <div style="position: relative; margin-top:50px;">
                            <label class="formulario__label">Contraseña Nueva</label>
                            <asp:TextBox ID="txtPasswordNueva" type="password" runat="server" class="formulario__input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPasswordNueva" runat="server" ControlToValidate="txtPasswordNueva"
                                ErrorMessage="*El campo Contraseña Nueva es obligatorio" CssClass="text-danger"
                                ValidationGroup="validacionGrupoContraseña"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Grupo: Repetir contraseña Nueva -->
                        <div style="position: relative; margin-top:50px;">
                            <label class="formulario__label">Repetir Contraseña Nueva</label>
                            <asp:TextBox ID="txtPasswordNueva2" type="password" runat="server" class="formulario__input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPasswordNueva2" runat="server" ControlToValidate="txtPasswordNueva2"
                                ErrorMessage="*El campo Repetir Contraseña Nueva es obligatorio" CssClass="text-danger"
                                ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPasswordNueva" runat="server" ControlToCompare="txtPasswordNueva"
                                ControlToValidate="txtPasswordNueva2" Operator="Equal"
                                ErrorMessage="*Las contraseñas no coinciden" CssClass="text-danger"
                                ValidationGroup="validacionGrupoContraseña"></asp:CompareValidator>
                        </div>

                        <!-- Grupo: boton Guardar Contraseña -->
                        <div class="formulario__grupo formulario__grupo-btn-enviar" style="margin-top:50px;">
                            <asp:Button ID="btn_GuardarContraseña" runat="server" Text="Guardar Cambios" class="formulario__btn"
                                CausesValidation="true" ValidationGroup="validacionGrupoContraseña" OnClick="btn_GuardarContraseña_Click" />
                        </div>

                    </asp:Panel>

                    <!-- Contenido de favoritos -->
                    <asp:Panel ID="pnl_Favoritos" runat="server">
                        <h3>Favoritos</h3>
                        <!-- ACA VA UNA GRIDVIEW con botones de accion para eliminar-->
                    </asp:Panel>

                    <!-- Contenido de pedidos -->
                    <asp:Panel ID="pnl_Pedidos" runat="server">
                        <h3>Pedidos</h3>
                        <!-- ACA VA UNA GRIDVIEW ------------hace falta otra pag para ver el detalle??-->
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
