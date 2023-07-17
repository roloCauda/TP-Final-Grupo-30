using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class DetalleCliente : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            lblGuardadoConExito.Visible = false;
            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if(usuario.TipoUsuario != TipoUsuario.ADMIN)
            {
                btnCancelar.Text = "Volver";
                btnCancelar.Visible = true;
                btnEliminar.Visible = false;
                btnGuardarCambios.Visible = false;
                ddlAcceso.Enabled = false;
            }

            if (!IsPostBack)
            {
                Usuario user = new Usuario();

                //Cargar datos en campos
                UsuarioNegocio negocioU = new UsuarioNegocio();
                int idUsuario = int.Parse(Request.QueryString["id"]);
                
                user = negocioU.CargarUsuario(idUsuario);

                DireccionNegocio negocioD = new DireccionNegocio();
                user.direccion = negocioD.CargarDireccion(user.direccion);

                LocalidadNegocio negocioL = new LocalidadNegocio();
                user.direccion.Localidad = negocioL.cargarLocalidad(user.direccion.Localidad);

                ProvinciaNegocio negocioP = new ProvinciaNegocio();
                user.direccion.Provincia = negocioP.cargarProvincia(user.direccion.Provincia);

                lblDNI.Text = user.DNI.ToString();

                lblNombres.Text = user.Nombres.ToString();
                lblApellidos.Text = user.Apellidos.ToString();
                lblEmail.Text = user.Email.ToString();

                if (!string.IsNullOrEmpty(user.Telefono))
                    lblTelefono.Text = user.Telefono.ToString();

                lblCalle.Text = user.direccion.Calle.ToString();
                lblNumero.Text = user.direccion.Numero.ToString();

                if (!string.IsNullOrEmpty(user.direccion.Piso.ToString()))
                    lblPiso.Text = user.direccion.Piso.ToString();

                if (!string.IsNullOrEmpty(user.direccion.Departamento))
                    lblDepartamento.Text = user.direccion.Departamento.ToString();

                lblCodPostal.Text = user.direccion.CodPostal.ToString();
                lblLocalidad.Text = user.direccion.Localidad.Descripcion.ToString();
                lblProvincia.Text = user.direccion.Provincia.Descripcion.ToString();

                TipoDeAccesoNegocio negocioA = new TipoDeAccesoNegocio();
                List<TipoDeAcceso> lista = negocioA.listar();

                ddlAcceso.DataSource = lista;
                ddlAcceso.DataValueField = "Id";
                ddlAcceso.DataTextField = "Descripcion";
                ddlAcceso.DataBind();

                ddlAcceso.SelectedValue = ((int)user.TipoUsuario).ToString();

                Session.Add("IdUsuario", user.IdUsuario);
            }

                /*PedidoNegocio negocioU = new PedidoNegocio();
                dgvPedidosCliente.DataSource = negocioU.listarPedidosPorCliente(IdUsuario);
                dgvPedidosCliente.DataBind();*/
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
            Response.Redirect("VerClientes.aspx");
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            int idUsuario = (int)Session["IdUsuario"];

            UsuarioNegocio negocioU = new UsuarioNegocio();
            negocioU.cambiarAcceso(idUsuario, int.Parse(ddlAcceso.SelectedValue));

            lblGuardadoConExito.Visible = true;
        }
    }
}