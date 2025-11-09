using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace TuProyecto
{
    public partial class RegistroMascota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
            }
        }

        private void CargarCombos()
        {
            string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();
                // Especies
                using (SqlCommand cmd = new SqlCommand("SELECT IdEspecie, Nombre FROM Especies", conn))
                {
                    ddlEspecie.DataSource = cmd.ExecuteReader();
                    ddlEspecie.DataTextField = "Nombre";
                    ddlEspecie.DataValueField = "IdEspecie";
                    ddlEspecie.DataBind();
                }

                conn.Close();
                conn.Open();
                // Razas
                using (SqlCommand cmd = new SqlCommand("SELECT IdRaza, Nombre FROM Razas", conn))
                {
                    ddlRaza.DataSource = cmd.ExecuteReader();
                    ddlRaza.DataTextField = "Nombre";
                    ddlRaza.DataValueField = "IdRaza";
                    ddlRaza.DataBind();
                }

                conn.Close();
                conn.Open();
                // Dueños
                using (SqlCommand cmd = new SqlCommand("SELECT IdDueno, NombreCompleto FROM Duenos", conn))
                {
                    ddlDueno.DataSource = cmd.ExecuteReader();
                    ddlDueno.DataTextField = "NombreCompleto";
                    ddlDueno.DataValueField = "IdDueno";
                    ddlDueno.DataBind();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                DateTime fecha = Convert.ToDateTime(txtFechaNacimiento.Text);
                int idEspecie = int.Parse(ddlEspecie.SelectedValue);
                int idRaza = int.Parse(ddlRaza.SelectedValue);
                int idDueno = int.Parse(ddlDueno.SelectedValue);
                string sexo = ddlSexo.SelectedValue;

                string color = string.IsNullOrWhiteSpace(txtColor.Text) ? null : txtColor.Text.Trim();
                string obs = string.IsNullOrWhiteSpace(txtObs.Text) ? null : txtObs.Text.Trim();
                string fotoUrl = GuardarFoto();

                string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Mascotas
                    (Nombre, FechaNacimiento, IdEspecie, IdRaza, IdDueno, Sexo, Color, Observaciones, FotoUrl)
                    VALUES
                    (@Nombre, @FechaNacimiento, @IdEspecie, @IdRaza, @IdDueno, @Sexo, @Color, @Observaciones, @FotoUrl)", conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fecha);
                    cmd.Parameters.AddWithValue("@IdEspecie", idEspecie);
                    cmd.Parameters.AddWithValue("@IdRaza", idRaza);
                    cmd.Parameters.AddWithValue("@IdDueno", idDueno);
                    cmd.Parameters.AddWithValue("@Sexo", (object)sexo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Color", (object)color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)obs ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FotoUrl", (object)fotoUrl ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(fotoUrl))
                {
                    imgPreview.ImageUrl = ResolveUrl(fotoUrl);
                    imgPreview.CssClass = imgPreview.CssClass.Replace("d-none", "").Trim();
                }

                MostrarMensaje("Mascota registrada con éxito", true);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al registrar la mascota" + ex.Message, false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        private string GuardarFoto()
        {
            if (!fuFoto.HasFile) return null;

            string ext = Path.GetExtension(fuFoto.FileName).ToLowerInvariant();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                throw new Exception("Formato de imagen no permitido");

            if (fuFoto.PostedFile.ContentLength > 2 * 1024 * 1024)
                throw new Exception("La imagen supera 2 MB.");

            string carpetaRel = "~/uploads/mascotas/";
            string carpetaAbs = Server.MapPath(carpetaRel);
            if (!Directory.Exists(carpetaAbs))
                Directory.CreateDirectory(carpetaAbs);

            string nombreArchivo = Guid.NewGuid().ToString("N") + ext;
            string rutaAbs = Path.Combine(carpetaAbs, nombreArchivo);
            fuFoto.SaveAs(rutaAbs);

            return carpetaRel + nombreArchivo;
        }

        private void MostrarMensaje(string msg, bool ok)
        {
            lblMensaje.Text = msg;
            lblMensaje.ForeColor = ok ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensaje.CssClass = "mensaje-centrado"; 
            lblMensaje.Visible = true;
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtFechaNacimiento.Text = "";
            if (ddlEspecie.Items.Count > 0) ddlEspecie.SelectedIndex = 0;
            if (ddlRaza.Items.Count > 0) ddlRaza.SelectedIndex = 0;
            if (ddlDueno.Items.Count > 0) ddlDueno.SelectedIndex = 0;
            if (ddlSexo.Items.Count > 0) ddlSexo.SelectedIndex = 0;
            txtColor.Text = "";
            txtObs.Text = "";
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
