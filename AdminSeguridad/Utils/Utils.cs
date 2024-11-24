using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Utils
{
    public static class Utils
    {
        public static string GenerarClave()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public static void RegistrarActividad(int usuarioID, string descripcion, string modulo, string tipoEvento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Datos (UsuarioID, Descripcion, Modulo, TipoEvento)
                         VALUES (@UsuarioID, @Descripcion, @Modulo, @TipoEvento)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@Modulo", modulo);
                command.Parameters.AddWithValue("@TipoEvento", tipoEvento);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}