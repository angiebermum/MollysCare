using System;

namespace MollysCare.Modelos
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdEspecie { get; set; }
        public int IdRaza { get; set; }
        public int IdDueno { get; set; }
    }
}

