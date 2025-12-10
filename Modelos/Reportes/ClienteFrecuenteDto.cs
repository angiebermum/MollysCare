using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MollysCare.Modelos.Reportes
{
    public class ClienteFrecuenteDto
    {
        public string Cliente { get; set; }        // viene del campo Pedido.Usuario
        public int NumeroPedidos { get; set; }
        public decimal TotalComprado { get; set; }
    }
}