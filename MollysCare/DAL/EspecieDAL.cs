using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MollysCare.Modelos;

namespace MollysCare.DAL
{
    public class EspecieDAL
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public static List<Especie> ObtenerTodas()
        {
            List<Especie> lista = new List<Especie>();

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = "SELECT * FROM Especies";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Especie e = new Especie
                    {
                        IdEspecie = Convert.ToInt32(reader["IdEspecie"]),
                        Nombre = reader["Nombre"].ToString()
                    };
                    lista.Add(e);
                }
            }

            return lista;
        }
    }
}
