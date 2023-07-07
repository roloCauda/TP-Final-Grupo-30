<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cuenta_Usuario.aspx.cs" Inherits="e_commerce.Cuenta_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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



                        <!------------- REVISAR PARA ABAJO -------------->


                        <div class="formulario" id="formulario">

                            <!-- Grupo: Usuario -->
                            <div class="formulario__grupo" id="grupo__usuario">
                                <label for="usuario" class="formulario__label">Usuario</label>
                                <div class="formulario__grupo-input">
                                    <input type="text" class="formulario__input" name="usuario" id="usuario" placeholder="john123">
                                    <i class="formulario__validacion-estado fas fa-times-circle"></i>
                                </div>
                                <p class="formulario__input-error">El usuario tiene que ser de 4 a 16 dígitos y solo puede contener numeros, letras y guion bajo.</p>
                            </div>

                            <!-- Grupo: Nombre -->
                            <div class="formulario__grupo" id="grupo__nombre">
                                <label for="nombre" class="formulario__label">Nombre</label>
                                <div class="formulario__grupo-input">
                                    <input type="text" class="formulario__input" name="nombre" id="nombre" placeholder="John Doe">
                                    <i class="formulario__validacion-estado fas fa-times-circle"></i>
                                </div>
                                <p class="formulario__input-error">El usuario tiene que ser de 4 a 16 dígitos y solo puede contener numeros, letras y guion bajo.</p>
                            </div>

                            <!-- Grupo: Contraseña -->
                            <div class="formulario__grupo" id="grupo__password">
                                <label for="password" class="formulario__label">Contraseña</label>
                                <div class="formulario__grupo-input">
                                    <input type="password" class="formulario__input" name="password" id="password">
                                    <i class="formulario__validacion-estado fas fa-times-circle"></i>
                                </div>
                                <p class="formulario__input-error">La contraseña tiene que ser de 4 a 12 dígitos.</p>
                            </div>

                            <!-- Grupo: Contraseña 2 -->
                            <div class="formulario__grupo" id="grupo__password2">
                                <label for="password2" class="formulario__label">Repetir Contraseña</label>
                                <div class="formulario__grupo-input">
                                    <input type="password" class="formulario__input" name="password2" id="password2">
                                    <i class="formulario__validacion-estado fas fa-times-circle"></i>
                                </div>
                                <p class="formulario__input-error">Ambas contraseñas deben ser iguales.</p>
                            </div>

                            <!-- Grupo: Correo Electronico -->
                            <div class="formulario__grupo" id="grupo__correo">
                                <label for="correo" class="formulario__label">Correo Electrónico</label>
                                <div class="formulario__grupo-input">
                                    <input type="email" class="formulario__input" name="correo" id="correo" placeholder="correo@correo.com">
                                    <i class="formulario__validacion-estado fas fa-times-circle"></i>
                                </div>
                                <p class="formulario__input-error">El correo solo puede contener letras, numeros, puntos, guiones y guion bajo.</p>
                            </div>

                            <!-- Grupo: Teléfono -->
                            <div class="formulario__grupo" id="grupo__telefono">
                                <label for="telefono" class="formulario__label">Teléfono</label>
                                <div class="formulario__grupo-input">
                                    <input type="text" class="formulario__input" name="telefono" id="telefono" placeholder="4491234567">
                                    <i class="formulario__validacion-estado fas fa-times-circle"></i>
                                </div>
                                <p class="formulario__input-error">El telefono solo puede contener numeros y el maximo son 14 dígitos.</p>
                            </div>

                            <!-- Grupo: Terminos y Condiciones -->
                            <div class="formulario__grupo" id="grupo__terminos">
                                <label class="formulario__label">
                                    <input class="formulario__checkbox" type="checkbox" name="terminos" id="terminos">
                                    Acepto los Terminos y Condiciones
                                </label>
                            </div>

                            <!-- Grupo: Mensaje -->
                            <div class="formulario__mensaje" id="formulario__mensaje">
                                <p><i class="fas fa-exclamation-triangle"></i><b>Error:</b> Por favor rellena el formulario correctamente. </p>
                            </div>

                            <!-- Grupo: boton enviar -->
                            <div class="formulario__grupo formulario__grupo-btn-enviar">
                                <button type="submit" class="formulario__btn"="">Enviar</button>
                                <p class="formulario__mensaje-exito" id="formulario__mensaje-exito">Formulario enviado exitosamente!</p>
                            </div>
                        </div>

                    </asp:Panel>


                    <!------------- REVISAR PARA ARRIBA-------------->




                    <!-- Contenido de la dirección -->
                    <asp:Panel ID="pnl_Direccion" runat="server">
                        <h3>Dirección</h3>
                        <p>Este es el contenido de la dirección.</p>
                    </asp:Panel>

                    <!-- Contenido de la favoritos -->
                    <asp:Panel ID="pnl_Favoritos" runat="server">
                        <h3>Favoritos</h3>
                        <p>Este es el contenido de la favoritos.</p>
                    </asp:Panel>

                    <!-- Contenido de la pedidos -->
                    <asp:Panel ID="pnl_Pedidos" runat="server">
                        <h3>Pedidos</h3>

                      
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
