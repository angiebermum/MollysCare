using System;
using MollysCare.Modelos.Pagos;

namespace MollysCare.Servicios
{
    
    public class PasarelaPagoService
    {
        public PagoResultadoDto ProcesarPago(PagoRequestDto request)
        {

            var metodo = string.IsNullOrWhiteSpace(request.Metodo)
                ? "PAYPAL"
                : request.Metodo.ToUpperInvariant();

            bool aprobado = request.MontoTotal > 0;

            return new PagoResultadoDto
            {
                Exitoso = aprobado,
                IdTransaccion = $"{metodo}-SIM-{Guid.NewGuid().ToString("N").Substring(0, 10)}",
                Mensaje = aprobado
                    ? $"Pago aprobado por {metodo} (simulado)."
                    : $"Pago rechazado por {metodo} (monto inválido)."
            };
        }
    }
}
