<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="e_commerce.Pag_Admin.AgregarProductos" %>

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
                <label for="txtCodigo" class="form-label">Código: </label>
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre: </label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio: </label>
                <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca: </label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción: </label>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3 d-flex align-items-center">
                        <label for="txtURLIMAGEN" class="form-label">URLImagen: </label>
                        <asp:TextBox runat="server" ID="txtURLIMAGEN" CssClass="form-control" />
                        <asp:Button ID="btnAgregarImagen" runat="server" Text="+" OnClick="btnAgregarImagen_Click"/>
                    </div>
                    <!--Inicio Carousel-->
                    <div id="carouselExample" class="carousel slide carousel-dark">
                        <div class="carousel-inner">
                            <asp:Repeater ID="rptItems" runat="server">
                                <ItemTemplate>
                                    <div class="carousel-item<%# Container.ItemIndex == 0 ? " active" : "" %>">
                                        <div class="d-flex justify-content-center align-items-center">
                                            <img src='<%# Eval("ImagenURL") %>' class="img-fluid" alt="...">
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                    <!--Fin Carousel-->
                    <asp:Button ID="btnEliminarImagen" runat="server" Text="Eliminar Imagen" OnClick="btnEliminarImagen_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <asp:Button ID="btnModificar" runat="server" Text="Modificar" type="submit" class="btn btn-primary btn-lg" OnClick="btnModificar_Click" />

    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" type="submit" class="btn btn-primary btn-lg" OnClick="btnAgregar_Click" />

    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" type="submit" class="btn btn-primary btn-lg" OnClick="btnCancelar_Click" />

</asp:Content>
