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
    public partial class Dashboard : System.Web.UI.Page
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
                CargarDatos();
            }
        }

        private void VerificarPermisos()
        {
            // Obtiene el rol del usuario desde la sesión
            string rolUsuario = Session["Rol"]?.ToString();
            var permisos = Session["Permisos"] as Dictionary<string, string>;

            if (string.IsNullOrEmpty(rolUsuario) || permisos == null)
            {
                Response.Redirect("~/PaginasWeb/SinPermiso.aspx"); 
                return;
            }

            if (permisos.ContainsKey("Dashboard") && permisos["Dashboard"] == "Completo")
            {
                HabilitarAccesoCompleto(); 
            }
            else
            {
                Response.Redirect("~/PaginasWeb/SinPermiso.aspx"); 
            }
        }

        private void HabilitarAccesoCompleto()
        {
            btnLeer.Visible = true;
            btnEscribir.Visible = true;
            btnModificar.Visible = true;
            btnEliminar.Visible = true;
        }

        // Obtiene los datos desde la capa de acceso a datos
        private void CargarDatos()
        {
            gvDashboard.DataSource = DashboardDataAccess.ObtenerTodosLosDatos();
            gvDashboard.DataBind();
        }

        protected void BtnLeer_Click(object sender, EventArgs e)
        {
            if (gvDashboard.SelectedIndex >= 0)
            {
                int id = Convert.ToInt32(gvDashboard.SelectedDataKey.Value);
                var data = DashboardDataAccess.ObtenerDatoPorID(id);

                if (data != null)
                {
                    lblID.Text = data.ID.ToString();
                    lblDescripcion.Text = data.Descripcion;
                }
            }
            else
            {
                lblMensaje.Text = "Selecciona un elemento para ver sus detalles.";
            }
        }
    
        protected void BtnEscribir_Click(object sender, EventArgs e)
        {
            var nuevoDato = new Dato
            {
                Descripcion = txtDescripcion.Text,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            bool resultado = DashboardDataAccess.AgregarDato(nuevoDato);

            if (resultado)
            {
                lblMensaje.Text = "Elemento agregado exitosamente.";
                CargarDatos(); 
            }
            else
            {
                lblMensaje.Text = "Error al agregar el elemento.";
            }
        }
    

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
        if (gvDashboard.SelectedIndex >= 0)
        {
            int id = Convert.ToInt32(gvDashboard.SelectedDataKey.Value);

            
            var datoModificado = new Dato
            {
                ID = id,
                Descripcion = txtDescripcion.Text,
                FechaActualizacion = DateTime.Now
            };

            bool resultado = DashboardDataAccess.ModificarDato(datoModificado);

            if (resultado)
            {
                lblMensaje.Text = "Elemento modificado exitosamente.";
                CargarDatos(); 
            }
            else
            {
                lblMensaje.Text = "Error al modificar el elemento.";
            }
        }
        else
        {
            lblMensaje.Text = "Selecciona un elemento para modificar.";
        }
    
}

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (gvDashboard.SelectedIndex >= 0)
            {
                int id = Convert.ToInt32(gvDashboard.SelectedDataKey.Value);

                bool resultado = DashboardDataAccess.EliminarDato(id);

                if (resultado)
                {
                    lblMensaje.Text = "Elemento eliminado exitosamente.";
                    CargarDatos(); 
                }
                else
                {
                    lblMensaje.Text = "Error al eliminar el elemento.";
                }
            }
            else
            {
                lblMensaje.Text = "Selecciona un elemento para eliminar.";
            }
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            // Redirige al menú principal
            Response.Redirect("~/PaginasWeb/Home.aspx");
        }
    }
    
}