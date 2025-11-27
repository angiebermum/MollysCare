using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class HistorialPedidos : Page
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            string usuario = (Session["Usuario"] ?? "").ToString();

            if (rol != "CLIENTE")
            {
                lblRol.Text = "Rol actual: " + rol;
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "Esta pantalla está disponible solo para clientes.";
                gvPedidosCliente.Visible = false;
                return;
            }

            lblRol.Text = "Rol actual: Cliente. Usuario: " + usuario;

            if (!IsPostBack)
            {
                CargarHistorial(usuario);
            }
        }

        private void CargarHistorial(string usuario)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT IdPedido, Fecha, Total, Estado
                  FROM dbo.Pedidos
                  WHERE Usuario = @Usuario
                  ORDER BY Fecha DESC", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuario);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPedidosCliente.DataSource = dt;
                gvPedidosCliente.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMensaje.CssClass = "text-muted d-block mb-3";
                    lblMensaje.Text = "Aún no has realizado pedidos.";
                }
                else
                {
                    lblMensaje.CssClass = "text-muted d-block mb-3";
                    lblMensaje.Text = "Aquí puedes revisar tus pedidos y su estado (En proceso, Enviado, Entregado).";
                }
            }
        }
    }
}
