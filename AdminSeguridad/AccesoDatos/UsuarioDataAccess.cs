using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;
using System.Linq;

namespace AdminSeguridad.AccesoDatos
{
    public class UsuarioDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todos los usuarios
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta con DISTINCT para evitar duplicados
                string query = @"
                    SELECT DISTINCT 
                        u.UsuarioID, u.Nombre, u.Apellido1, u.Apellido2, u.Email, u.Clave,
                        u.FechaCreacion, u.FechaActualizacion, u.RolID, r.NombreRol AS RolNombre
                    FROM Usuarios u
                    INNER JOIN Roles r ON u.RolID = r.RolID";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido1 = reader["Apellido1"].ToString(),
                        Apellido2 = reader["Apellido2"].ToString(),
                        Email = reader["Email"].ToString(),
                        Clave = reader["Clave"].ToString(),
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"]),
                        RolID = Convert.ToInt32(reader["RolID"]),
                        RolNombre = reader["RolNombre"].ToString()
                    };
                    usuarios.Add(usuario);
                }
            }

            // Eliminar duplicados si los hubiera por lógica interna
            usuarios = usuarios.GroupBy(u => u.UsuarioID).Select(g => g.First()).ToList();
            return usuarios;
        }

        // Método para obtener un usuario por su ID
        public Usuario ObtenerUsuarioPorID(int usuarioID)
        {
            Usuario usuario = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT u.UsuarioID, u.Nombre, u.Apellido1, u.Apellido2, u.Email, u.Clave, 
                           u.FechaCreacion, u.FechaActualizacion, u.RolID, r.NombreRol AS RolNombre
                    FROM Usuarios u
                    INNER JOIN Roles r ON u.RolID = r.RolID
                    WHERE u.UsuarioID = @UsuarioID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido1 = reader["Apellido1"].ToString(),
                        Apellido2 = reader["Apellido2"].ToString(),
                        Email = reader["Email"].ToString(),
                        Clave = reader["Clave"].ToString(),
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"]),
                        RolID = Convert.ToInt32(reader["RolID"]),
                        RolNombre = reader["RolNombre"].ToString()
                    };
                }
            }
            return usuario;
        }

        // Método para agregar un nuevo usuario con detalles
        public bool AgregarUsuarioConDetalles(Usuario usuario, Telefono telefono, Ubicacion ubicacion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand cmdUsuario = new SqlCommand(
                        @"INSERT INTO Usuarios (Nombre, Apellido1, Apellido2, Email, Clave, FechaCreacion, FechaActualizacion, RolID) 
                          VALUES (@Nombre, @Apellido1, @Apellido2, @Email, @Clave, @FechaCreacion, @FechaActualizacion, @RolID); 
                          SELECT SCOPE_IDENTITY();",
                        connection, transaction);

                    cmdUsuario.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmdUsuario.Parameters.AddWithValue("@Apellido1", usuario.Apellido1);
                    cmdUsuario.Parameters.AddWithValue("@Apellido2", usuario.Apellido2);
                    cmdUsuario.Parameters.AddWithValue("@Email", usuario.Email);
                    cmdUsuario.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmdUsuario.Parameters.AddWithValue("@FechaCreacion", usuario.FechaCreacion);
                    cmdUsuario.Parameters.AddWithValue("@FechaActualizacion", usuario.FechaActualizacion);
                    cmdUsuario.Parameters.AddWithValue("@RolID", usuario.RolID);

                    int usuarioID = Convert.ToInt32(cmdUsuario.ExecuteScalar());

                    // Valida y agrega teléfono si no existe
                    string verificarTelefono = "SELECT COUNT(*) FROM Telefonos WHERE Numero = @Numero AND UsuarioID = @UsuarioID";
                    SqlCommand cmdVerificar = new SqlCommand(verificarTelefono, connection, transaction);
                    cmdVerificar.Parameters.AddWithValue("@Numero", telefono.NumeroTelefono);
                    cmdVerificar.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    int existeTelefono = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (existeTelefono == 0)
                    {
                        SqlCommand cmdTelefono = new SqlCommand(
                            "INSERT INTO Telefonos (Numero, UsuarioID) VALUES (@Numero, @UsuarioID);",
                            connection, transaction);
                        cmdTelefono.Parameters.AddWithValue("@Numero", telefono.NumeroTelefono);
                        cmdTelefono.Parameters.AddWithValue("@UsuarioID", usuarioID);
                        cmdTelefono.ExecuteNonQuery();
                    }

                    // Agrega ubicación
                    SqlCommand cmdUbicacion = new SqlCommand(
                        "INSERT INTO Ubicaciones (ProvinciaID, Canton, Distrito, OtrasSenas, UsuarioID) " +
                        "VALUES (@ProvinciaID, @Canton, @Distrito, @OtrasSenas, @UsuarioID);",
                        connection, transaction);
                    cmdUbicacion.Parameters.AddWithValue("@ProvinciaID", ubicacion.ProvinciaID);
                    cmdUbicacion.Parameters.AddWithValue("@Canton", ubicacion.Canton);
                    cmdUbicacion.Parameters.AddWithValue("@Distrito", ubicacion.Distrito);
                    cmdUbicacion.Parameters.AddWithValue("@OtrasSenas", ubicacion.OtrasSenas);
                    cmdUbicacion.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    cmdUbicacion.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Método para actualizar un usuario existente
        public bool ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Usuarios 
                      SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2, 
                          Email = @Email, Clave = @Clave, FechaActualizacion = @FechaActualizacion, RolID = @RolID 
                      WHERE UsuarioID = @UsuarioID",
                    connection);

                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido1", usuario.Apellido1);
                cmd.Parameters.AddWithValue("@Apellido2", usuario.Apellido2);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                cmd.Parameters.AddWithValue("@FechaActualizacion", usuario.FechaActualizacion);
                cmd.Parameters.AddWithValue("@RolID", usuario.RolID);
                cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para eliminar un usuario
        public bool EliminarUsuario(int usuarioID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID", connection);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
