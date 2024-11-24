using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Menu
    {
        public int MenuID { get; set; }
        public string NombreMenu { get; set; }
        public string URL { get; set; }
        public Rol Rol { get; set; }
    }
}