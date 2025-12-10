using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MollysCare.Formularios
{
    public partial class Productos : Page
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        private const int COL_ACCIONES = 0;
        private const int COL_CARRITO = 1;
        private const int COL_PROVEEDOR = 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT IdProducto,
                         Nombre,
                         Categoria,
                         Marca,
                         Descripcion,
                         Proveedor,
                         Precio,
                         StockActual,
                         StockMinimo
                  FROM dbo.Productos
                  WHERE EsActivo = 1", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            // LINQ sobre DataTable (para cumplir el punto de LINQ)
            var query = dt.AsEnumerable();

            string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            bool esCliente = (rol == "CLIENTE");

            // El cliente solo ve productos con stock
            if (esCliente)
            {
                query = query.Where(r => r.Field<int>("StockActual") > 0);
            }

            // Ordenamos por nombre
            query = query.OrderBy(r => r.Field<string>("Nombre"));

            DataTable dtResultado = query.Any() ? query.CopyToDataTable() : dt.Clone();

            gvProductos.DataSource = dtResultado;
            gvProductos.DataBind();

            ConfigurarColumnasPorRol();
        }

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
            string descripcion = txtDescripcion.Text.Trim();
            string proveedor = txtProveedor.Text.Trim();

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
                      (Nombre, Descripcion, Precio, StockActual, StockMinimo, Categoria, Marca, Proveedor, EsActivo)
                      VALUES
                      (@Nombre, @Descripcion, @Precio, @StockActual, @StockMinimo, @Categoria, @Marca, @Proveedor, 1)", cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion",
                        string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@StockActual", stockActual);
                    cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);
                    cmd.Parameters.AddWithValue("@Categoria",
                        string.IsNullOrWhiteSpace(categoria) ? (object)DBNull.Value : categoria);
                    cmd.Parameters.AddWithValue("@Marca",
                        string.IsNullOrWhiteSpace(marca) ? (object)DBNull.Value : marca);
                    cmd.Parameters.AddWithValue("@Proveedor",
                        string.IsNullOrWhiteSpace(proveedor) ? (object)DBNull.Value : proveedor);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.CssClass = "text-success d-block mb-2";
                lblMensaje.Text = "Producto registrado correctamente.";

                txtNombre.Text = txtCategoria.Text = txtMarca.Text = "";
                txtPrecio.Text = txtStockActual.Text = txtStockMinimo.Text = "";
                txtDescripcion.Text = "";
                txtProveedor.Text = "";

                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-2";
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }

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

        protected void gvProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProductos.EditIndex = -1;
            CargarProductos();
        }

        protected void gvProductos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string rol = (Session["Rol"] ?? "").ToString();
            if (rol != "ADMIN")
            {
                lblMensaje.Text = "No tiene permisos para actualizar productos.";
                e.Cancel = true;
                return;
            }

            int idProducto = -1;

            if (gvProductos.DataKeys != null &&
                gvProductos.DataKeys.Count > e.RowIndex &&
                gvProductos.DataKeys[e.RowIndex].Value != null)
            {
                int.TryParse(gvProductos.DataKeys[e.RowIndex].Value.ToString(), out idProducto);
            }

            if (idProducto <= 0)
            {
                GridViewRow rowKey = gvProductos.Rows[e.RowIndex];
                int.TryParse(rowKey.Cells[2].Text, out idProducto);
            }

            if (idProducto <= 0)
            {
                lblMensaje.Text = "No se pudo identificar el producto a actualizar.";
                e.Cancel = true;
                return;
            }

            string nombre = (e.NewValues["Nombre"] ?? "").ToString().Trim();
            string categoria = (e.NewValues["Categoria"] ?? "").ToString().Trim();
            string marca = (e.NewValues["Marca"] ?? "").ToString().Trim();
            string descripcion = (e.NewValues["Descripcion"] ?? "").ToString().Trim();
            string proveedor = (e.NewValues["Proveedor"] ?? "").ToString().Trim();

            string precioTexto = (e.NewValues["Precio"] ?? "").ToString();
            string stockActualTexto = (e.NewValues["StockActual"] ?? "").ToString();
            string stockMinimoTexto = (e.NewValues["StockMinimo"] ?? "").ToString();

            if (!decimal.TryParse(precioTexto, out decimal precio) ||
                !int.TryParse(stockActualTexto, out int stockActual) ||
                !int.TryParse(stockMinimoTexto, out int stockMinimo))
            {
                lblMensaje.Text = "Verifique precio y cantidades al actualizar.";
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "El nombre del producto es obligatorio.";
                e.Cancel = true;
                return;
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(
                    @"UPDATE dbo.Productos
                      SET Nombre      = @Nombre,
                          Categoria   = @Categoria,
                          Marca       = @Marca,
                          Descripcion = @Descripcion,
                          Proveedor   = @Proveedor,
                          Precio      = @Precio,
                          StockActual = @StockActual,
                          StockMinimo = @StockMinimo
                      WHERE IdProducto = @IdProducto", cn))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Categoria",
                        string.IsNullOrWhiteSpace(categoria) ? (object)DBNull.Value : categoria);
                    cmd.Parameters.AddWithValue("@Marca",
                        string.IsNullOrWhiteSpace(marca) ? (object)DBNull.Value : marca);
                    cmd.Parameters.AddWithValue("@Descripcion",
                        string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);
                    cmd.Parameters.AddWithValue("@Proveedor",
                        string.IsNullOrWhiteSpace(proveedor) ? (object)DBNull.Value : proveedor);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@StockActual", stockActual);
                    cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvProductos.EditIndex = -1;
                lblMensaje.CssClass = "text-success d-block mb-2";
                lblMensaje.Text = "Producto actualizado correctamente.";

                e.Cancel = true;
                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-2";
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
                e.Cancel = true;
            }
        }

        protected void gvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rol = (Session["Rol"] ?? "").ToString();
            if (rol != "ADMIN")
            {
                lblMensaje.Text = "No tiene permisos para eliminar productos.";
                return;
            }

            int idProducto = -1;

            if (gvProductos.DataKeys != null &&
                gvProductos.DataKeys.Count > e.RowIndex &&
                gvProductos.DataKeys[e.RowIndex].Value != null)
            {
                int.TryParse(gvProductos.DataKeys[e.RowIndex].Value.ToString(), out idProducto);
            }

            if (idProducto <= 0)
            {
                lblMensaje.Text = "No se pudo identificar el producto a eliminar.";
                return;
            }

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

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito")
            {
                string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
                if (rol != "CLIENTE") return;

                if (int.TryParse(e.CommandArgument.ToString(), out int idProducto))
                {
                    var carrito = Session["Carrito"] as List<int> ?? new List<int>();
                    carrito.Add(idProducto);
                    Session["Carrito"] = carrito;

                    lblCarritoMensaje.Text = "Producto añadido al carrito.";
                }
            }
        }

        private void ConfigurarColumnasPorRol()
        {
            string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            bool esAdmin = (rol == "ADMIN");
            bool esCliente = (rol == "CLIENTE");

            pnlAdmin.Visible = esAdmin;

            gvProductos.Columns[COL_ACCIONES].Visible = esAdmin;
            gvProductos.Columns[COL_CARRITO].Visible = esCliente;
            gvProductos.Columns[COL_PROVEEDOR].Visible = esAdmin;

            lblRol.Text = esAdmin
                ? "Rol actual: Administrador (puede gestionar productos)."
                : "Rol actual: Cliente (solo lectura, puede añadir al carrito).";
        }
    }
}
