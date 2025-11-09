using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string rol = ddlRol.SelectedValue;

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Ingrese correo y contraseña.";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT IdUsuario, Nombre, Rol
                  FROM Usuarios
                  WHERE Correo = @Correo AND Contrasena = @Contrasena AND Rol = @Rol", cn))
            {
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                cmd.Parameters.AddWithValue("@Rol", rol);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int idUsuario = dr.GetInt32(0);
                        string nombre = dr.GetString(1);
                        string rolDb = dr.GetString(2);

                        // Guardamos info básica en sesión
                        Session["UsuarioId"] = idUsuario;
                        Session["UsuarioNombre"] = nombre;
                        Session["Rol"] = rolDb;
                        Session["Usuario"] = correo; // para el check del menú

                        Response.Redirect("Menu.aspx");
                    }
                    else
                    {
                        lblMensaje.Text = "Credenciales o rol incorrectos.";
                    }
                }
            }
        }
    }
}
