using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;

namespace AdminSeguridad.AccesoDatos
{
    public static class DashboardDataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        public static List<Dato> ObtenerTodosLosDatos()
        {
            List<Dato> datos = new List<Dato>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                //INNER JOIN PARA COMBINAR LA TABLA DATOS Y CATEGORIAS
                string query = @"
                   SELECT d.ID, d.Descripcion, d.FechaCreacion, d.FechaActualizacion, c.Nombre AS CategoriaNombre
                   FROM Datos d
                   INNER JOIN Categorias c ON d.CategoriaID = c.CategoriaID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    datos.Add(new Dato
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        FechaCreacion = reader.GetDateTime(2),
                        FechaActualizacion = reader.GetDateTime(3),
                        CategoriaNombre = reader.GetString(4)
                    });
                }
            }
            return datos;
        }
        public static Dato ObtenerDatoPorID(int id)
        {
            Dato dato = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN PARA COMBINAR LA TABLA DATOS Y CATEGORIAS POR ID
                string query = @"
                    SELECT d.ID, d.Descripcion, d.FechaCreacion, d.FechaActualizacion, c.Nombre AS CategoriaNombre
                    FROM Datos d
                    INNER JOIN Categorias c ON d.CategoriaID = c.CategoriaID
                    WHERE d.ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dato = new Dato
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        FechaCreacion = reader.GetDateTime(2),
                        FechaActualizacion = reader.GetDateTime(3),
                        CategoriaNombre = reader.GetString(4)
                    };
                }
            }
            return dato;
        }

        public static bool AgregarDato(Dato dato)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Datos (Descripcion, FechaCreacion, FechaActualizacion) VALUES (@Descripcion, @FechaCreacion, @FechaActualizacion)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Descripcion", dato.Descripcion);
                command.Parameters.AddWithValue("@FechaCreacion", dato.FechaCreacion);
                command.Parameters.AddWithValue("@FechaActualizacion", dato.FechaActualizacion);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool ModificarDato(Dato dato)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Datos SET Descripcion = @Descripcion, FechaActualizacion = @FechaActualizacion WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", dato.ID);
                command.Parameters.AddWithValue("@Descripcion", dato.Descripcion);
                command.Parameters.AddWithValue("@FechaActualizacion", dato.FechaActualizacion);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool EliminarDato(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Datos WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}