using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataReciboAgente
    {
        public DateTime fecha { get; set; }
        public string folio { get; set; }
        public double monto { get; set; }
        public string solicitud { get; set; }
        public string cliente { get; set; }
        public string asesor { get; set; }
        public string usuario { get; set; }
        public string sucursal { get; set; }
        public string descripcion { get; set; }
        public string estatus { get; set; }
    }
}
