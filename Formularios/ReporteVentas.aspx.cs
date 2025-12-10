using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using MollysCare.Controladores;

namespace MollysCare.Formularios
{
    public partial class ReporteVentas : Page
    {
        private readonly ReportesVentasController _controller = new ReportesVentasController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var hoy = DateTime.Today;
                txtHasta.Text = hoy.ToString("yyyy-MM-dd");
                txtDesde.Text = hoy.AddDays(-7).ToString("yyyy-MM-dd");
                ddlModo.SelectedValue = "DIARIO";

                GenerarReporte();
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            lblError.Text = string.Empty;

            if (!DateTime.TryParse(txtDesde.Text, out var desde))
            {
                lblError.Text = "Debe indicar una fecha 'Desde' válida.";
                LimpiarControles();
                return;
            }

            if (!DateTime.TryParse(txtHasta.Text, out var hasta))
            {
                lblError.Text = "Debe indicar una fecha 'Hasta' válida.";
                LimpiarControles();
                return;
            }

            if (hasta < desde)
            {
                lblError.Text = "La fecha 'Hasta' no puede ser menor que la fecha 'Desde'.";
                LimpiarControles();
                return;
            }

            var modo = ddlModo.SelectedValue;

            try
            {
                var vm = _controller.GenerarReporte(modo, desde, hasta, top: 5);

                // TABLAS
                gvVentasPeriodo.DataSource = vm.VentasPorPeriodo;
                gvVentasPeriodo.DataBind();

                gvProductos.DataSource = vm.ProductosMasVendidos;
                gvProductos.DataBind();

                gvClientes.DataSource = vm.ClientesFrecuentes;
                gvClientes.DataBind();

                // HIDDENFIELDS PARA JS
                hfVentasPeriodos.Value = string.Join("|",
                    vm.VentasPorPeriodo.Select(v => v.EtiquetaPeriodo));

                hfVentasMontos.Value = string.Join("|",
                    vm.VentasPorPeriodo.Select(v =>
                        v.TotalVentas.ToString(CultureInfo.InvariantCulture)));

                hfGananciasMontos.Value = string.Join("|",
                    vm.VentasPorPeriodo.Select(v =>
                        v.Ganancia.ToString(CultureInfo.InvariantCulture)));

                hfProductosNombres.Value = string.Join("|",
                    vm.ProductosMasVendidos.Select(p => p.Nombre));

                hfProductosCantidades.Value = string.Join("|",
                    vm.ProductosMasVendidos.Select(p =>
                        p.CantidadVendida.ToString(CultureInfo.InvariantCulture)));

                hfClientesNombres.Value = string.Join("|",
                    vm.ClientesFrecuentes.Select(c => c.Cliente));

                hfClientesPedidos.Value = string.Join("|",
                    vm.ClientesFrecuentes.Select(c =>
                        c.NumeroPedidos.ToString(CultureInfo.InvariantCulture)));
            }
            catch (Exception ex)
            {
                LimpiarControles();
                lblError.Text = "Se produjo un error al generar el informe: " + ex.Message;
            }
        }

        private void LimpiarControles()
        {
            gvVentasPeriodo.DataSource = null;
            gvVentasPeriodo.DataBind();

            gvProductos.DataSource = null;
            gvProductos.DataBind();

            gvClientes.DataSource = null;
            gvClientes.DataBind();

            hfVentasPeriodos.Value = string.Empty;
            hfVentasMontos.Value = string.Empty;
            hfGananciasMontos.Value = string.Empty;
            hfProductosNombres.Value = string.Empty;
            hfProductosCantidades.Value = string.Empty;
            hfClientesNombres.Value = string.Empty;
            hfClientesPedidos.Value = string.Empty;
        }
    }
}
