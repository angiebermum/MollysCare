using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MollysCare.Modelos;

namespace MollysCare.DAL
{
    public class TratamientoDAL
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public static void Insertar(Tratamiento t)
        {
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = @"INSERT INTO Tratamientos (IdMascota, TipoTratamiento, FechaAplicacion, Observaciones)
                               VALUES (@IdMascota, @TipoTratamiento, @FechaAplicacion, @Observaciones)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdMascota", t.IdMascota);
                cmd.Parameters.AddWithValue("@TipoTratamiento", t.TipoTratamiento);
                cmd.Parameters.AddWithValue("@FechaAplicacion", t.FechaAplicacion);
                cmd.Parameters.AddWithValue("@Observaciones", t.Observaciones);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Tratamiento> ObtenerPorMascota(int idMascota)
        {
            List<Tratamiento> lista = new List<Tratamiento>();

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string sql = "SELECT * FROM Tratamientos WHERE IdMascota = @IdMascota";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdMascota", idMascota);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tratamiento t = new Tratamiento
                    {
                        IdTratamiento = Convert.ToInt32(reader["IdTratamiento"]),
                        IdMascota = Convert.ToInt32(reader["IdMascota"]),
                        TipoTratamiento = reader["TipoTratamiento"].ToString(),
                        FechaAplicacion = Convert.ToDateTime(reader["FechaAplicacion"]),
                        Observaciones = reader["Observaciones"].ToString()
                    };
                    lista.Add(t);
                }
            }

            return lista;
        }
    }
}
