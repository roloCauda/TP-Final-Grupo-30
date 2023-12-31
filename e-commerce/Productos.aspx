﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="e_commerce.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-default">
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater runat="server" ID="repRepetidor" OnItemDataBound="repRepetidor_ItemDataBound">
                <ItemTemplate>
                    <div class="col">
                        <div class="card custom-card" style="align-items:center;">       
                            <img src="<%#Eval("ListaImagenes[0]") %>" class="card-img-top" alt="..." style="padding-top:15px; height:300px; width:270px;">                            
                            <div class="card-body text-center">
                                <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                <p class="card-text"><%#Eval("Descripcion") %></p>
                                <a href="Detalle.aspx?id=<%#Eval("IdArticulo") %>" class="btn btn-primary" style="margin-bottom:15px;">Ver Detalle</a>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lnkFavorito" runat="server" CssClass="bi bi-heart" OnClick="lnkFavorito_Click" CommandArgument='<%# Eval("IdArticulo") %>'></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
