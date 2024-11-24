using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;
using System.Linq;
using System.Web;

namespace AdminSeguridad.AccesoDatos
{
    public class PermisoDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todos los permisos
        public List<Permiso> ObtenerPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Permiso permiso = new Permiso
                    {
                        PermisoID = Convert.ToInt32(reader["PermisoID"]),
                        NombrePermiso = reader["NombrePermiso"].ToString()
                    };
                    permisos.Add(permiso);
                }
            }
            return permisos;
        }

        // Método para obtener un permiso por su ID
        public Permiso ObtenerPermisoPorID(int permisoID)
        {
            Permiso permiso = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos WHERE PermisoID = @PermisoID", connection);
                cmd.Parameters.AddWithValue("@PermisoID", permisoID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    permiso = new Permiso
                    {
                        PermisoID = Convert.ToInt32(reader["PermisoID"]),
                        NombrePermiso = reader["NombrePermiso"].ToString()
                    };
                }
            }
            return permiso;
        }

        // Método para agregar un nuevo permiso
        public bool AgregarPermiso(Permiso permiso)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Permisos (NombrePermiso) VALUES (@NombrePermiso)", connection);
                cmd.Parameters.AddWithValue("@NombrePermiso", permiso.NombrePermiso);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para actualizar un permiso existente
        public bool ActualizarPermiso(Permiso permiso)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Permisos SET NombrePermiso = @NombrePermiso WHERE PermisoID = @PermisoID", connection);
                cmd.Parameters.AddWithValue("@NombrePermiso", permiso.NombrePermiso);
                cmd.Parameters.AddWithValue("@PermisoID", permiso.PermisoID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para eliminar un permiso
        public bool EliminarPermiso(int permisoID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Permisos WHERE PermisoID = @PermisoID", connection);
                cmd.Parameters.AddWithValue("@PermisoID", permisoID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
