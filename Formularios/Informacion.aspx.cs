using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class Informacion : Page
    {
        private const int INFO_ID = 1; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rolRaw = (Session["Rol"] ?? "").ToString();
                string rol = rolRaw.ToUpperInvariant();

                bool esAdmin = (rol == "ADMIN");
                bool esCliente = (rol == "CLIENTE");

                lblRolInfo.Text = esAdmin
                    ? "Rol actual: Administrador (puede editar la información)."
                    : "Rol actual: Cliente (solo lectura).";

            
                EstablecerModoSoloLectura(!esAdmin);

                CargarInformacion();
            }
        }

        private void EstablecerModoSoloLectura(bool soloLectura)
        {
            txtNombreNegocio.ReadOnly = soloLectura;
            txtDescripcion.ReadOnly = soloLectura;
            txtDireccion.ReadOnly = soloLectura;
            txtTelefono.ReadOnly = soloLectura;
            txtCorreo.ReadOnly = soloLectura;
            txtInstagram.ReadOnly = soloLectura;
            txtPolitica.ReadOnly = soloLectura;

            btnGuardar.Visible = !soloLectura;

            if (soloLectura)
            {
                lblNotaSoloLectura.Text = "Solo el administrador puede modificar esta información.";
            }
            else
            {
                lblNotaSoloLectura.Text = string.Empty;
            }
        }

        private void CargarInformacion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT TOP 1 IdInfo, NombreNegocio, Descripcion, Direccion, Telefono, Correo, Instagram, PoliticaDevoluciones
                               FROM InformacionNegocio
                               WHERE IdInfo = @IdInfo";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdInfo", INFO_ID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtNombreNegocio.Text = dr["NombreNegocio"].ToString();
                            txtDescripcion.Text = dr["Descripcion"].ToString();
                            txtDireccion.Text = dr["Direccion"].ToString();
                            txtTelefono.Text = dr["Telefono"].ToString();
                            txtCorreo.Text = dr["Correo"].ToString();
                            txtInstagram.Text = dr["Instagram"].ToString();
                            txtPolitica.Text = dr["PoliticaDevoluciones"].ToString();
                        }
                        else
                        {
                            lblMensaje.CssClass = "text-danger";
                            lblMensaje.Text = "No se encontró la información del negocio. Verifique la tabla InformacionNegocio.";
                        }
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"UPDATE InformacionNegocio
                                   SET NombreNegocio = @Nombre,
                                       Descripcion = @Descripcion,
                                       Direccion = @Direccion,
                                       Telefono = @Telefono,
                                       Correo = @Correo,
                                       Instagram = @Instagram,
                                       PoliticaDevoluciones = @Politica
                                   WHERE IdInfo = @IdInfo";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtNombreNegocio.Text.Trim());
                        cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                        cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Instagram", txtInstagram.Text.Trim());
                        cmd.Parameters.AddWithValue("@Politica", txtPolitica.Text.Trim());
                        cmd.Parameters.AddWithValue("@IdInfo", INFO_ID);

                        int filas = cmd.ExecuteNonQuery();

                        lblMensaje.CssClass = "text-success";
                        lblMensaje.Text = (filas > 0)
                            ? "Información actualizada correctamente."
                            : "No se actualizó ningún registro.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = "Ocurrió un error al guardar: " + ex.Message;
            }
        }
    }
}
