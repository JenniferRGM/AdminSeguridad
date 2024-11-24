using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Provincia
    {
        public int ProvinciaID { get; set; }
        public string NombreProvincia { get; set; }
        public List<Canton> Cantones { get; set; }
    }
}