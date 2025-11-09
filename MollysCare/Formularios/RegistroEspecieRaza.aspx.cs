using System;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using MollysCare.DAL;
using MollysCare.Modelos;

namespace MollysCare.Formularios
{
    public partial class RegistroEspecieRaza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarEspecies();
        }

        private void CargarEspecies()
        {
            ddlEspecie.DataSource = EspecieDAL.ObtenerTodas();
            ddlEspecie.DataTextField = "Nombre";
            ddlEspecie.DataValueField = "IdEspecie";
            ddlEspecie.DataBind();
            ddlEspecie.Items.Insert(0, "Seleccione una opción:");
        }

        protected void btnAgregarEspecie_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtEspecie.Text.Trim();

                if (string.IsNullOrEmpty(nombre))
                {
                    MostrarMensaje("Debe ingresar el nombre de la especie.", false);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString))
                {
                    string sql = "INSERT INTO Especies (Nombre) VALUES (@Nombre)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MostrarMensaje("Especie guardada correctamente", true);
                txtEspecie.Text = "";
                CargarEspecies();
            }
            catch
            {
                MostrarMensaje("Error al guardar especie", false);
            }
        }

        protected void btnAgregarRaza_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtRaza.Text.Trim();

                if (ddlEspecie.SelectedIndex == 0)
                {
                    MostrarMensaje("Debe seleccionar una especie:", false);
                    return;
                }
                if (string.IsNullOrEmpty(nombre))
                {
                    MostrarMensaje("Debe ingresar el nombre de la raza.", false);
                    return;
                }

                int idEspecie = int.Parse(ddlEspecie.SelectedValue);

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString))
                {
                    string sql = "INSERT INTO Razas (Nombre, IdEspecie) VALUES (@Nombre, @IdEspecie)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@IdEspecie", idEspecie);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MostrarMensaje("Raza guardada correctamente", true);
                txtRaza.Text = "";
            }
            catch
            {
                MostrarMensaje("Error al guardar raza", false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        private void MostrarMensaje(string mensaje, bool exito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = exito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensaje.Visible = true;
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
