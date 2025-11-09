using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo permitir acceso si es ADMIN
            if (Session["Rol"] == null || Session["Rol"].ToString() != "ADMIN")
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string confirmar = txtConfirmar.Text.Trim();
            string rol = ddlRol.SelectedValue;

            if (string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Complete todos los datos.";
                lblMensaje.CssClass = "d-block mb-2 text-danger";
                return;
            }

            if (contrasena != confirmar)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                lblMensaje.CssClass = "d-block mb-2 text-danger";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                {
                    cn.Open();

                    // Verificar si ya existe el correo
                    using (SqlCommand cmdExiste = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.Usuarios WHERE Correo = @Correo", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Correo", correo);
                        int existe = (int)cmdExiste.ExecuteScalar();

                        if (existe > 0)
                        {
                            lblMensaje.Text = "Ya existe un usuario registrado con ese correo.";
                            lblMensaje.CssClass = "d-block mb-2 text-danger";
                            return;
                        }
                    }

                    // Insertar nuevo usuario
                    using (SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO dbo.Usuarios (Nombre, Correo, Contrasena, Rol)
                          VALUES (@Nombre, @Correo, @Contrasena, @Rol)", cn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@Rol", rol);

                        cmd.ExecuteNonQuery();
                    }
                }

                lblMensaje.Text = "Usuario registrado correctamente.";
                lblMensaje.CssClass = "d-block mb-2 text-success";

                txtNombre.Text = txtCorreo.Text = "";
                txtContrasena.Text = txtConfirmar.Text = "";
                ddlRol.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar: " + ex.Message;
                lblMensaje.CssClass = "d-block mb-2 text-danger";
            }
        }
    }
}
