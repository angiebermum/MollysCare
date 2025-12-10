using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MollysCare.Modelos.Reportes
{
    public class ProductoMasVendidoDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int CantidadVendida { get; set; }
        public decimal MontoTotal { get; set; }
    }
}