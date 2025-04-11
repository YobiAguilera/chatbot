using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataNotaServicio
    {
        public string folio { get; set; } 
        public DateTime fecha { get; set; }
        public string fechaContrato { get; set; }
        public string atendio { get; set; }
        public string atendioSucursal { get; set; }
        public string nombreContratante { get; set; }
        //public string telefonoContratante { get; set; }
        //public string domicilioContratante { get; set; }
        public string nombreSolicitante { get; set; }
        public string nombreEstadoContrato { get; set; }
        public string telefonoSolicitante { get; set; }
        public string domicilioSolicitante { get; set; }
        public string domicilioSucursales { get; set; }     
        public string nombreFinado { get; set; } 
        public string domicilioFinado { get; set; }
        public string domicilioFilial { get; set; }
        public string imprimirDomicilioFilial { get; set; }
        public string sucursalFilial { get; set; }
        public string validarFilial { get; set; }
        public string validarCremacion { get; set; }
        public string validarInhumacion { get; set; }
        public string validarOtro { get; set; }
        public string telefonoFinado { get; set; }
        public string sucursal { get; set; } 
        public string tipo { get; set; } 
        public string RFC { get; set; } 
        public string Email { get; set; } 
        public string velacion { get; set; } 
        public string noContrato { get; set; } 
        public string ataud { get; set; } 
        public string urna { get; set; } 
        public double importeContratado { get; set; } 
        public double importeAbonado { get; set; } 
        public double saldo { get; set; } 
        public double subtotal { get; set; } 
        public double iva { get; set; } 
        public double total { get; set; } 
    }
}
