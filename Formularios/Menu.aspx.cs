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

            if (estaLogueado)
            {
                string rol = (Session["Rol"] ?? "").ToString();
                
                pnlUsuariosAdmin.Visible = (rol == "ADMIN");
            }
            else
            {
                pnlUsuariosAdmin.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
           
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Menu.aspx");
        }
    }
}
