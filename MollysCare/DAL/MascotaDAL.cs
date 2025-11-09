using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MollysCare.Modelos;

namespace MollysCare.DAL
{
    public class MascotaDAL
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public static void Insertar(Mascota mascota)
        {
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = @"INSERT INTO Mascotas (Nombre, FechaNacimiento, IdEspecie, IdRaza, IdDueno)
                               VALUES (@Nombre, @FechaNacimiento, @IdEspecie, @IdRaza, @IdDueno)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                cmd.Parameters.AddWithValue("@FechaNacimiento", mascota.FechaNacimiento);
                cmd.Parameters.AddWithValue("@IdEspecie", mascota.IdEspecie);
                cmd.Parameters.AddWithValue("@IdRaza", mascota.IdRaza);
                cmd.Parameters.AddWithValue("@IdDueno", mascota.IdDueno);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Mascota> ObtenerTodas()
        {
            List<Mascota> lista = new List<Mascota>();

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = "SELECT * FROM Mascotas";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mascota m = new Mascota
                    {
                        IdMascota = Convert.ToInt32(reader["IdMascota"]),
                        Nombre = reader["Nombre"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        IdEspecie = Convert.ToInt32(reader["IdEspecie"]),
                        IdRaza = Convert.ToInt32(reader["IdRaza"]),
                        IdDueno = Convert.ToInt32(reader["IdDueno"])
                    };
                    lista.Add(m);
                }
            }

            return lista;
        }
    }
}
