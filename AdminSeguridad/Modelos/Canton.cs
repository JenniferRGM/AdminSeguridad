using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Canton
    {
        public int CantonID { get; set; }
        public string NombreCanton { get; set; }
        public int ProvinciaID { get; set; }
    }
}