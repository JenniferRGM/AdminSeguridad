using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSeguridad.AccesoDatos;
using AdminSeguridad.Modelos;
using AdminSeguridad.Utils;

namespace AdminSeguridad.PaginasWeb
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private string connectionString = "Data Source=JENNY\\SQLEXPRESS;Initial Catalog=AdminSeguridad;Integrated Security=True";

        private UsuarioDataAccess usuarioDataAccess = new UsuarioDataAccess();
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
                CargarUsuarios();
                CargarRoles();
                CargarProvincias();
            }

        }

        private void CargarRoles()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT RolID, NombreRol FROM Roles", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable rolesTable = new DataTable();

                adapter.Fill(rolesTable);

                ddlRol.DataSource = rolesTable;
                ddlRol.DataTextField = "NombreRol";
                ddlRol.DataValueField = "RolID";
                ddlRol.DataBind();

                ddlRol.Items.Insert(0, new ListItem("-- Seleccione un Rol --", "0"));
            }
        }


        private void CargarProvincias()
        {
            ProvinciaDataAccess provinciaDataAccess = new ProvinciaDataAccess();
            var provincias = provinciaDataAccess.ObtenerProvinciasConCantones();

            ddlProvincia.DataSource = provincias;
            ddlProvincia.DataTextField = "NombreProvincia";
            ddlProvincia.DataValueField = "ProvinciaID";
            ddlProvincia.DataBind();

            ddlProvincia.Items.Insert(0, new ListItem("--Seleccione Provincia--", "0"));
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int provinciaID = int.Parse(ddlProvincia.SelectedValue);
            CantonDataAccess cantonDataAccess = new CantonDataAccess();
            var cantones = CantonDataAccess.ObtenerCantonesPorProvincia(provinciaID);

            txtCanton.DataSource = cantones;
            txtCanton.DataTextField = "Nombre";
            txtCanton.DataValueField = "Canton";
            txtCanton.DataBind();

            txtCanton.Items.Insert(0, new ListItem("--Seleccione Cantón--", "0"));
        }

        protected void txtCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            int provinciaID = Convert.ToInt32(ddlProvincia.SelectedValue);
            CantonDataAccess cantonDataAccess = new CantonDataAccess();
            var cantones = CantonDataAccess.ObtenerCantonesPorProvincia(provinciaID);

            txtCanton.DataSource = cantones;
            txtCanton.DataTextField = "NombreCanton";
            txtCanton.DataValueField = "Canton";
            txtCanton.DataBind();
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

            // Verifica si el usuario tiene permisos para Gestión de Usuarios
            if (permisos.ContainsKey("GestionUsuarios"))
            {
                string permiso = permisos["GestionUsuarios"];

                if (permiso == "Escritura")
                {
                    HabilitarEscritura(); // Permite solo escritura
                }
                else if (permiso == "LecturaEscritura")
                {
                    HabilitarLecturaEscritura(); // Permite lectura y escritura
                }
                else
                {
                    Response.Redirect("~/PaginasWeb/SinPermiso.aspx");
                }
            }
            else
            {
                Response.Redirect("~/PaginasWeb/SinPermiso.aspx");
            }
        }

        private void HabilitarEscritura()
        {
            btnEscribir.Visible = true;
            btnLeer.Visible = false;
        }
        private void HabilitarLecturaEscritura()
        {
            btnLeer.Visible = true;
            btnEscribir.Visible = true;
        }

        private void CargarUsuarios()
        {
            var usuarios = usuarioDataAccess.ObtenerUsuarios();

            if (usuarios.GroupBy(u => u.UsuarioID).Any(g => g.Count() > 1))
            {
                lblMensaje.Text = "Error: Se detectaron usuarios duplicados.";
                return;
            }

            gvUsuarios.DataSource = usuarioDataAccess.ObtenerUsuarios();
            gvUsuarios.DataBind();
        }

        protected void BtnLeer_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el GridView
            if (gvUsuarios.SelectedIndex >= 0)
            {
                // Obtiene el ID del usuario seleccionado
                int usuarioID = Convert.ToInt32(gvUsuarios.SelectedDataKey.Value);

                // Llama a un método para obtener los detalles del usuario
                var usuario = usuarioDataAccess.ObtenerUsuarioPorID(usuarioID);

                if (usuario != null)
                {
                    // Muestra los detalles del usuario en campos de texto o etiquetas
                    
                    lblNombre.Text = usuario.Nombre;
                    lblApellido1.Text = usuario.Apellido1;
                    lblApellido2.Text = usuario.Apellido2;
                    lblEmail.Text = usuario.Email;
                }
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona un usuario en la lista para ver sus detalles.";
            }
        }

        protected void BtnEscribir_Click(object sender, EventArgs e)
        {
            // Crea un nuevo objeto usuario y asigna los valores de los campos de entrada
            Usuario nuevoUsuario = new Usuario
            {
                Nombre = txtNombre.Text,
                Apellido1 = txtApellido1.Text,
                Apellido2 = txtApellido2.Text,
                Email = txtEmail.Text,
                Clave = string.IsNullOrEmpty(txtClave.Text) ? AdminSeguridad.Utils.Utils.GenerarClave() : txtClave.Text,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now,
                RolID = int.Parse(ddlRol.SelectedValue) 
            };

            Telefono nuevoTelefono = new Telefono
            {
                CodigoPais = "+506",
                NumeroTelefono = txtTelefono.Text
            };

            Ubicacion nuevaUbicacion = new Ubicacion
            {
                ProvinciaID = int.Parse(ddlProvincia.SelectedValue),
                Canton = txtCanton.Text.Trim(),
                Distrito = txtDistrito.Text.Trim(),
                OtrasSenas = txtOtrasSenas.Text
            };

            UsuarioDataAccess dataAccess = new UsuarioDataAccess();
            if (dataAccess.AgregarUsuarioConDetalles(nuevoUsuario, nuevoTelefono, nuevaUbicacion))
            {
                lblMensaje.Text = "Usuario creado exitosamente.";
                lblMensaje.CssClass = "mensaje-exito";
            }
            else
            {
                lblMensaje.Text = "Error al crear el usuario.";
                lblMensaje.CssClass = "mensaje-error";
            }
        }

        protected void GuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Crea un nuevo objeto Usuario con los datos del formulario
                Usuario nuevoUsuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido1 = txtApellido1.Text.Trim(),
                    Apellido2 = txtApellido2.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Clave = string.IsNullOrEmpty(txtClave.Text) ? AdminSeguridad.Utils.Utils.GenerarClave() : txtClave.Text,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                    RolID = int.Parse(ddlRol.SelectedValue)
                };

                // Crea un nuevo objeto Telefono con los datos del formulario
                Telefono nuevoTelefono = new Telefono
                {
                    CodigoPais = "+506",
                    NumeroTelefono = txtTelefono.Text.Trim()
                };

                // Crea un nuevo objeto Ubicacion con los datos del formulario
                Ubicacion nuevaUbicacion = new Ubicacion
                {
                    ProvinciaID = int.Parse(ddlProvincia.SelectedValue),
                    Canton = txtCanton.Text.Trim(),
                    Distrito = txtDistrito.Text.Trim(),
                    OtrasSenas = txtOtrasSenas.Text.Trim()
                };

                // Llama al método para agregar el usuario con detalles
                UsuarioDataAccess usuarioDataAccess = new UsuarioDataAccess();
                bool resultado = usuarioDataAccess.AgregarUsuarioConDetalles(nuevoUsuario, nuevoTelefono, nuevaUbicacion);

                if (resultado)
                {
                    lblMensaje.Text = "Usuario creado o actualizado exitosamente.";
                    lblMensaje.CssClass = "mensaje-exito";
                }
                else
                {
                    lblMensaje.Text = "Error al guardar el usuario.";
                    lblMensaje.CssClass = "mensaje-error";
                }

                // Recarga la lista de usuarios
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "mensaje-error";
            }
        }

        protected void BtnRegresarHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PaginasWeb/Home.aspx");
        }


    }
}