using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;
using System.Linq;
using System.Web;

namespace AdminSeguridad.AccesoDatos
{
    public class ProvinciaDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todas las provincias
        public List<Provincia> ObtenerProvincias()
        {
            List<Provincia> provincias = new List<Provincia>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Provincias", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Provincia provincia = new Provincia
                    {
                        ProvinciaID = Convert.ToInt32(reader["ProvinciaID"]),
                        NombreProvincia = reader["NombreProvincia"].ToString()
                    };
                    provincias.Add(provincia);
                }
            }
            return provincias;
        }

        // Método para obtener una provincia por su ID
        public Provincia ObtenerProvinciaPorID(int provinciaID)
        {
            Provincia provincia = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Provincias WHERE ProvinciaID = @ProvinciaID", connection);
                cmd.Parameters.AddWithValue("@ProvinciaID", provinciaID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    provincia = new Provincia
                    {
                        ProvinciaID = Convert.ToInt32(reader["ProvinciaID"]),
                        NombreProvincia = reader["NombreProvincia"].ToString()
                    };
                }
            }
            return provincia;
        }

        // Método para obtener provincias con cantones
        public List<Provincia> ObtenerProvinciasConCantones()
        {
            List<Provincia> provincias = new List<Provincia>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT p.ProvinciaID, p.NombreProvincia, c.CantonID, c.NombreCanton 
            FROM Provincias p
            INNER JOIN Cantones c ON p.ProvinciaID = c.ProvinciaID";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int provinciaID = Convert.ToInt32(reader["ProvinciaID"]);
                    Provincia provincia = provincias.FirstOrDefault(p => p.ProvinciaID == provinciaID);
                    if (provincia == null)
                    {
                        provincia = new Provincia
                        {
                            ProvinciaID = provinciaID,
                            NombreProvincia = reader["NombreProvincia"].ToString(),
                            Cantones = new List<Canton>() // Asegúrate de que Provincia tenga esta propiedad
                        };
                        provincias.Add(provincia);
                    }

                    // Agregar canton a la lista de cantones de la provincia
                    provincia.Cantones.Add(new Canton
                    {
                        CantonID = Convert.ToInt32(reader["CantonID"]),
                        NombreCanton = reader["NombreCanton"].ToString()
                    });
                }
            }
            return provincias;
        }


        // Método para agregar una nueva provincia
        public bool AgregarProvincia(Provincia provincia)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Provincias (NombreProvincia) VALUES (@NombreProvincia)", connection);
                cmd.Parameters.AddWithValue("@NombreProvincia", provincia.NombreProvincia);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para actualizar una provincia existente
        public bool ActualizarProvincia(Provincia provincia)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Provincias SET NombreProvincia = @NombreProvincia WHERE ProvinciaID = @ProvinciaID", connection);
                cmd.Parameters.AddWithValue("@NombreProvincia", provincia.NombreProvincia);
                cmd.Parameters.AddWithValue("@ProvinciaID", provincia.ProvinciaID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para eliminar una provincia
        public bool EliminarProvincia(int provinciaID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Provincias WHERE ProvinciaID = @ProvinciaID", connection);
                cmd.Parameters.AddWithValue("@ProvinciaID", provinciaID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}