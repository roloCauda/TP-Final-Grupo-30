<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="e_commerce.Pag_Admin.AgregarMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--update panel-->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">

        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción: </label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                    ErrorMessage="*El campo Descripción es obligatorio" CssClass="text-danger" ValidationGroup="validacionGrupo"></asp:RequiredFieldValidator>
            </div>

            <asp:Button ID="btnModificar" runat="server" Text="Modificar" type="submit" class="btn btn-primary btn-lg" OnClick="btnModificar_Click" ValidationGroup="validacionGrupo"/>

            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" type="submit" class="btn btn-primary btn-lg" OnClick="btnAgregar_Click" ValidationGroup="validacionGrupo"/>

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" type="submit" class="btn btn-primary btn-lg" OnClick="btnCancelar_Click"/>
        </div>
        <div class="col-6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3 d-flex align-items-center">
                        <label for="txtURLIMAGEN" class="form-label">URLImagen: </label>
                        <asp:TextBox runat="server" ID="txtURLIMAGEN" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtURLIMAGEN_TextChanged"/>
                        <asp:RegularExpressionValidator ID="revURLIMAGEN" runat="server" ControlToValidate="txtURLIMAGEN"
                            ValidationExpression="^(http|https):\/\/[\w\-]+(\.[\w\-]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$"
                            ErrorMessage="*El campo URLIMAGEN no puede contener espacios en blanco" CssClass="text-danger"
                            ValidationGroup="validacionGrupo"></asp:RegularExpressionValidator>
                        <asp:Button ID="btnAgregarImagen" runat="server" Text="+" OnClick="btnAgregarImagen_Click" />
                    </div>
                    <asp:Image ID="imgMarca" runat="server" />
                    <asp:LinkButton ID="btnEliminarImagen" runat="server" OnClick="btnEliminarImagen_Click">Eliminar</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
