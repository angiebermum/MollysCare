using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TuProyecto
{
    public partial class ReporteMascotas : System.Web.UI.Page
    {
        private const string VS_KEY = "DATOS_REPORTE";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarDatos();
            }
        }

        private void CargarFiltros()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT IdEspecie, Nombre FROM Especies ORDER BY Nombre", conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    ddlFiltroEspecie.DataSource = rdr;
                    ddlFiltroEspecie.DataTextField = "Nombre";
                    ddlFiltroEspecie.DataValueField = "IdEspecie";
                    ddlFiltroEspecie.DataBind();
                }
                ddlFiltroEspecie.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", ""));

                CargarRazas(ddlFiltroEspecie.SelectedValue);

                using (var cmd = new SqlCommand("SELECT IdDueno, NombreCompleto FROM Duenos ORDER BY NombreCompleto", conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    ddlFiltroDueno.DataSource = rdr;
                    ddlFiltroDueno.DataTextField = "NombreCompleto";
                    ddlFiltroDueno.DataValueField = "IdDueno";
                    ddlFiltroDueno.DataBind();
                }
                ddlFiltroDueno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", ""));
            }
        }

        private void CargarRazas(string idEspecie)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            string sql = string.IsNullOrEmpty(idEspecie)
                ? "SELECT IdRaza, Nombre FROM Razas ORDER BY Nombre"
                : "SELECT IdRaza, Nombre FROM Razas WHERE IdEspecie=@IdEspecie ORDER BY Nombre";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (!string.IsNullOrEmpty(idEspecie))
                    cmd.Parameters.AddWithValue("@IdEspecie", idEspecie);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    ddlFiltroRaza.DataSource = rdr;
                    ddlFiltroRaza.DataTextField = "Nombre";
                    ddlFiltroRaza.DataValueField = "IdRaza";
                    ddlFiltroRaza.DataBind();
                }
            }
            ddlFiltroRaza.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", ""));
        }

        protected void ddlFiltroEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarRazas(ddlFiltroEspecie.SelectedValue);
        }

        private void CargarDatos()
        {
            var dt = ObtenerDatosFiltrados();
            gvReporte.DataSource = dt;
            gvReporte.DataBind();

            bool sinFiltros =
                string.IsNullOrEmpty(ddlFiltroEspecie.SelectedValue) &&
                string.IsNullOrEmpty(ddlFiltroRaza.SelectedValue) &&
                string.IsNullOrEmpty(ddlFiltroDueno.SelectedValue);

            string filtrosTxt = "";
            if (!string.IsNullOrEmpty(ddlFiltroEspecie.SelectedValue))
                filtrosTxt += $"Especie: {ddlFiltroEspecie.SelectedItem.Text}; ";
            if (!string.IsNullOrEmpty(ddlFiltroRaza.SelectedValue))
                filtrosTxt += $"Raza: {ddlFiltroRaza.SelectedItem.Text}; ";
            if (!string.IsNullOrEmpty(ddlFiltroDueno.SelectedValue))
                filtrosTxt += $"Dueño: {ddlFiltroDueno.SelectedItem.Text}; ";
            filtrosTxt = filtrosTxt.Trim().TrimEnd(';');

            if (dt.Rows.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = sinFiltros
                    ? "Aún no hay mascotas registradas."
                    : "No hay resultados que coincidan con los filtros.";
                lblMensaje.CssClass = "mensaje-centrado text-danger";
                lblResumen.Text = "";
            }
            else
            {
                lblMensaje.Visible = false;
                lblResumen.Text = string.IsNullOrEmpty(filtrosTxt)
                    ? $"Mostrando <strong>{dt.Rows.Count}</strong> mascotas."
                    : $"Mostrando <strong>{dt.Rows.Count}</strong> mascotas (filtros: {filtrosTxt}).";
            }

            ViewState[VS_KEY] = dt; 
        }

        private DataTable ObtenerDatosFiltrados()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            string sql = @"
                SELECT m.IdMascota, m.Nombre, m.FechaNacimiento,
                       e.Nombre AS Especie, r.Nombre AS Raza, d.NombreCompleto AS Dueno
                FROM Mascotas m
                LEFT JOIN Especies e ON m.IdEspecie = e.IdEspecie
                LEFT JOIN Razas r    ON m.IdRaza = r.IdRaza
                LEFT JOIN Duenos d   ON m.IdDueno = d.IdDueno
                WHERE 1=1";

            var dt = new DataTable();

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(ddlFiltroEspecie.SelectedValue))
                {
                    sql += " AND m.IdEspecie = @IdEspecie";
                    cmd.Parameters.AddWithValue("@IdEspecie", ddlFiltroEspecie.SelectedValue);
                }
                if (!string.IsNullOrEmpty(ddlFiltroRaza.SelectedValue))
                {
                    sql += " AND m.IdRaza = @IdRaza";
                    cmd.Parameters.AddWithValue("@IdRaza", ddlFiltroRaza.SelectedValue);
                }
                if (!string.IsNullOrEmpty(ddlFiltroDueno.SelectedValue))
                {
                    sql += " AND m.IdDueno = @IdDueno";
                    cmd.Parameters.AddWithValue("@IdDueno", ddlFiltroDueno.SelectedValue);
                }

                sql += " ORDER BY m.Nombre";
                cmd.CommandText = sql;

                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlFiltroEspecie.SelectedIndex = 0;
            CargarRazas("");
            ddlFiltroDueno.SelectedIndex = 0;
            CargarDatos();
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var data = (ViewState[VS_KEY] as DataTable) ?? ObtenerDatosFiltrados();

                using (var wb = new ClosedXML.Excel.XLWorkbook())
                using (var ms = new MemoryStream())
                {
                    wb.Worksheets.Add(data, "Mascotas");
                    wb.SaveAs(ms);

                    var bytes = ms.ToArray();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Mascotas.xlsx");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al exportar a Excel: " + ex.Message, false);
            }
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                var data = (ViewState[VS_KEY] as DataTable) ?? ObtenerDatosFiltrados();

                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 20f, 20f))
                    {
                        PdfWriter.GetInstance(doc, ms);
                        doc.Open();

                        var titulo = new Paragraph("Reporte de Mascotas")
                        { Alignment = Element.ALIGN_CENTER, SpacingAfter = 10f };
                        doc.Add(titulo);

                        var tabla = new PdfPTable(data.Columns.Count) { WidthPercentage = 100 };
                        foreach (DataColumn col in data.Columns)
                        {
                            var cell = new PdfPCell(new Phrase(col.ColumnName)) { BackgroundColor = BaseColor.LIGHT_GRAY };
                            tabla.AddCell(cell);
                        }
                        foreach (DataRow row in data.Rows)
                            foreach (var cell in row.ItemArray)
                                tabla.AddCell(new Phrase(Convert.ToString(cell)));

                        doc.Add(tabla);
                        doc.Close();
                    }

                    var bytes = ms.ToArray();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Mascotas.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al exportar a PDF: " + ex.Message, false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        private void MostrarMensaje(string msg, bool ok)
        {
            lblMensaje.Text = msg;
            lblMensaje.CssClass = "mensaje-centrado " + (ok ? "text-success" : "text-danger");
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
