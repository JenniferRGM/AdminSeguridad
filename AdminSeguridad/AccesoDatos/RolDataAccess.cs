using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdminSeguridad.Modelos;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AdminSeguridad.AccesoDatos
{
    public class RolDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todos los roles
        public List<Rol> ObtenerRoles()
        {
            List<Rol> roles = new List<Rol>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN PARA RECUPERAR LA INFORMACION SOBRE LOS PERMISOS QUE SE ASOCIAN AL ROL
                connection.Open();
                string query = @"
                    SELECT r.RolID, r.NombreRol, p.PermisoID, p.NombrePermiso
                    FROM Roles r
                    INNER JOIN Permisos p ON r.RolID = p.RolID";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Rol rol = new Rol
                    {
                        RolID = Convert.ToInt32(reader["RolID"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        Permiso = new Permiso
                        {
                            PermisoID = Convert.ToInt32(reader["PermisoID"]),
                            NombrePermiso = reader["NombrePermiso"].ToString()
                        }
                    };
                    roles.Add(rol);
                }
            }
            return roles;
        }

        // Método para obtener un rol por su ID
        public Rol ObtenerRolPorID(int rolID)
        {
            Rol rol = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT r.RolID, r.NombreRol, p.PermisoID, p.NombrePermiso
                    FROM Roles r
                    INNER JOIN Permisos p ON r.RolID = p.RolID
                    WHERE r.RolID = @RolID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RolID", rolID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    rol = new Rol
                    {
                        RolID = Convert.ToInt32(reader["RolID"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        Permiso = new Permiso
                        {
                            PermisoID = Convert.ToInt32(reader["PermisoID"]),
                            NombrePermiso = reader["NombrePermiso"].ToString()
                        }
                    };
                }
            }
            return rol;
        }

        // Método para agregar un nuevo rol
        public bool AgregarRol(Rol rol)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Roles (NombreRol) VALUES (@NombreRol)", connection);
                cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para actualizar un rol existente
        public bool ActualizarRol(Rol rol)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Roles SET NombreRol = @NombreRol WHERE RolID = @RolID", connection);
                cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                cmd.Parameters.AddWithValue("@RolID", rol.RolID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para eliminar un rol
        public bool EliminarRol(int rolID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Roles WHERE RolID = @RolID", connection);
                cmd.Parameters.AddWithValue("@RolID", rolID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


    }
}