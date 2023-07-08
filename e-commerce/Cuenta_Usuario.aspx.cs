﻿using dominio;
using e_commerce.Pag_Cliente;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class Cuenta_Usuario : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public dominio.Carrito carrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                carrito = (dominio.Carrito)Session["ListaItems"];

                MostrarPanel("Perfil");

                LocalidadNegocio negocioLocalidad = new LocalidadNegocio();
                List<Localidad> listaLocalidad = negocioLocalidad.listar();

                ProvinciaNegocio negocioProvincia = new ProvinciaNegocio();
                List<Provincia> listaProvincia = negocioProvincia.listar();

                ddlLocalidad.DataSource = listaLocalidad;
                ddlLocalidad.DataValueField = "Id";
                ddlLocalidad.DataTextField = "Descripcion";
                ddlLocalidad.DataBind();

                ddlProvincia.DataSource = listaProvincia;
                ddlProvincia.DataValueField = "Id";
                ddlProvincia.DataTextField = "Descripcion";
                ddlProvincia.DataBind();

                //si esta en MI CUENTA no es necesario
                ddlLocalidad.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione --", ""));

                /*  Actualiza las Label de la Master */
                Label lblCantCarrito = Master.FindControl("lblCantCarrito") as Label;
                lblCantCarrito.Text = carrito.ListaItems.Count.ToString();

                Label lblPrecio = Master.FindControl("lblPrecio") as Label;
                lblPrecio.Text = "$" + carrito.total.ToString();

                //Cargar datos en campos
                Usuario user = (Usuario)Session["usuario"];

                txtDNI.Text = user.DNI.ToString();
                txtNombres.Text = user.Nombres.ToString();
                txtApellidos.Text = user.Apellidos.ToString();
                txtEmail.Text = user.Email.ToString();
                txtPasswordActual.Text = user.Contraseña.ToString();
                txtTelefono.Text = user.Telefono.ToString();
                txtCalle.Text = user.direccion.Calle.ToString();
                txtNumeracion.Text = user.direccion.Numero.ToString();
                txtPiso.Text = user.direccion.Piso.ToString();
                txtDepartamento.Text = user.direccion.Departamento.ToString();
                txtCP.Text = user.direccion.CodPostal.ToString();
                ddlLocalidad.SelectedValue = user.direccion.Localidad.Id.ToString();
                ddlProvincia.SelectedValue = user.direccion.Provincia.Id.ToString();
             }
        }

        protected void lnk_Opcion_Click(object sender, EventArgs e)
        {
            LinkButton lnk_Opcion = (LinkButton)sender;
            string opcion = lnk_Opcion.CommandArgument;
            MostrarPanel(opcion);
        }

        private void MostrarPanel(string opcion)
        {
            pnl_Perfil.Visible = (opcion == "Perfil");
            pnl_Direccion.Visible = (opcion == "Direccion");
            pnl_Contraseña.Visible = (opcion == "Contraseña");
            pnl_Favoritos.Visible = (opcion == "Favoritos");
            pnl_Pedidos.Visible = (opcion == "Pedidos");
        }

        protected void lnk_Salir_Click(object sender, EventArgs e)
        {
            //cerrar SESION
        }

        protected void btn_GuardarCambiosPerfil_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {

        }

        protected void btn_GuardarContraseña_Click(object sender, EventArgs e)
        {

        }
    }
}