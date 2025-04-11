using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataReporteRecibos
    {
         public string id { get; set; }
         public string contrato { get; set; }
         public string folio { get; set; }
         public string cliente { get; set; }
         public DateTime fecha { get; set; }
         public double monto { get; set; }
        public string nota { get; set; }
        public string tipoPago { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public string sucursal { get; set; }
        public string estatus { get; set; }
        public string usuario { get; set; }
        public string concepto { get; set; }

        #region gasto de recibos

        public string beneficiario { get; set; }
        public string cuenta { get; set; }
        public string categoria { get; set; }

        #endregion


    }
}
