using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MollysCare.Formularios
{
    public partial class Productos : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Debe estar logueado
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string rol = (Session["Rol"] ?? "").ToString();
            bool esAdmin = rol == "ADMIN";

            lblRol.Text = "Rol actual: " + (esAdmin ? "Administrador (puede gestionar productos)" : "Cliente (solo lectura)");
            pnlAdmin.Visible = esAdmin;   // solo admin ve el formulario de alta

            if (!IsPostBack)
            {
                CargarProductos();
            }

            // Aseguramos visibilidad de la columna de acciones según el rol
            gvProductos.Columns[0].Visible = esAdmin; // CommandField
        }

        private void CargarProductos()
        {
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT IdProducto, Nombre, Categoria, Marca, Precio, StockActual, StockMinimo
                  FROM dbo.Productos
                  WHERE EsActivo = 1", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProductos.DataSource = dt;
                gvProductos.DataBind();
            }

            // Después de enlazar, ajustar visibilidad de columna de acciones según rol
            bool esAdmin = (Session["Rol"] ?? "").ToString() == "ADMIN";
            gvProductos.Columns[0].Visible = esAdmin;
        }

        // Alta de nuevo producto (solo admin)
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string rol = (Session["Rol"] ?? "").ToString();
            if (rol != "ADMIN")
            {
                lblMensaje.Text = "No tiene permisos para registrar productos.";
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string categoria = txtCategoria.Text.Trim();
            string marca = txtMarca.Text.Trim();

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) ||
                !int.TryParse(txtStockActual.Text.Trim(), out int stockActual) ||
                !int.TryParse(txtStockMinimo.Text.Trim(), out int stockMinimo))
            {
                lblMensaje.Text = "Verifique precio y cantidades.";
                return;
            }

            if (string.IsNullOrEmpty(nombre))
            {
                lblMensaje.Text = "El nombre del producto es obligatorio.";
                return;
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO dbo.Productos
                      (Nombre, Descripcion, Precio, StockActual, StockMinimo, Categoria, Marca, EsActivo)
                      VALUES (@Nombre, @Descripcion, @Precio, @StockActual, @StockMinimo, @Categoria, @Marca, 1)", cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@StockActual", stockActual);
                    cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);
                    cmd.Parameters.AddWithValue("@Categoria", (object)categoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Marca", (object)marca ?? DBNull.Value);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.CssClass = "text-success d-block mb-2";
                lblMensaje.Text = "Producto registrado correctamente.";

                txtNombre.Text = txtCategoria.Text = txtMarca.Text = "";
                txtPrecio.Text = txtStockActual.Text = txtStockMinimo.Text = "";

                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-2";
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }

        // Poner fila en modo edición
        protected void gvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string rol = (Session["Rol"] ?? "").ToString();
            if (rol != "ADMIN")
            {
                lblMensaje.Text = "No tiene permisos para editar productos.";
                return;
            }

            gvProductos.EditIndex = e.NewEditIndex;
            CargarProductos();
        }

        // Cancelar edición
        protected void gvProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProductos.EditIndex = -1;
            CargarProductos();
        }

        // Actualizar producto
        protected void gvProductos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string rol = (Session["Rol"] ?? "").ToString();
            if (rol != "ADMIN")
            {
                lblMensaje.Text = "No tiene permisos para actualizar productos.";
                return;
            }

            int idProducto = Convert.ToInt32(gvProductos.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvProductos.Rows[e.RowIndex];

            // Columnas: 0=acciones, 1=IdProducto, 2=Nombre, 3=Categoria, 4=Marca, 5=Precio, 6=StockActual, 7=StockMinimo
            string nombre = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string categoria = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();
            string marca = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();

            string precioTexto = ((TextBox)row.Cells[5].Controls[0]).Text.Trim();
            string stockActualTexto = ((TextBox)row.Cells[6].Controls[0]).Text.Trim();
            string stockMinimoTexto = ((TextBox)row.Cells[7].Controls[0]).Text.Trim();

            if (!decimal.TryParse(precioTexto, out decimal precio) ||
                !int.TryParse(stockActualTexto, out int stockActual) ||
                !int.TryParse(stockMinimoTexto, out int stockMinimo))
            {
                lblMensaje.Text = "Verifique precio y cantidades al actualizar.";
                return;
            }

            if (string.IsNullOrEmpty(nombre))
            {
                lblMensaje.Text = "El nombre del producto es obligatorio.";
                return;
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(
                    @"UPDATE dbo.Productos
                      SET Nombre = @Nombre,
                          Categoria = @Categoria,
                          Marca = @Marca,
                          Precio = @Precio,
                          StockActual = @StockActual,
                          StockMinimo = @StockMinimo
                      WHERE IdProducto = @IdProducto", cn))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Categoria", (object)categoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Marca", (object)marca ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@StockActual", stockActual);
                    cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvProductos.EditIndex = -1;
                lblMensaje.CssClass = "text-success d-block mb-2";
                lblMensaje.Text = "Producto actualizado correctamente.";
                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-2";
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
            }
        }

        // Eliminar producto (baja lógica: EsActivo = 0)
        protected void gvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rol = (Session["Rol"] ?? "").ToString();
            if (rol != "ADMIN")
            {
                lblMensaje.Text = "No tiene permisos para eliminar productos.";
                return;
            }

            int idProducto = Convert.ToInt32(gvProductos.DataKeys[e.RowIndex].Value);

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(
                    @"UPDATE dbo.Productos
                      SET EsActivo = 0
                      WHERE IdProducto = @IdProducto", cn))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.CssClass = "text-success d-block mb-2";
                lblMensaje.Text = "Producto eliminado correctamente.";
                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-2";
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
            }
        }
    }
}
