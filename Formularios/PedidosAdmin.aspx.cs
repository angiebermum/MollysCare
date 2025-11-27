using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MollysCare.Formularios
{
    public partial class PedidosAdmin : Page
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
            if (rol != "ADMIN")
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "Solo el administrador puede gestionar los pedidos.";
                gvPedidos.Visible = false;
                return;
            }

            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            using (SqlConnection cn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT IdPedido, Usuario, Fecha, Total, Estado
                  FROM dbo.Pedidos
                  ORDER BY Fecha DESC", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPedidos.DataSource = dt;
                gvPedidos.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMensaje.CssClass = "text-muted d-block mb-3";
                    lblMensaje.Text = "No hay pedidos registrados.";
                }
                else
                {
                    lblMensaje.CssClass = "text-muted d-block mb-3";
                    lblMensaje.Text = "Seleccione un pedido y edite el estado para actualizarlo.";
                }
            }
        }

        protected void gvPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPedidos.EditIndex = e.NewEditIndex;
            CargarPedidos();
        }

        protected void gvPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPedidos.EditIndex = -1;
            CargarPedidos();
        }

        protected void gvPedidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idPedido = Convert.ToInt32(gvPedidos.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvPedidos.Rows[e.RowIndex];

            DropDownList ddlEstado = row.FindControl("ddlEstado") as DropDownList;
            if (ddlEstado == null)
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "No se pudo recuperar el estado seleccionado.";
                e.Cancel = true;
                return;
            }

            string nuevoEstado = ddlEstado.SelectedValue;

            try
            {
                using (SqlConnection cn = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand(
                    @"UPDATE dbo.Pedidos
                      SET Estado = @Estado
                      WHERE IdPedido = @IdPedido", cn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvPedidos.EditIndex = -1;
                lblMensaje.CssClass = "text-success d-block mb-3";
                lblMensaje.Text = "Estado del pedido actualizado correctamente.";
                e.Cancel = true;
                CargarPedidos();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "Error al actualizar el estado: " + ex.Message;
                e.Cancel = true;
            }
        }

        protected void gvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if ((e.Row.RowType == DataControlRowType.DataRow) &&
                (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                var ddl = e.Row.FindControl("ddlEstado") as DropDownList;
                if (ddl != null)
                {
                    string estadoActual = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
                    ListItem item = ddl.Items.FindByValue(estadoActual);
                    if (item != null)
                    {
                        ddl.ClearSelection();
                        item.Selected = true;
                    }
                }
            }
        }
    }
}
