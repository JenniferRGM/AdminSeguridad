using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.AccesoDatos
{
    public class DatabaseConnection
    {
        private SqlConnection connection;

        // Constructor que inicializa la conexión
        public DatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        // Método para obtener la conexión
        public SqlConnection GetConnection()
        {
            return connection;
        }

        // Método para abrir la conexión si está cerrada
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Método para cerrar la conexión si está abierta
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}