using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MollysCare.Formularios
{
    public partial class Inventario : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string rol = (Session["Rol"] ?? "").ToString();
            lblRol.Text = "Rol actual: " + (rol == "ADMIN" ? "Administrador" : "Cliente");

            if (!IsPostBack)
            {
                CargarInventario();
            }
        }

        private void CargarInventario()
        {
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT 
                      IdProducto,
                      Nombre,
                      Categoria,
                      Marca,
                      StockActual,
                      StockMinimo,
                      CASE 
                          WHEN StockActual < StockMinimo THEN 'Stock bajo'
                          ELSE 'OK'
                      END AS Estado
                  FROM dbo.Productos
                  WHERE EsActivo = 1", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvInventario.DataSource = dt;
                gvInventario.DataBind();
            }
        }

        protected void gvInventario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Tomamos los valores de las celdas de stock
                int stockActual = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "StockActual"));
                int stockMinimo = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "StockMinimo"));

                if (stockActual < stockMinimo)
                {
                    // Fila en rojo clarito cuando el stock es bajo
                    e.Row.CssClass = "table-danger";
                }
            }
        }
    }
}
