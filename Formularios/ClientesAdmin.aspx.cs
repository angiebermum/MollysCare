using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace MollysCare.Formularios
{
    public partial class ClientesAdmin : Page
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

            string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            if (rol != "ADMIN")
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "Esta pantalla solo está disponible para administradores.";
                gvClientes.Visible = false;
                return;
            }

            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT  u.Nombre,
                          u.Correo,
                          COUNT(p.IdPedido)        AS CantidadPedidos,
                          ISNULL(SUM(p.Total), 0)  AS MontoTotal
                  FROM Usuarios u
                  LEFT JOIN Pedidos p
                     ON p.Usuario = u.Correo
                  WHERE u.Rol = 'CLIENTE'
                  GROUP BY u.Nombre, u.Correo
                  ORDER BY u.Nombre", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvClientes.DataSource = dt;
                gvClientes.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMensaje.Text = "No hay clientes registrados.";
                }
                else
                {
                    lblMensaje.Text = "Listado de clientes y resumen de su actividad en la tienda.";
                }
            }
        }
    }
}
