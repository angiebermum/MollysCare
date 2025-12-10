using System.Collections.Generic;

namespace MollysCare.Modelos.Envios
{
    
    public class EnviosViewModel
    {
        public List<PedidoEnvioDto> Pedidos { get; set; } = new List<PedidoEnvioDto>();
        public string Mensaje { get; set; }
        public bool EsExitoso { get; set; }
    }
}
