﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="e_commerce.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--Carrousel-->
    <div id="carouselExampleAutoplaying" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="https://i.imgur.com/tiH7YdL.jpg" class="d-block w-100 carousel-img" alt="...">
            </div>
            <div class="carousel-item">
                <img src="https://i.imgur.com/RWLntdI.png" class="d-block w-100 carousel-img" alt="...">
            </div>
            <div class="carousel-item">
                <img src="https://i.imgur.com/Hq9sqAH.jpg" class="d-block w-100 carousel-img" alt="...">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

    <div style="display: flex; justify-content: center; align-items: center; padding-top: 50px;">
        <h2>PRODUCTOS DESTACADOS</h2>
    </div>

    <div class="container-default">
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater runat="server" ID="repRepetidor" OnItemDataBound="repRepetidor_ItemDataBound">
                <ItemTemplate>
                    <div class="col">
                        <div class="card custom-card" style="align-items: center;">
                            <img src="<%#Eval("ListaImagenes[0]") %>" class="card-img-top" alt="..." style="padding-top: 15px; height: 300px; width: 270px;">
                            <div class="card-body text-center">
                                <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                <p class="card-text"><%#Eval("Descripcion") %></p>
                                <a href="Detalle.aspx?id=<%#Eval("IdArticulo") %>" class="btn btn-primary" style="margin-bottom:15px;">Ver Detalle</a>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lnkFavorito" runat="server" CssClass="bi bi-heart" OnClick="lnkFavoritoClick" CommandArgument='<%# Eval("IdArticulo") %>'></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div style="display: flex; justify-content: center; align-items: center; height: 150px;">
        <a href="Productos.aspx" class="btn" style="display: inline-block; margin: 0 auto; background-color: #1c3166; color: #fff;">Ver Más</a>
    </div>

    <div style="display: flex; flex-direction: column; justify-content: center; align-items: center; min-height: 150px; margin-top: 50px;">
        <div style="padding-bottom:20px;">
            <h2>ENCONTRANOS</h2>
        </div>

        <div style="background-color:#1c3166; text-align:center; color:white;">
            <img src="https://i.imgur.com/SuthGue.png" alt="mapa" style="padding-bottom:15px;"/>
            <p>
                Estamos en Parque Chacabuco, Capital Federal
            </p>
            <p>
                José María Moreno 1354
            </p>

        </div>
    </div>

    <div style="display: flex; flex-direction: column; justify-content: center; align-items: center; min-height: 150px; margin-top: 150px;">
        <div>
            <h2>MARCAS</h2>
        </div>
        <div class="container-default">
            <div class="row row-cols-1 row-cols-md-5 g-4">
                <asp:Repeater runat="server" ID="repRepetidorMarca">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card custom-card" style="align-items: center; height: 200px; padding: 15px;">
                                <img src="<%#Eval("ImagenURL") %>" class="card-img-top" alt="..." style="height: 175px; width: 200px;">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </div>


</asp:Content>
