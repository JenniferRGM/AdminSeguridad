using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Modelos
{
    public class Historial
    {
        public int ID { get; set; }
        public string UsuarioID { get; set; }
        public string Descripcion { get; set; }
        public string Modulo { get; set; }
        public string TipoEvento { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

}