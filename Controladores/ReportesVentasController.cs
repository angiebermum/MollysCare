using System;
using MollysCare.Data;
using MollysCare.Modelos.Reportes;

namespace MollysCare.Controladores
{
 
    public class ReportesVentasController
    {
        private readonly ReportesVentasRepository _repo;

        public ReportesVentasController()
        {
            _repo = new ReportesVentasRepository();
        }

        public ReporteVentasViewModel GenerarReporte(
            string modo,
            DateTime desde,
            DateTime hasta,
            int top = 5)
        {

            if (string.IsNullOrWhiteSpace(modo))
                modo = "DIARIO";

            modo = modo.ToUpperInvariant();

            var vm = new ReporteVentasViewModel
            {
                Modo = modo,
                FechaDesde = desde,
                FechaHasta = hasta
            };

            vm.VentasPorPeriodo = _repo.ObtenerVentasPorPeriodo(desde, hasta, modo);
            vm.ProductosMasVendidos = _repo.ObtenerProductosMasVendidos(desde, hasta, top);
            vm.ClientesFrecuentes = _repo.ObtenerClientesFrecuentes(desde, hasta, top);

            return vm;
        }
    }
}
