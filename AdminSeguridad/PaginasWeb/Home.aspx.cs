using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AdminSeguridad.PaginasWeb
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarMenuDinamico();
            }

        }
        private void GenerarMenuDinamico()
        {
            // Verifica si hay un menú en la sesión
            if (Session["UserMenu"] != null)
            {
                DataTable menuData = (DataTable)Session["UserMenu"];
                string menuHtml = "<ul>";

                foreach (DataRow row in menuData.Rows)
                {
                    string nombreMenu = row["NombreMenu"].ToString();
                    string url = row["URL"].ToString();
                    bool permisoLectura = Convert.ToBoolean(row["PermisoLectura"]);

                    // Depuración para verificar el contenido del menú
                    System.Diagnostics.Debug.WriteLine($"Menú: {nombreMenu}, URL: {url}, Permiso: {permisoLectura}");

                    // Solo agrega el enlace si el usuario tiene permiso de lectura
                    if (permisoLectura)
                    {
                        menuHtml += $"<li><a href='{ResolveUrl(url)}'>{nombreMenu}</a></li>";
                    }
                }
                menuHtml += "</ul>";

                // Asigna el HTML generado al control Literal
                menuList.Text = menuHtml;
            }
            else
            {
                // Si no hay menú en la sesión, redirige al login
                Response.Redirect("Login.aspx");
            }
        }
    }
}
