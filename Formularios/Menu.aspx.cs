using System;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class Menu : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool estaLogueado = Session["Usuario"] != null;

            pnlDashboard.Visible = estaLogueado;
            pnlLogin.Visible = !estaLogueado;
            pnlLogout.Visible = estaLogueado;

            if (!estaLogueado)
            {
                pnlUsuariosAdmin.Visible = false;
                pnlClientesAdmin.Visible = false;
                pnlReportesAdmin.Visible = false;
                pnlAjaxDemoAdmin.Visible = false;
                pnlCarritoCliente.Visible = false;
                pnlMiPerfil.Visible = false;
                pnlInformacion.Visible = false;
                return;
            }

            string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            bool esAdmin = rol == "ADMIN";
            bool esCliente = rol == "CLIENTE";

            pnlUsuariosAdmin.Visible = esAdmin;
            pnlClientesAdmin.Visible = esAdmin;
            pnlReportesAdmin.Visible = esAdmin;
            pnlAjaxDemoAdmin.Visible = esAdmin;

            pnlCarritoCliente.Visible = esCliente;

            pnlMiPerfil.Visible = true;
            pnlInformacion.Visible = true;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Menu.aspx");
        }
    }
}
