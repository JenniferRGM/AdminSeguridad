using AdminSeguridad.AccesoDatos;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSeguridad.PaginasWeb
{
    public partial class Reportes : System.Web.UI.Page
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
                CargarReportes();
            }
        }

        private void VerificarPermisos()
        {
            string rolUsuario = Session["Rol"]?.ToString();
            if (Session["Permisos"] == null)
            {
                Response.Redirect("SinPermiso.aspx");
                return;
            }

            // Recupera los permisos del rol actual desde la sesión
            var permisos = (Dictionary<string, string>)Session["Permisos"];

            if (!string.IsNullOrEmpty(rolUsuario))
            {
                // Verifica si el usuario tiene permisos para la página actual
                if (permisos.ContainsKey("Reportes"))
                {
                    string permiso = permisos["Reportes"];
                    lblMensajePermiso.Text = $"Permiso: {permiso}";

                    if (permiso == "Lectura")
                    {
                        HabilitarLectura();
                    }
                    else if (permiso == "Modificacion" || permiso == "Completo")
                    {
                        HabilitarModificacion();
                    }
                    else
                    {
                        Response.Redirect("SinPermiso.aspx");
                    }
                }
                else
                {
                    Response.Redirect("SinPermiso.aspx"); // Si no tiene permisos para la página
                }
            }
            else
            {
                Response.Redirect("SinPermiso.aspx"); // Si no hay un rol asignado
            }
        }

        private void HabilitarLectura()
        {
            btnLeer.Visible = true;
            gvReportes.Enabled = true;
        }

        private void HabilitarModificacion()
        {
            btnLeer.Visible = true;
            btnModificar.Visible = true;
            gvReportes.Enabled = true;
        }


        private void CargarReportes()
        {
            gvReportes.DataSource = ReporteDataAccess.ObtenerTodosLosReportes();
            gvReportes.DataBind();
        }

        protected void btnLeer_Click(object sender, EventArgs e)
        {
            if (gvReportes.SelectedIndex >= 0)
            {
                // Obtiene el ID del reporte seleccionado en el GridView
                int idReporte = Convert.ToInt32(gvReportes.SelectedDataKey.Value);

                // Asume que tienes un método en ReporteDataAccess para obtener un reporte por ID
                var reporte = ReporteDataAccess.ObtenerReportePorID(idReporte);

                if (reporte != null)
                {
                    lblID.Text = reporte.ID.ToString();
                    lblDescripcion.Text = reporte.Descripcion;
                    lblFecha.Text = reporte.Fecha.ToString("dd/MM/yyyy");
                }
                else
                {
                    lblMensaje.Text = "No se pudo cargar el reporte seleccionado.";
                }
            }
            else
            {
                lblMensaje.Text = "Selecciona un reporte para ver los detalles.";
            }
        }

        protected void BtnRegresarHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PaginasWeb/Home.aspx");
        }

    }
}