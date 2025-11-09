using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MollysCare.Modelos;

namespace MollysCare.DAL
{
    public class RazaDAL
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public static List<Raza> ObtenerPorEspecie(int idEspecie)
        {
            List<Raza> lista = new List<Raza>();

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = "SELECT * FROM Razas WHERE IdEspecie = @IdEspecie";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdEspecie", idEspecie);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Raza r = new Raza
                    {
                        IdRaza = Convert.ToInt32(reader["IdRaza"]),
                        Nombre = reader["Nombre"].ToString(),
                        IdEspecie = Convert.ToInt32(reader["IdEspecie"])
                    };
                    lista.Add(r);
                }
            }

            return lista;
        }
    }
}

