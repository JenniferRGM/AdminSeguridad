using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Configuracion
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioNombre { get; set; }
    }
}