using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataVentasPiso
    {
        public string nota { get; set; }
        public DateTime fecha { get; set; }
        public string contrato { get; set; }
        public string tipoServicio { get; set; }
        public decimal monto { get; set; }
        public string recibos { get; set; }
        public decimal pago { get; set; }
        public decimal adeudo { get; set; }
        public string sucursal { get; set; }
        public string jefeTurno { get; set; }
    }

    public class dataServiciosExtraVentasPiso
    {
        public string nota { get; set; }
        public string contrato { get; set; }
        public DateTime fecha { get; set; }
        public string tipoServicio { get; set; }
        public string concepto { get; set; }
        public decimal cantidad { get; set; }
        public string recibos { get; set; }
        public decimal pago { get; set; }
        public decimal adeudo { get; set; }
        public string sucursal { get; set; }
        public string jefeTurno { get; set; }
    }
    public class dataReporteMensualCombinado
    {
        public string mes { get; set; }
        public decimal montoUsos { get; set; }
        public decimal montoMejoras { get; set; }
    }
    public class dataReporteJefes
    {
        public string jefeTurno { get; set; }
        public decimal montoUsos { get; set; }
        public decimal montoMejoras { get; set; }
        public decimal montoServiciosExtra { get; set; }
        public decimal total { get; set; }
        public decimal comision { get; set; }
    }
    
}
