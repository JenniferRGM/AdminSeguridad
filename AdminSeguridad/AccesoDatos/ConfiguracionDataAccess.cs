using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;


namespace AdminSeguridad.AccesoDatos
{
    public  class ConfiguracionDataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todas las configuraciones
        public static List<Configuracion> ObtenerTodasLasConfiguraciones()
        {
            List<Configuracion> configuraciones = new List<Configuracion>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN PARA INCLUIR EL USUARIO QUE CREÓ CADA CONFIGURACIÓN
                string query = @"
                    SELECT c.ID, c.Descripcion, c.Valor, c.FechaCreacion, c.FechaActualizacion, u.Nombre AS UsuarioNombre
                    FROM Configuraciones c
                    INNER JOIN Usuarios u ON c.UsuarioID = u.UsuarioID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    configuraciones.Add(new Configuracion
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Valor = reader.GetString(2),
                        FechaCreacion = reader.GetDateTime(3),
                        FechaActualizacion = reader.GetDateTime(4),
                        UsuarioNombre = reader.GetString(5)
                    });
                }
            }
            return configuraciones;
        }

        // Método para obtener una configuración por ID
        public static Configuracion ObtenerConfiguracionPorID(int id)
        {
            Configuracion configuracion = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN PARA INCLUIR EL USUARIO PARA CONSULTAR LA CONFIGURACION ESPECIFICA
                string query = @"
                    SELECT c.ID, c.Descripcion, c.Valor, c.FechaCreacion, c.FechaActualizacion, u.Nombre AS UsuarioNombre
                    FROM Configuraciones c
                    INNER JOIN Usuarios u ON c.UsuarioID = u.UsuarioID
                    WHERE c.ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    configuracion = new Configuracion
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Valor = reader.GetString(2),
                        FechaCreacion = reader.GetDateTime(3),
                        FechaActualizacion = reader.GetDateTime(4),
                        UsuarioNombre = reader.GetString(5)

                    };
                }
            }
            return configuracion;
        }

        // Método para modificar una configuración existente
        public static bool ModificarConfiguracion(Configuracion configuracion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Configuraciones SET Descripcion = @Descripcion, Valor = @Valor, FechaActualizacion = @FechaActualizacion WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", configuracion.ID);
                command.Parameters.AddWithValue("@Descripcion", configuracion.Descripcion);
                command.Parameters.AddWithValue("@Valor", configuracion.Valor);
                command.Parameters.AddWithValue("@FechaActualizacion", configuracion.FechaActualizacion);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}