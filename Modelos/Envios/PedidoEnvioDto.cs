using System;

namespace MollysCare.Modelos.Envios
{

    public class PedidoEnvioDto
    {
        public int IdPedido { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}
