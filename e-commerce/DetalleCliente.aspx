<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="DetalleCliente.aspx.cs" Inherits="e_commerce.DetalleCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">

        <div class="col-6">
            <h3>Perfil</h3>
            <div class="mb-3">
                <label for="lblDNI" class="form-label">DNI: </label>
                <asp:Label ID="lblDNI" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblNombres" class="form-label">Nombres: </label>
                <asp:Label ID="lblNombres" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblApellidos" class="form-label">Apellido: </label>
                <asp:Label ID="lblApellidos" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblEmail" class="form-label">Email: </label>
                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblTelefono" class="form-label">Telefono: </label>
                <asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="ddlAcceso" class="form-label">Tipo de Acceso: </label>
                <asp:DropDownList ID="ddlAcceso" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar" type="submit" class="btn btn-primary btn-lg" OnClick="btnGuardarCambios_Click" ValidationGroup="validacionGrupo" />

                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" type="submit" class="btn btn-primary btn-lg" OnClick="btnEliminar_Click" ValidationGroup="validacionGrupo" />

                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" type="submit" class="btn btn-primary btn-lg" OnClick="btnCancelar_Click" />
            </div>
            <div>
                <asp:Label ID="lblGuardadoConExito" runat="server" Text="El usuario fue actualizado con éxito"></asp:Label>
            </div>
        </div>

        <div class="col-6">
            <h3>Direccion</h3>
            <div class="mb-3">
                <label for="lblCalle" class="form-label">Calle: </label>
                <asp:Label ID="lblCalle" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblNumero" class="form-label">Numero: </label>
                <asp:Label ID="lblNumero" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblPiso" class="form-label">Piso: </label>
                <asp:Label ID="lblPiso" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblDepartamento" class="form-label">Departamento: </label>
                <asp:Label ID="lblDepartamento" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblCodPostal" class="form-label">Código Postal: </label>
                <asp:Label ID="lblCodPostal" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblLocalidad" class="form-label">Localidad: </label>
                <asp:Label ID="lblLocalidad" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="lblProvincia" class="form-label">Provincia: </label>
                <asp:Label ID="lblProvincia" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
