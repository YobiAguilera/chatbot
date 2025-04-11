using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataReporteAvisos
    {
        public string contrato { get; set; }
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public string frecuencia { get; set; }
        public double monto { get; set; }
        public string fecha { get; set; }
        public string ruta { get; set; }
        public double saldo { get; set; }
        public double vencido { get; set; }
        public double montoAbono { get; set; }
        public double pagoRequerido { get; set; }
        public int numeroPago { get; set; }
        public double costoPlan { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
    }
}
