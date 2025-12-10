using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MollysCare.Controladores;

namespace MollysCare.Formularios
{
    public partial class SeguimientoEnvios : Page
    {
        private readonly EnviosController _controller = new EnviosController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            var rol = (Session["Rol"] ?? "").ToString().ToUpperInvariant();
            if (rol != "ADMIN")
            {
                Response.Redirect("Menu.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            var vm = _controller.ObtenerPedidosParaGestion();

            gvEnvios.DataSource = vm.Pedidos;
            gvEnvios.DataBind();

            lblMensaje.Text = vm.Mensaje;
            lblMensaje.CssClass = "envios-meta d-block mb-3" +
                                  (!string.IsNullOrWhiteSpace(vm.Mensaje)
                                      ? (vm.EsExitoso ? " text-success" : " text-danger")
                                      : "");
        }

        protected void gvEnvios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ActualizarEstado")
            {
                int idPedido;
                if (int.TryParse(e.CommandArgument.ToString(), out idPedido))
                {
                    var vm = _controller.ActualizarEstadoDesdeWebServiceAdmin(idPedido);

                    gvEnvios.DataSource = vm.Pedidos;
                    gvEnvios.DataBind();

                    lblMensaje.Text = vm.Mensaje;
                    lblMensaje.CssClass = "envios-meta d-block mb-3 " +
                                          (vm.EsExitoso ? "text-success" : "text-danger");
                }
            }
        }
    }
}

