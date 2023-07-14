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
    public partial class VerEmpelados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];

            if (Session["usuario"] == null || user.TipoUsuario == TipoUsuario.CLIENTE)
            {
                Response.Redirect("Default.aspx");
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvEmpleados.DataSource = negocio.listarSegunAcceso(2);
            dgvEmpleados.DataBind(); /*para que enlace los datos, que los escriba en la grilla*/
        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            dgvEmpleados.DataBind();
        }

        protected void dgvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}