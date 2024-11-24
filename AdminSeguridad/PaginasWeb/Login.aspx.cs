using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdminSeguridad.PaginasWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Autenticación del usuario y obtención del rol
            string rolUsuario = ObtenerRolUsuario(username, password);

            if (!string.IsNullOrEmpty(rolUsuario))
            {
                // Guarda el rol en la sesión
                Session["Rol"] = rolUsuario;

                // Configura permisos en la sesión
                ConfigurarPermisosPorRol(rolUsuario);

                // Configura el menú dinámico según el rol
                ConfigurarMenuUsuario(rolUsuario);

                // Redirige al Home
                Response.Redirect("~/PaginasWeb/Home.aspx");
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos";
            }
        }

        private void ConfigurarPermisosPorRol(string rolUsuario)
        {
            // Define permisos por rol
            var permisosPorRol = new Dictionary<string, Dictionary<string, string>>()
            {
                { "Administrador", new Dictionary<string, string>
                    {
                        { "Dashboard", "Completo" },
                        { "GestionUsuarios", "Escritura" },
                        { "Reportes", "Lectura" },
                        { "Configuracion", "Modificacion" },
                        { "Historial", "Lectura" }
                    }
                },
                { "Usuario", new Dictionary<string, string>
                    {
                        { "Reportes", "Lectura" },
                        { "Configuracion", "Modificacion" },
                        { "Historial", "Lectura" }
                    }
                },
                { "Supervisor", new Dictionary<string, string>
                    {
                        { "Dashboard", "Completo" },
                        { "GestionUsuarios", "LecturaEscritura" }
                    }
                },
                { "Auditor", new Dictionary<string, string>
                    {
                        { "Configuracion", "Modificacion" }
                    }
                }
            };

            // Asigna los permisos del rol actual a la sesión
            if (permisosPorRol.ContainsKey(rolUsuario))
            {
                Session["Permisos"] = permisosPorRol[rolUsuario];
            }
        }

        private void ConfigurarMenuUsuario(string rolUsuario)
        {
            // DataTable para el menú
            DataTable menuData = new DataTable();
            menuData.Columns.Add("NombreMenu", typeof(string));
            menuData.Columns.Add("URL", typeof(string));
            menuData.Columns.Add("PermisoLectura", typeof(bool));

            // Define el menú según el rol
            if (rolUsuario == "Administrador")
            {
                menuData.Rows.Add("Dashboard", "~/PaginasWeb/Dashboard.aspx", true);
                menuData.Rows.Add("Gestión de Usuarios", "~/PaginasWeb/GestionUsuarios.aspx", true);
                menuData.Rows.Add("Reportes", "~/PaginasWeb/Reportes.aspx", true);
                menuData.Rows.Add("Configuración", "~/PaginasWeb/Configuracion.aspx", true);
                menuData.Rows.Add("Historial", "~/PaginasWeb/Historial.aspx", true);
            }
            else if (rolUsuario == "Usuario")
            {
                menuData.Rows.Add("Reportes", "~/PaginasWeb/Reportes.aspx", true);
                menuData.Rows.Add("Configuración", "~/PaginasWeb/Configuracion.aspx", false);
                menuData.Rows.Add("Historial", "~/PaginasWeb/Historial.aspx", true);
            }
            else if (rolUsuario == "Supervisor")
            {
                menuData.Rows.Add("Dashboard", "~/PaginasWeb/Dashboard.aspx", true);
                menuData.Rows.Add("Gestión de Usuarios", "~/PaginasWeb/GestionUsuarios.aspx", true);
            }
            else if (rolUsuario == "Auditor")
            {
                menuData.Rows.Add("Configuración", "~/PaginasWeb/Configuracion.aspx", true);
            }

            // Asigna el menú a la sesión
            Session["UserMenu"] = menuData;
        }

        private string ObtenerRolUsuario(string username, string password)
        {
            string rolUsuario = string.Empty;

            // Cadena de conexión desde Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["AdminSeguridadDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT Roles.NombreRol 
                                 FROM Login 
                                 INNER JOIN Roles ON Login.RolID = Roles.RolID 
                                 WHERE Usuario = @usuario AND Clave = @clave";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@usuario", username);
                cmd.Parameters.AddWithValue("@clave", password);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Obtiene el nombre del rol
                    rolUsuario = reader["NombreRol"].ToString();
                }
            }

            return rolUsuario;
        }
    }
}
