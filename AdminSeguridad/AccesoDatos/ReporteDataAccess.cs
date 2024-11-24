using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;
using AdminSeguridad.PaginasWeb;

namespace AdminSeguridad.AccesoDatos
{
    public class ReporteDataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todos los reportes
        public static List<Reporte> ObtenerTodosLosReportes()
        {
            List<Reporte> reportes = new List<Reporte>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN PARA VERIFICAR QUE USUARIO GENERÓ EL REPORTE
                string query = @"
                    SELECT r.ID, r.Descripcion, r.Fecha, u.Nombre AS UsuarioNombre
                    FROM Reportes r
                    INNER JOIN Usuarios u ON r.UsuarioID = u.UsuarioID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    reportes.Add(new Reporte
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        UsuarioNombre = reader.GetString(3)
                    });
                }
            }
            return reportes;
        }

        // Método para obtener un reporte por ID
        public static Reporte ObtenerReportePorID(int id)
        {
            Reporte reporte = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT r.ID, r.Descripcion, r.Fecha, u.Nombre AS UsuarioNombre
                    FROM Reportes r
                    INNER JOIN Usuarios u ON r.UsuarioID = u.UsuarioID
                    WHERE r.ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reporte = new Reporte
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        UsuarioNombre = reader.GetString(3)
                    };
                }
            }
            return reporte;
        }
    }
}