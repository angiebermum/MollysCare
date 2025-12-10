using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MollysCare.Modelos.Reportes
{
    public class ReporteVentasViewModel
    {
        public string Modo { get; set; }              // "DIARIO", "SEMANAL", "MENSUAL", "ANUAL"
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public List<VentaPeriodoDto> VentasPorPeriodo { get; set; }
            = new List<VentaPeriodoDto>();

        public List<ProductoMasVendidoDto> ProductosMasVendidos { get; set; }
            = new List<ProductoMasVendidoDto>();

        public List<ClienteFrecuenteDto> ClientesFrecuentes { get; set; }
            = new List<ClienteFrecuenteDto>();
    }
}