using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TuProyecto
{
    public partial class ListaMascotas : System.Web.UI.Page
    {
        private const string VS_TABLE = "MascotasTabla";
        private const string VS_SORTEXPR = "SortExpr";
        private const string VS_SORTDIR = "SortDir"; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos(string filtroNombre = null)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            string sql = @"
                SELECT m.IdMascota, m.Nombre, m.FechaNacimiento,
                       e.Nombre AS Especie, r.Nombre AS Raza, d.NombreCompleto AS Dueno
                FROM Mascotas m
                LEFT JOIN Especies e ON m.IdEspecie = e.IdEspecie
                LEFT JOIN Razas r    ON m.IdRaza   = r.IdRaza
                LEFT JOIN Duenos d   ON m.IdDueno  = d.IdDueno
                WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(filtroNombre))
            {
                sql += " AND m.Nombre LIKE @q";
            }

            string sortExpr = (ViewState[VS_SORTEXPR] as string) ?? "m.Nombre";
            string sortDir = (ViewState[VS_SORTDIR] as string) ?? "ASC";
            sql += $" ORDER BY {sortExpr} {sortDir}";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                if (!string.IsNullOrWhiteSpace(filtroNombre))
                    cmd.Parameters.AddWithValue("@q", "%" + filtroNombre.Trim() + "%");

                var dt = new DataTable();
                da.Fill(dt);

                ViewState[VS_TABLE] = dt;
                gvMascotas.DataSource = dt;
                gvMascotas.DataBind();

                lblMensaje.Visible = dt.Rows.Count == 0;
                lblMensaje.Text = string.IsNullOrWhiteSpace(filtroNombre)
                    ? "Aún no hay mascotas registradas"
                    : "No hay resultados que coincidan con la búsqueda.";
                lblMensaje.CssClass = "mensaje-centrado text-danger";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvMascotas.PageIndex = 0;
            CargarDatos(txtBuscar.Text);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            gvMascotas.PageIndex = 0;
            ViewState[VS_SORTEXPR] = null;
            ViewState[VS_SORTDIR] = null;
            CargarDatos();
        }


        protected void btnNueva_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroMascota.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        protected void gvMascotas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMascotas.PageIndex = e.NewPageIndex;
            CargarDatos(txtBuscar.Text);
        }

        protected void gvMascotas_Sorting(object sender, GridViewSortEventArgs e)
        {
            string currentExpr = ViewState[VS_SORTEXPR] as string;
            string currentDir = ViewState[VS_SORTDIR] as string ?? "ASC";

            if (currentExpr == e.SortExpression)
                currentDir = currentDir == "ASC" ? "DESC" : "ASC";
            else
                currentDir = "ASC";

            string exprSql;
            switch (e.SortExpression)
            {
                case "IdMascota":
                    exprSql = "m.IdMascota";
                    break;
                case "Nombre":
                    exprSql = "m.Nombre";
                    break;
                case "FechaNacimiento":
                    exprSql = "m.FechaNacimiento";
                    break;
                case "Especie":
                    exprSql = "e.Nombre";
                    break;
                case "Raza":
                    exprSql = "r.Nombre";
                    break;
                case "Dueno":
                    exprSql = "d.NombreCompleto";
                    break;
                default:
                    exprSql = "m.Nombre";
                    break;
            }

            ViewState[VS_SORTEXPR] = exprSql;
            ViewState[VS_SORTDIR] = currentDir;

            CargarDatos(txtBuscar.Text);
        }

        protected void gvMascotas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMascotas.EditIndex = e.NewEditIndex;
            CargarDatos(txtBuscar.Text);
        }

        protected void gvMascotas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMascotas.EditIndex = -1;
            CargarDatos(txtBuscar.Text);
        }

        protected void gvMascotas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gvMascotas.DataKeys[e.RowIndex].Value;

            GridViewRow row = gvMascotas.Rows[e.RowIndex];

            string nuevoNombre = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();

            var txtFecha = (TextBox)row.FindControl("txtFechaEdit");
            DateTime fecha = DateTime.Parse(txtFecha.Text);

            string connStr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"UPDATE Mascotas
                                              SET Nombre=@n, FechaNacimiento=@f
                                              WHERE IdMascota=@id", conn))
            {
                cmd.Parameters.AddWithValue("@n", nuevoNombre);
                cmd.Parameters.AddWithValue("@f", fecha);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvMascotas.EditIndex = -1;
            CargarDatos(txtBuscar.Text);
        }

        protected void gvMascotas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvMascotas.DataKeys[e.RowIndex].Value;

            string connStr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("DELETE FROM Mascotas WHERE IdMascota=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                CargarDatos(txtBuscar.Text);
                lblMensaje.Visible = true;
                lblMensaje.Text = "Mascota eliminada correctamente";
                lblMensaje.CssClass = "mensaje-centrado text-success";
            }
            catch (Exception ex)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No se pudo eliminar la mascota" + ex.Message;
                lblMensaje.CssClass = "mensaje-centrado text-danger";
            }
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
