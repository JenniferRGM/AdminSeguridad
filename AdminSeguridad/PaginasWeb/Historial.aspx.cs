using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSeguridad.AccesoDatos;
using AdminSeguridad.Modelos;


namespace AdminSeguridad.PaginasWeb
{
    public partial class Historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] == null || Session["UserMenu"] == null)
            {
                // Si no hay sesión válida, redirige al login
                Response.Redirect("~/PaginasWeb/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                VerificarPermisos();
                CargarHistorial();
                
            }

        }

        private void VerificarPermisos()
        {
            // Obtiene el rol del usuario desde la sesión
            string rolUsuario = Session["Rol"]?.ToString();
            var permisos = Session["Permisos"] as Dictionary<string, string>;

            // Verifica si el rol o los permisos no están definidos
            if (string.IsNullOrEmpty(rolUsuario) || permisos == null)
            {
                Response.Redirect("~/PaginasWeb/SinPermiso.aspx"); // Redirige si no hay permisos
                return;
            }

            // Verifica si el usuario tiene permisos para la página de historial
            if (permisos.ContainsKey("Historial"))
            {
                string permiso = permisos["Historial"];

                if (permiso == "Lectura" || permiso == "LecturaEscritura" || permiso == "Completo")
                {
                    // Permite el acceso basado en el nivel de permiso
                    lblMensajePermiso.Text = $"Acceso permitido con nivel: {permiso}.";
                }
                else
                {
                    Response.Redirect("~/PaginasWeb/SinPermiso.aspx"); // Redirige si no tiene permisos
                }
            }
            else
            {
                Response.Redirect("~/PaginasWeb/SinPermiso.aspx"); // Redirige si no tiene permisos para Historial
            }
        }


        private void CargarHistorial()
        {
            try
            {
                List<AdminSeguridad.Modelos.Historial> historial = HistorialAccess.ObtenerHistorial();
                gvHistorial.DataSource = historial;
                gvHistorial.DataBind();
                lblMensaje.Text = "Historial cargado exitosamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar el historial: " + ex.Message;
            }
        }

        protected void BtnRegresarHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PaginasWeb/Home.aspx");
        }

    }
}