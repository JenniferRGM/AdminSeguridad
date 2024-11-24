using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;

namespace AdminSeguridad.AccesoDatos
{
    public class CantonDataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadConnectionString"].ConnectionString;

        // Método para obtener todos los cantones
        public List<Canton> ObtenerCantones()
        {
            List<Canton> cantones = new List<Canton>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cantones", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Canton canton = new Canton
                    {
                        CantonID = Convert.ToInt32(reader["CantonID"]),
                        NombreCanton = reader["NombreCanton"].ToString(),
                        ProvinciaID = Convert.ToInt32(reader["ProvinciaID"])
                    };
                    cantones.Add(canton);
                }
            }
            return cantones;
        }

        // Método para obtener los cantones por ID de provincia
        public static List<Canton> ObtenerCantonesPorProvincia(int provinciaID)
        {
            List<Canton> cantones = new List<Canton>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cantones WHERE ProvinciaID = @ProvinciaID", connection);
                cmd.Parameters.AddWithValue("@ProvinciaID", provinciaID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Canton canton = new Canton
                    {
                        CantonID = Convert.ToInt32(reader["CantonID"]),
                        NombreCanton = reader["NombreCanton"].ToString(),
                        ProvinciaID = Convert.ToInt32(reader["ProvinciaID"])
                    };
                    cantones.Add(canton);
                }
            }
            return cantones;
        }
    }
}