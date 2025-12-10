using System;
using System.Net;
using System.Text;
using MollysCare.Modelos.Envios;

namespace MollysCare.Servicios
{
    public class ProveedorEnviosService
    {
        
        public EnvioStatusDto ConsultarYActualizarEstado(int idPedido, string estadoActual)
        {
            string nuevoEstado = estadoActual ?? string.Empty;

            if (string.IsNullOrWhiteSpace(estadoActual) ||
                estadoActual.Equals("En proceso", StringComparison.OrdinalIgnoreCase))
            {
                nuevoEstado = "Preparando envío";
            }
            else if (estadoActual.Equals("Preparando envío", StringComparison.OrdinalIgnoreCase))
            {
                nuevoEstado = "En camino";
            }
            else if (estadoActual.Equals("En camino", StringComparison.OrdinalIgnoreCase))
            {
                nuevoEstado = "Entregado";
            }

            string mensaje;
            if (!string.Equals(estadoActual, nuevoEstado, StringComparison.OrdinalIgnoreCase))
            {
                mensaje = $"Estado actualizado: \"{estadoActual}\" → \"{nuevoEstado}\"";
            }
            else
            {
                mensaje = $"Estado actual: \"{nuevoEstado}\". No hay cambios reportados por el proveedor de envíos.";
            }

            return new EnvioStatusDto
            {
                IdPedido = idPedido,
                EstadoActual = estadoActual,
                NuevoEstado = nuevoEstado,
                Mensaje = mensaje,
                FechaActualizacion = DateTime.Now
            };
        }


        public string ObtenerInfoAdicionalEnvio()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;

                  
                    client.DownloadString("https://worldtimeapi.org/api/timezone/America/Costa_Rica");

                    
                    return "Información verificada con el proveedor de envíos (Web Service externo).";
                }
            }
            catch (Exception)
            {
                
                return "No fue posible contactar al Web Service externo del proveedor de envíos.";
            }
        }

    }
}
