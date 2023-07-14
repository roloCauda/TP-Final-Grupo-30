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
    public partial class VerClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];

            if (Session["usuario"] == null || user.TipoUsuario == TipoUsuario.CLIENTE)
            {
                Response.Redirect("Default.aspx");
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvClientes.DataSource = negocio.listarSegunAcceso(3);
            dgvClientes.DataBind(); /*para que enlace los datos, que los escriba en la grilla*/
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            dgvClientes.DataBind();
        }

        protected void dgvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}