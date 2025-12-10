namespace MollysCare.Modelos.Pagos
{
   
    public class PagoViewModel
    {
        public decimal Monto { get; set; }
        public string Metodo { get; set; }        
        public string Descripcion { get; set; }
        public string CorreoCliente { get; set; }

        public bool? FueExitoso { get; set; }
        public string MensajeResultado { get; set; }
        public string IdTransaccion { get; set; }
    }
}
