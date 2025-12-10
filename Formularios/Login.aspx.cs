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
            
            if (!Page.IsValid)
                return;

            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text; 
            string rolSeleccionado = ddlRol.SelectedValue;

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
                  WHERE Correo = @Correo
                    AND Contrasena = @Contrasena
                    AND Rol = @Rol", cn))
            {
               
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                cmd.Parameters.AddWithValue("@Rol", rolSeleccionado);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int idUsuario = dr.GetInt32(dr.GetOrdinal("IdUsuario"));
                        string nombre = dr["Nombre"].ToString();
                        string rolDb = dr["Rol"].ToString();

                        Session["UsuarioId"] = idUsuario;
                        Session["UsuarioNombre"] = nombre;
                        Session["Rol"] = rolDb;
                        Session["Usuario"] = correo;

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
