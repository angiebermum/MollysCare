namespace MollysCare.Modelos.Pagos
{
  
    public class PagoRequestDto
    {
        public decimal MontoTotal { get; set; }
        public string Moneda { get; set; } = "CRC";   
        public string Metodo { get; set; }            
        public string Descripcion { get; set; }
        public string CorreoCliente { get; set; }
    }
}
