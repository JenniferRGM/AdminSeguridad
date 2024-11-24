using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using AdminSeguridad.Modelos;
using System.Linq;
using System.Web;

namespace AdminSeguridad.AccesoDatos
{
    public class MenuDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

        // Método para obtener todos los menús
        public List<Menu> ObtenerMenus()
        {
            List<Menu> menus = new List<Menu>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN QUE OBTIENE LOS MENUS CON EL ROL ASOCIADO
                connection.Open();
                string query = @"
                    SELECT m.MenuID, m.NombreMenu, m.URL, r.NombreRol
                    FROM Menus m
                    INNER JOIN Roles r ON m.RolID = r.RolID";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Menu menu = new Menu
                    {
                        MenuID = Convert.ToInt32(reader["MenuID"]),
                        NombreMenu = reader["NombreMenu"].ToString(),
                        URL = reader["URL"].ToString(),
                        Rol = new Rol
                        {
                            NombreRol = reader["NombreRol"].ToString()
                        }
                    };
                    menus.Add(menu);
                }
            }
            return menus;
        }

        // Método para obtener un menú por su ID
        public Menu ObtenerMenuPorID(int menuID)
        {
            Menu menu = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //INNER JOIN PARA OBTENER EL MENU EN ESPECIFICO CON SU ROL
                connection.Open();
                string query = @"
                    SELECT m.MenuID, m.NombreMenu, m.URL, r.NombreRol
                    FROM Menus m
                    INNER JOIN Roles r ON m.RolID = r.RolID
                    WHERE m.MenuID = @MenuID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MenuID", menuID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    menu = new Menu
                    {
                        MenuID = Convert.ToInt32(reader["MenuID"]),
                        NombreMenu = reader["NombreMenu"].ToString(),
                        URL = reader["URL"].ToString(),
                        Rol = new Rol
                        {
                            NombreRol = reader["NombreRol"].ToString()
                        }
                    };
                }
            }
            return menu;
        }

        // Método para agregar un nuevo menú
        public bool AgregarMenu(Menu menu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Menus (NombreMenu, URL) VALUES (@NombreMenu, @URL)", connection);
                cmd.Parameters.AddWithValue("@NombreMenu", menu.NombreMenu);
                cmd.Parameters.AddWithValue("@URL", menu.URL);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para actualizar un menú existente
        public bool ActualizarMenu(Menu menu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Menus SET NombreMenu = @NombreMenu, URL = @URL WHERE MenuID = @MenuID", connection);
                cmd.Parameters.AddWithValue("@NombreMenu", menu.NombreMenu);
                cmd.Parameters.AddWithValue("@URL", menu.URL);
                cmd.Parameters.AddWithValue("@MenuID", menu.MenuID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método para eliminar un menú
        public bool EliminarMenu(int menuID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Menus WHERE MenuID = @MenuID", connection);
                cmd.Parameters.AddWithValue("@MenuID", menuID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}