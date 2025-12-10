using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MollysCare.Modelos.Envios;

namespace MollysCare.Data
{
    public class EnviosRepository
    {
        private readonly string _cs;

        public EnviosRepository()
        {
            _cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public List<PedidoEnvioDto> ObtenerPedidosPorUsuario(string usuario)
        {
            var lista = new List<PedidoEnvioDto>();

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                @"SELECT IdPedido, Total, Estado
                  FROM dbo.Pedidos
                  WHERE Usuario = @Usuario
                  ORDER BY IdPedido DESC;", cn))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PedidoEnvioDto
                        {
                            IdPedido = dr.GetInt32(dr.GetOrdinal("IdPedido")),
                            Total = dr.GetDecimal(dr.GetOrdinal("Total")),
                            Estado = dr["Estado"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public List<PedidoEnvioDto> ObtenerTodosLosPedidos()
        {
            var lista = new List<PedidoEnvioDto>();

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                @"SELECT IdPedido, Total, Estado
                  FROM dbo.Pedidos
                  ORDER BY IdPedido DESC;", cn))
            {
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PedidoEnvioDto
                        {
                            IdPedido = dr.GetInt32(dr.GetOrdinal("IdPedido")),
                            Total = dr.GetDecimal(dr.GetOrdinal("Total")),
                            Estado = dr["Estado"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public PedidoEnvioDto ObtenerPedidoPorId(int idPedido)
        {
            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                @"SELECT IdPedido, Total, Estado
                  FROM dbo.Pedidos
                  WHERE IdPedido = @IdPedido;", cn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new PedidoEnvioDto
                        {
                            IdPedido = dr.GetInt32(dr.GetOrdinal("IdPedido")),
                            Total = dr.GetDecimal(dr.GetOrdinal("Total")),
                            Estado = dr["Estado"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public void ActualizarEstadoPedido(int idPedido, string nuevoEstado)
        {
            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                @"UPDATE dbo.Pedidos
                  SET Estado = @Estado
                  WHERE IdPedido = @IdPedido;", cn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
