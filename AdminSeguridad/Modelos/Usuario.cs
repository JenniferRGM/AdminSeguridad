using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int RolID { get; set; }
        public string RolNombre { get; set; }
    }
}