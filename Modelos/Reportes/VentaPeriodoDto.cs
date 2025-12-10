using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MollysCare.Modelos.Reportes
{
    public class VentaPeriodoDto
    {
        public string EtiquetaPeriodo { get; set; }   // "2025-01-01", "2025-01", "2025-W42", etc.
        public decimal TotalVentas { get; set; }
        public decimal Ganancia { get; set; }
    }
}