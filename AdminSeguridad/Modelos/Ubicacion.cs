using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Ubicacion
    {
        public int UbicacionID { get; set; }
        public int UsuarioID { get; set; }
        public int ProvinciaID { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string OtrasSenas { get; set; }

    }
}