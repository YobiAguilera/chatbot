using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataHistorialEstatusVehiculos
    {
        public string placas { get; set; }
        public string modelo { get; set; }
        public string serie { get; set; }
        public string estatus { get; set; }
        public DateTime? fechaFueraServicio { get; set; } 
        public DateTime? fechaActivo { get; set; } 
        public string usuario { get; set; }
        public string sucursal { get; set; }
    }

}
