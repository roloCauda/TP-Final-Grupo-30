<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="e_commerce.Pag_Cliente.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-detalle" style="height: 70vh;">
        <div class="row">
            <!--Columna 1-->
            <div class="col-md-6 carousel-estilo" style="background-color: white;">

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


            </div>
            <!--Columna 2-->
            <div class="col-md-6 d-flex flex-column align-items-center justify-content-center" style="text-align: center;">
                <div>
                    <h2 style="margin-bottom: 20px;">
                        <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>
                    </h2>
                    <h3 style="font-size: 20px;">Descripción: 
                            <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                    </h3>
                    <h3 style="font-size: 20px;">Marca: 
                            <asp:Label ID="lblMarca" runat="server" Text=""></asp:Label>
                    </h3>
                    <h3 style="font-size: 20px;">Categoría:
                            <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
                    </h3>
                    <h3 style="font-size: 20px;">Precio: $
                            <asp:Label ID="lblPrecioArt" runat="server" Text=""></asp:Label>
                    </h3>
                </div>


                <div class="btn-group" role="group" aria-label="Basic example" style="margin-top: 25px;">


                    <asp:Button ID="btnQuitarAlCarrito" runat="server" Text="-" type="button" class="btn btn-primary" OnClick="btnQuitar_click" />
                    <button type="button" class="btn btn-primary custom-button">
                        <asp:Label ID="lblCantCarrito" runat="server" Text="Agregar Al Carrito"></asp:Label>
                    </button>
                    <asp:Button ID="btnAgregarAlCarrito" runat="server" Text="+" type="button" class="btn btn-primary" OnClick="btnAgregar_click" />
                
                        </div>


            </div>
        </div>
    </div>


</asp:Content>
