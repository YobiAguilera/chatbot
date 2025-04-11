using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class ccDetalleMO
    {
        public int id { get; set; }
        public string concepto { get; set; }
        public double costo { get; set; }
        public DateTime? fechaAltaSql { get; set; }
        public string folioSol { get; set; }
    }
    public class ccDetalleCA
    {
        public int id { get; set; }
        public string concepto { get; set; }
        public double costo { get; set; }
        public DateTime? fechaAltaSql { get; set; }
        public string folioSol { get; set; }
    }
    public class ccRevisiones
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public string folioSol { get; set; }
        public string foto1 { get; set; }
        public string foto2 { get; set; }
        public string foto3 { get; set; }
        public string hora { get; set; }
        public string observaciones { get; set; }
        public string usuario { get; set; }
    }
}
