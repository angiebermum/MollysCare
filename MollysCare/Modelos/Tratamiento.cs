using System;

namespace MollysCare.Modelos
{
    public class Tratamiento
    {
        public int IdTratamiento { get; set; }
        public int IdMascota { get; set; }
        public string TipoTratamiento { get; set; } 
        public DateTime FechaAplicacion { get; set; }
        public string Observaciones { get; set; }
    }
}

