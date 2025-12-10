using System;
using System.Globalization;
using System.Web.UI;
using MollysCare.Controladores;
using MollysCare.Modelos.Pagos;

namespace MollysCare.Formularios
{
    public partial class Pago : Page
    {
        private readonly PagoController _controller = new PagoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1) Intentamos tomar el total del carrito desde sesión
                //    (ajusta el nombre de la sesión según tu proyecto)
                decimal total = 0;

                if (Session["TotalCarrito"] is decimal totalCarrito)
                {
                    total = totalCarrito;
                    lblDetalleMonto.Text = "Monto tomado automáticamente del carrito.";
                }
                else
                {
                    // Si no hay total en sesión, puedes probar con un valor de ejemplo
                    total = 10000m;
                    lblDetalleMonto.Text = "Monto de ejemplo (no se encontró TotalCarrito en sesión).";
                }

                txtMonto.Text = total.ToString("N2", new CultureInfo("es-CR"));

                // Si tienes el correo del usuario en sesión, lo precargas
                if (Session["Correo"] is string correo && !string.IsNullOrWhiteSpace(correo))
                {
                    txtCorreo.Text = correo;
                }

                txtDescripcion.Text = "Pago de pedido Molly's Care.";
            }
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            lblMensaje.CssClass = "d-block mb-3";

            if (!decimal.TryParse(
                    txtMonto.Text,
                    NumberStyles.Any,
                    new CultureInfo("es-CR"),
                    out var monto) || monto <= 0)
            {
                lblMensaje.Text = "El monto a pagar no es válido.";
                lblMensaje.CssClass += " text-danger";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                lblMensaje.Text = "Debe indicar un correo electrónico.";
                lblMensaje.CssClass += " text-danger";
                return;
            }

            var vm = new PagoViewModel
            {
                Monto = monto,
                Metodo = ddlMetodo.SelectedValue,
                CorreoCliente = txtCorreo.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            vm = _controller.ProcesarPago(vm);

            if (vm.FueExitoso == true)
            {
                lblMensaje.Text = $"✅ {vm.MensajeResultado} " +
                                  $"ID transacción: <strong>{vm.IdTransaccion}</strong>";
                lblMensaje.CssClass += " text-success";

                
            }
            else
            {
                lblMensaje.Text = $"❌ {vm.MensajeResultado}";
                lblMensaje.CssClass += " text-danger";
            }
        }
    }
}
