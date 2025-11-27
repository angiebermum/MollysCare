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
    public partial class Carrito : Page
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private class CarritoItem
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public string Categoria { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal { get; set; }
        }

        private List<CarritoItem> ObtenerItemsCarrito(out decimal total)
        {
            total = 0m;

            var listaIds = Session["Carrito"] as List<int>;
            var items = new List<CarritoItem>();

            if (listaIds == null || listaIds.Count == 0)
                return items;

            
            var cantidades = listaIds
                .GroupBy(id => id)
                .Select(g => new { IdProducto = g.Key, Cantidad = g.Count() })
                .ToList();

            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();

                foreach (var c in cantidades)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        @"SELECT IdProducto, Nombre, Categoria, Precio
                          FROM dbo.Productos
                          WHERE IdProducto = @IdProducto AND EsActivo = 1", cn))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", c.IdProducto);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                decimal precio = dr.GetDecimal(dr.GetOrdinal("Precio"));
                                int cantidad = c.Cantidad;
                                decimal subtotal = precio * cantidad;

                                var item = new CarritoItem
                                {
                                    IdProducto = dr.GetInt32(dr.GetOrdinal("IdProducto")),
                                    Nombre = dr["Nombre"].ToString(),
                                    Categoria = dr["Categoria"] != DBNull.Value
                                                ? dr["Categoria"].ToString()
                                                : string.Empty,
                                    Precio = precio,
                                    Cantidad = cantidad,
                                    Subtotal = subtotal
                                };

                                items.Add(item);
                                total += subtotal;
                            }
                        }
                    }
                }
            }

            return items;
        }

        private void CargarCarrito()
        {
            decimal total;
            var items = ObtenerItemsCarrito(out total);

            gvCarrito.DataSource = items;
            gvCarrito.DataBind();

            if (items.Count == 0)
            {
                lblMensaje.CssClass = "text-muted d-block mb-3";
                lblMensaje.Text = "No hay productos en el carrito.";
                lblTotal.Text = "₡0.00";
            }
            else
            {
                lblMensaje.CssClass = "text-success d-block mb-3";
                lblMensaje.Text = "";
                lblTotal.Text = string.Format("₡{0:N2}", total);
            }

            bool hayItems = (items.Count > 0);
            bool esCliente = (Session["Rol"] ?? "").ToString()
                                .ToUpperInvariant() == "CLIENTE";

           
            btnVaciar.Visible = hayItems;
            btnConfirmarPedido.Visible = hayItems && esCliente;
        }

      
        protected void btnVaciar_Click(object sender, EventArgs e)
        {
            Session["Carrito"] = null;
            lblMensaje.CssClass = "text-success d-block mb-3";
            lblMensaje.Text = "Carrito vaciado correctamente.";
            CargarCarrito();
        }

       
        protected void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            
            string rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            if (rol != "CLIENTE")
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "Solo los clientes pueden realizar pedidos.";
                return;
            }

            string usuario = (Session["Usuario"] ?? "").ToString();
            if (string.IsNullOrWhiteSpace(usuario))
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "No se pudo identificar el usuario.";
                return;
            }

            decimal total;
            var items = ObtenerItemsCarrito(out total);

            if (items.Count == 0)
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "No hay productos en el carrito.";
                return;
            }

            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    
                    int idPedido;
                    using (SqlCommand cmdPedido = new SqlCommand(
                        @"INSERT INTO dbo.Pedidos (Usuario, Total, Estado)
                          VALUES (@Usuario, @Total, @Estado);
                          SELECT SCOPE_IDENTITY();", cn, tran))
                    {
                        cmdPedido.Parameters.AddWithValue("@Usuario", usuario);
                        cmdPedido.Parameters.AddWithValue("@Total", total);
                        cmdPedido.Parameters.AddWithValue("@Estado", "En proceso");

                        object result = cmdPedido.ExecuteScalar();
                        idPedido = Convert.ToInt32(result);
                    }

                   
                    foreach (var item in items)
                    {
                        using (SqlCommand cmdDet = new SqlCommand(
                            @"INSERT INTO dbo.PedidoDetalle
                              (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal)
                              VALUES
                              (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)", cn, tran))
                        {
                            cmdDet.Parameters.AddWithValue("@IdPedido", idPedido);
                            cmdDet.Parameters.AddWithValue("@IdProducto", item.IdProducto);
                            cmdDet.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                            cmdDet.Parameters.AddWithValue("@PrecioUnitario", item.Precio);
                            cmdDet.Parameters.AddWithValue("@Subtotal", item.Subtotal);

                            cmdDet.ExecuteNonQuery();
                        }
                    }


                    tran.Commit();

            
                    Session["Carrito"] = null;
                    lblMensaje.CssClass = "text-success d-block mb-3";
                    lblMensaje.Text = "Pedido registrado correctamente. Estado inicial: En proceso.";
                    CargarCarrito();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    lblMensaje.CssClass = "text-danger d-block mb-3";
                    lblMensaje.Text = "Error al registrar el pedido: " + ex.Message;
                }
            }
        }
    }
}
