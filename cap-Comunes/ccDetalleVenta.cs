using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class ccDetalleVenta
    {
        public string nombre { get; set; }
        public string unidad { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double costo { get; set; }
        public double iva { get; set; }
        public double subtotal { get; set; }
    }
}
