using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class RolPermiso
    {
        public int RolID { get; set; }
        public int PermisoID { get; set; }
        public int MenuID { get; set; }
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoEliminacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

    }
}