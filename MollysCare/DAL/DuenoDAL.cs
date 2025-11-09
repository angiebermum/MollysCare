using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MollysCare.Modelos;

namespace MollysCare.DAL
{
    public class DuenoDAL
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public static List<Dueno> ObtenerTodos()
        {
            List<Dueno> lista = new List<Dueno>();

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = "SELECT * FROM Duenos";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dueno d = new Dueno
                    {
                        IdDueno = Convert.ToInt32(reader["IdDueno"]),
                        NombreCompleto = reader["NombreCompleto"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };
                    lista.Add(d);
                }
            }

            return lista;
        }
    }
}
