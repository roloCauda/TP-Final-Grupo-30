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
                        
                        <div class="row mb-4">
                            <!-- Nombres-->
                            <div class="col">
                                <div class="form-floating-label-transform">
                                    <input type="text" id="ip_Nombres" class="form-control" />
                                    <label class="form-label" for="ip_Nombres">Nombres</label>
                                </div>
                            </div>
                            <!-- Apellido-->
                            <div class="col">
                                <div class="form-floating-label-transform">
                                    <input type="text" id="ip_Apellidos" class="form-control" />
                                    <label class="form-label" for="ip_Apellidos">Apellidos</label>
                                </div>
                            </div>
                        </div>
                        <!-- Email-->
                        <div class="form-floating-label-transform mb-4">
                            <input type="email" id="ip_email" class="form-control" />
                            <label class="form-label" for="ip_email">Email address</label>
                        </div>
                        <!-- Guardar Cambios-->
                        <button type="submit" ID="btn_GuardarCambios" class="btn btn-primary btn-block mb-2">Guardar Cambios</button>
                        
                    </asp:Panel>

                    <!-- Contenido de la dirección -->
                    <asp:Panel ID="pnl_Direccion" runat="server">
                        <h3>Dirección</h3>
                        <p>Este es el contenido de la dirección.</p>
                    </asp:Panel>

                    <!-- Contenido de la contraseña -->
                    <asp:Panel ID="pnl_Contrasena" runat="server">
                        <h3>Contraseña</h3>
                        <!-- Password input -->
                        <div class="form-floating-label-transform mb-4">
                            <input type="password" id="in_ContraseñaActual" class="form-control" />
                            <label class="form-label" for="in_ContraseñaActual">Contraseña Actual</label>
                        </div>
                        <div class="form-floating-label-transform mb-4">
                            <input type="password" id="in_ContraseñaNueva" class="form-control" />
                            <label class="form-label" for="in_ContraseñaNueva">Nueva contraseña</label>
                        </div>
                        <div class="form-floating-label-transform mb-4">
                            <input type="password" id="in_RepetirContraseñaNueva" class="form-control" />
                            <label class="form-label" for="in_RepetirContraseñaNueva">Repetir contraseña</label>
                        </div>
                        <!-- Guardar Cambios-->
                        <button type="submit" ID="btn_GuardarCambiosContraseña" class="btn btn-primary btn-block mb-2">Guardar Contraseña</button>
                    </asp:Panel>

                    <!-- Contenido de la favoritos -->
                    <asp:Panel ID="pnl_Favoritos" runat="server">
                        <h3>Favoritos</h3>
                        <p>Este es el contenido de la favoritos.</p>
                    </asp:Panel>

                    <!-- Contenido de la pedidos -->
                    <asp:Panel ID="pnl_Pedidos" runat="server">
                        <h3>Pedidos</h3>
                        <p>Este es el contenido de la pedidos.</p>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
