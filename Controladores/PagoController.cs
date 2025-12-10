using MollysCare.Modelos.Pagos;
using MollysCare.Servicios;

namespace MollysCare.Controladores
{
    
    public class PagoController
    {
        private readonly PasarelaPagoService _service;

        public PagoController()
        {
            _service = new PasarelaPagoService();
        }

        public PagoViewModel ProcesarPago(PagoViewModel modelo)
        {
            var request = new PagoRequestDto
            {
                MontoTotal = modelo.Monto,
                Moneda = "CRC",
                Metodo = modelo.Metodo,
                Descripcion = modelo.Descripcion,
                CorreoCliente = modelo.CorreoCliente
            };

            var resultado = _service.ProcesarPago(request);

            modelo.FueExitoso = resultado.Exitoso;
            modelo.MensajeResultado = resultado.Mensaje;
            modelo.IdTransaccion = resultado.IdTransaccion;

            return modelo;
        }
    }
}
