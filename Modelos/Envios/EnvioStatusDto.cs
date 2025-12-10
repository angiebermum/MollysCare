using System;

namespace MollysCare.Modelos.Envios
{
  
    public class EnvioStatusDto
    {
        public int IdPedido { get; set; }
        public string EstadoActual { get; set; }
        public string NuevoEstado { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
