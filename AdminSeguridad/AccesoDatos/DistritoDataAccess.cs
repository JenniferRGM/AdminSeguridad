using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;

namespace AdminSeguridad.AccesoDatos
{
    public class DistritoDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todos los distritos
        public List<Distrito> ObtenerDistritos()
        {
            List<Distrito> distritos = new List<Distrito>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Distritos", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Distrito distrito = new Distrito
                    {
                        DistritoID = Convert.ToInt32(reader["DistritoID"]),
                        NombreDistrito = reader["NombreDistrito"].ToString(),
                        CantonID = Convert.ToInt32(reader["CantonID"])
                    };
                    distritos.Add(distrito);
                }
            }
            return distritos;
        }

        // Método para obtener los distritos por ID de cantón
        public List<Distrito> ObtenerDistritosPorCanton(int cantonID)
        {
            List<Distrito> distritos = new List<Distrito>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Distritos WHERE CantonID = @CantonID", connection);
                cmd.Parameters.AddWithValue("@CantonID", cantonID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Distrito distrito = new Distrito
                    {
                        DistritoID = Convert.ToInt32(reader["DistritoID"]),
                        NombreDistrito = reader["NombreDistrito"].ToString(),
                        CantonID = Convert.ToInt32(reader["CantonID"])
                    };
                    distritos.Add(distrito);
                }
            }
            return distritos;
        }
    }
}