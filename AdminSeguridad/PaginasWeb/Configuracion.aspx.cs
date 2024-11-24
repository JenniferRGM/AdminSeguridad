using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSeguridad.AccesoDatos;
using AdminSeguridad.Modelos;

namespace AdminSeguridad.PaginasWeb
{
    public partial class Configuracion : System.Web.UI.Page
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
                CargarConfiguraciones();
            }

        }

        private void VerificarPermisos()
        {
            string rolUsuario = Session["Rol"]?.ToString();
            var permisos = (Dictionary<string, string>)Session["Permisos"];

            if (permisos.ContainsKey("Configuracion"))
            {
                string permiso = permisos["Configuracion"];
                if (permiso == "Modificacion")
                {
                    btnLeer.Visible = true;
                    btnModificar.Visible = true;
                }
                else
                {
                    Response.Redirect("SinPermiso.aspx");
                }
            }
            else
            {
                Response.Redirect("SinPermiso.aspx");
            }
        }

        // Método para cargar las configuraciones en el GridView
        private void CargarConfiguraciones()
        {
            gvConfiguraciones.DataSource = ConfiguracionDataAccess.ObtenerTodasLasConfiguraciones();
            gvConfiguraciones.DataBind();
        }

        // Acción para leer la configuración seleccionada en el GridView
        protected void BtnLeer_Click(object sender, EventArgs e)
        {
            if (gvConfiguraciones.SelectedIndex >= 0)
            {
                int id = Convert.ToInt32(gvConfiguraciones.SelectedDataKey.Value);
                var configuracion = ConfiguracionDataAccess.ObtenerConfiguracionPorID(id);

                if (configuracion != null)
                {
                    lblID.Text = configuracion.ID.ToString();
                    txtDescripcion.Text = configuracion.Descripcion;
                    txtValor.Text = configuracion.Valor; 
                }
                else
                {
                    lblMensaje.Text = "No se encontró la configuración seleccionada.";
                }
            }
            else
            {
                lblMensaje.Text = "Selecciona una configuración para ver sus detalles.";
            }
        }

        // Acción para modificar la configuración
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            AdminSeguridad.Modelos.Configuracion configuracion = new AdminSeguridad.Modelos.Configuracion
            {
                ID = Convert.ToInt32(lblID.Text),
                Descripcion = txtDescripcion.Text,
                Valor = txtValor.Text,
                FechaActualizacion = DateTime.Now
            };

            bool resultado = ConfiguracionDataAccess.ModificarConfiguracion(configuracion);
            lblMensaje.Text = resultado ? "Configuración modificada exitosamente." : "Error al modificar la configuración.";
        }
    }
    

    
}