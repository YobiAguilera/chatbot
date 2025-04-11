using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes.dataReportes
{
    public class dataReportePagares
    {
        public string sucursal { get; set; }
        public string folio { get; set; }
        public DateTime fecha { get; set; }
        public string folioContrato { get; set; }
        public string solicitante { get; set; }
        public string jefeTurno { get; set; }
        public int numeroPagares { get; set; }
        public decimal adeudo { get; set; }
        public string clave { get; set; }
        public int pagareNumero { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public decimal abonado { get; set; }
        public decimal intereses { get; set; }
        public decimal saldoActual { get; set; }
        public string estatus { get; set; }
        public string tipoRecibo { get; set; }
        public int idconcepto { get; set; }

    }
}
