<%@ WebHandler Language="C#" Class="ObtenerMascotas" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class ObtenerMascotas : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        List<object> mascotas = new List<object>();

        string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(cadena))
        {
            string sql = @"
                SELECT m.IdMascota, m.Nombre, m.FechaNacimiento,
                       e.Nombre AS Especie, r.Nombre AS Raza, d.NombreCompleto AS Dueno
                FROM Mascotas m
                INNER JOIN Especies e ON m.IdEspecie = e.IdEspecie
                INNER JOIN Razas r ON m.IdRaza = r.IdRaza
                INNER JOIN Duenos d ON m.IdDueno = d.IdDueno";

            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                mascotas.Add(new
                {
                    IdMascota = reader["IdMascota"],
                    Nombre = reader["Nombre"],
                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]).ToString("yyyy-MM-dd"),
                    Especie = reader["Especie"],
                    Raza = reader["Raza"],
                    Dueno = reader["Dueno"]
                });
            }
        }

        var json = new JavaScriptSerializer().Serialize(mascotas);
        context.Response.Write(json);
    }

    public bool IsReusable => false;
}
