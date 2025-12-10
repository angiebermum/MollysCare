using DocumentFormat.OpenXml.Wordprocessing;
using MollysCare.Modelos.Reportes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;

namespace MollysCare.Data
{
    public class ReportesVentasRepository
    {
        private readonly string _connectionString;

        public ReportesVentasRepository()
        {
           
            _connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
           
        }

        public List<VentaPeriodoDto> ObtenerVentasPorPeriodo(DateTime desde, DateTime hasta, string modo)
        {
            var lista = new List<VentaPeriodoDto>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Reporte_VentasPorPeriodo", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaDesde", desde.Date);
                cmd.Parameters.AddWithValue("@FechaHasta", hasta.Date);
                cmd.Parameters.AddWithValue("@Modo", modo.ToUpperInvariant());

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new VentaPeriodoDto
                        {
                            EtiquetaPeriodo = dr["EtiquetaPeriodo"].ToString(),
                            TotalVentas = dr.GetDecimal(dr.GetOrdinal("TotalVentas")),
                            Ganancia = dr.GetDecimal(dr.GetOrdinal("Ganancia"))
                        });
                    }
                }
            }

            return lista;
        }

        public List<ProductoMasVendidoDto> ObtenerProductosMasVendidos(DateTime desde, DateTime hasta, int top = 10)
        {
            var lista = new List<ProductoMasVendidoDto>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Reporte_ProductosMasVendidos", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaDesde", desde.Date);
                cmd.Parameters.AddWithValue("@FechaHasta", hasta.Date);
                cmd.Parameters.AddWithValue("@Top", top);

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ProductoMasVendidoDto
                        {
                            IdProducto = (int)dr["IdProducto"],
                            Nombre = dr["Nombre"].ToString(),
                            CantidadVendida = (int)dr["CantidadVendida"],
                            MontoTotal = dr.GetDecimal(dr.GetOrdinal("MontoTotal"))
                        });
                    }
                }
            }

            return lista;
        }

        public List<ClienteFrecuenteDto> ObtenerClientesFrecuentes(DateTime desde, DateTime hasta, int top = 10)
        {
            var lista = new List<ClienteFrecuenteDto>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Reporte_ClientesFrecuentes", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaDesde", desde.Date);
                cmd.Parameters.AddWithValue("@FechaHasta", hasta.Date);
                cmd.Parameters.AddWithValue("@Top", top);

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ClienteFrecuenteDto
                        {
                            Cliente = dr["Cliente"].ToString(),
                            NumeroPedidos = (int)dr["NumeroPedidos"],
                            TotalComprado = dr.GetDecimal(dr.GetOrdinal("TotalComprado"))
                        });
                    }
                }
            }

            return lista;
        }
    }
}
