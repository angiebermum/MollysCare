using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class MiPerfil : Page
    {
        private readonly string cs =
            ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string usuario = Session["Usuario"].ToString();
            string rol = (Session["Rol"] ?? "").ToString();

            lblInfoRol.Text = $"Usuario: {usuario} — Rol: {rol}";

            if (!IsPostBack)
            {
                CargarDatos(usuario);
            }
        }

        private void CargarDatos(string correo)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Nombre, Correo, Contrasena 
                  FROM Usuarios
                  WHERE Correo = @Correo", cn))
            {
                cmd.Parameters.AddWithValue("@Correo", correo);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        txtNombre.Text = dr["Nombre"].ToString();
                        lblCorreo.Text = dr["Correo"].ToString();
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string correo = Session["Usuario"].ToString();
            string nombre = txtNombre.Text.Trim();
            string nueva = txtNuevaContrasena.Text.Trim();
            string confirmar = txtConfirmarContrasena.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "El nombre no puede estar vacío.";
                return;
            }

            if (!string.IsNullOrEmpty(nueva) || !string.IsNullOrEmpty(confirmar))
            {
                if (nueva != confirmar)
                {
                    lblMensaje.Text = "Las contraseñas no coinciden.";
                    return;
                }
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = string.IsNullOrEmpty(nueva)
                        ? @"UPDATE Usuarios
                            SET Nombre = @Nombre
                            WHERE Correo = @Correo"
                        : @"UPDATE Usuarios
                            SET Nombre = @Nombre,
                                Contrasena = @Contrasena
                            WHERE Correo = @Correo";

                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    if (!string.IsNullOrEmpty(nueva))
                    {
                        cmd.Parameters.AddWithValue("@Contrasena", nueva);
                    }

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.CssClass = "text-success d-block mb-2";
                lblMensaje.Text = "Datos actualizados correctamente.";
                txtNuevaContrasena.Text = txtConfirmarContrasena.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-2";
                lblMensaje.Text = "Error al actualizar el perfil: " + ex.Message;
            }
        }
    }
}
