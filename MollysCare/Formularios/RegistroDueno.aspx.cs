using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MollysCare.Formularios
{
    public partial class RegistroDueno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim();
                string direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MostrarMensaje("El nombre es obligatorio", false);
                    return;
                }

                string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Duenos (NombreCompleto, Telefono, Correo, Direccion)
                    VALUES (@NombreCompleto, @Telefono, @Correo, @Direccion)", conn))
                {
                    cmd.Parameters.AddWithValue("@NombreCompleto", nombre);
                    cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", (object)correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", (object)direccion ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MostrarMensaje("Dueño registrado con éxito", true);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al registrar el dueño" + ex.Message, false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        private void MostrarMensaje(string msg, bool ok)
        {
            lblMensaje.Text = msg;
            lblMensaje.CssClass = "mensaje-centrado " + (ok ? "text-success" : "text-danger");
            lblMensaje.Visible = true;
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
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
