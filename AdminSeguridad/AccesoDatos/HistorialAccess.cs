using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AdminSeguridad.Modelos;

namespace AdminSeguridad.AccesoDatos
{
    public class HistorialAccess
    {
        public static List<Historial> ObtenerHistorial()
        {
            var historial = new List<Historial>();
            string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ID, UsuarioID, Descripcion, FechaCreacion, Modulo, TipoEvento FROM Datos";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    historial.Add(new Historial
                    {
                        ID = reader.GetInt32(0),
                        UsuarioID = reader.GetInt32(1).ToString(),
                        Descripcion = reader.GetString(2),
                        FechaCreacion = reader.GetDateTime(3),
                        Modulo = reader.GetString(4),
                        TipoEvento = reader.GetString(5)
                    });
                }
            }

            return historial;
        }
    }
}