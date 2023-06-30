<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="AgregarCategoria.aspx.cs" Inherits="e_commerce.Pag_Admin.AgregarCategoria" %>

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
            </div>

            <asp:Button ID="btnModificar" runat="server" Text="Modificar" type="submit" class="btn btn-primary btn-lg" OnClick="btnModificar_Click" />

            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" type="submit" class="btn btn-primary btn-lg" OnClick="btnAgregar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" type="submit" class="btn btn-primary btn-lg" OnClick="btnCancelar_Click" />
        </div>
        <div class="col-6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtURLIMAGEN" class="form-label">URLImagen: </label>
                        <asp:TextBox runat="server" ID="txtURLIMAGEN" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtURLIMAGEN_TextChanged"/>
                        <asp:Button ID="btnAgregarImagen" runat="server" Text="+" OnClick="btnAgregarImagen_Click" />
                    </div>
                    <asp:Image ImageUrl="https://laboratoriodesuenos.com/wp-content/uploads/2020/02/default.jpg" ID="imgCategoria" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
