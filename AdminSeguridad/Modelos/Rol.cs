using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Rol
    {
        public int RolID { get; set; }
        public string NombreRol { get; set; }
        public Permiso Permiso { get; set; }
    }
}