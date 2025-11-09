using System;
using System.Web.UI;
using MollysCare.DAL;
using MollysCare.Modelos;

namespace MollysCare.Formularios
{
    public partial class RegistroTratamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMascota.DataSource = MascotaDAL.ObtenerTodas();
                ddlMascota.DataTextField = "Nombre";
                ddlMascota.DataValueField = "IdMascota";
                ddlMascota.DataBind();
                ddlMascota.Items.Insert(0, "Seleccione:");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Tratamiento t = new Tratamiento
                {
                    IdMascota = int.Parse(ddlMascota.SelectedValue),
                    TipoTratamiento = ddlTipoTratamiento.SelectedValue,
                    FechaAplicacion = DateTime.Parse(txtFecha.Text),
                    Observaciones = txtObservaciones.Text
                };

                TratamientoDAL.Insertar(t);
                lblMensaje.Text = "Tratamiento registrado correctamente";

                ddlMascota.SelectedIndex = 0;
                ddlTipoTratamiento.SelectedIndex = 0;
                txtFecha.Text = "";
                txtObservaciones.Text = "";
            }
            catch
            {
                lblMensaje.Text = "Error al registrar tratamiento.";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        private void Ok(string msg)
        {
            lblMensaje.Text = msg;
            lblMensaje.CssClass = "mensaje-centrado text-success";
            lblMensaje.Visible = true;
        }

        private void Fail(Exception ex, string userMsg)
        {
            lblMensaje.Text = userMsg;
            lblMensaje.CssClass = "mensaje-centrado text-danger";
            lblMensaje.Visible = true;
            System.Diagnostics.Debug.WriteLine(ex); 
        }


    }
}
