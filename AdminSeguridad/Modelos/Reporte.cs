using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Reporte
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioNombre { get; set; }
    }
}