<%@ WebHandler Language="C#" Class="ObtenerMascotas" %>

using Newtonsoft.Json;
using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Script.Serialization;


// Código para probarhttps://localhost:44375/ObtenerMascotas.ashx

public class ObtenerMascotas : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var lista = new List<object>();

        string cs = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        int id;
        object idParam = int.TryParse(context.Request["id"], out id) ? (object)id : DBNull.Value;

        string sql = @"
            SELECT m.IdMascota, m.Nombre, m.FechaNacimiento,
                   e.Nombre AS Especie, r.Nombre AS Raza, d.NombreCompleto AS Dueno
            FROM Mascotas m
            INNER JOIN Especies e ON m.IdEspecie = e.IdEspecie
            INNER JOIN Razas r    ON m.IdRaza   = r.IdRaza
            INNER JOIN Duenos d   ON m.IdDueno  = d.IdDueno
            WHERE (@Id IS NULL OR m.IdMascota = @Id)
            ORDER BY m.Nombre;";

        try
        {
            using (SqlConnection conn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter p = cmd.Parameters.Add("@Id", SqlDbType.Int);
                p.Value = idParam;

                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new
                        {
                            IdMascota = rd["IdMascota"],
                            Nombre = rd["Nombre"],
                            FechaNacimiento = Convert.ToDateTime(rd["FechaNacimiento"]).ToString("yyyy-MM-dd"),
                            Especie = rd["Especie"],
                            Raza = rd["Raza"],
                            Dueno = rd["Dueno"]
                        });
                    }
                }
            }

            //var json = new JavaScriptSerializer().Serialize(lista);
            //context.Response.Write(json);
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
                context.Response.Write(json);

        }
        catch
        {
            context.Response.StatusCode = 500;
            context.Response.Write("{\"ok\":false,\"msg\":\"Error del servidor\"}");
        }
    }

    public bool IsReusable { get { return false; } }
}
