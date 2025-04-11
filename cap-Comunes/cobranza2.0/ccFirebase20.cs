using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static cap_Comunes.ccFireBase;

namespace cap_Comunes
{
    public class ccFirebase20
    {
        private string authSecret { get; set; }
        private string basePath { get; set; }

        public IFirebaseClient client;
        private IFirebaseConfig config;


        public ccFirebase20()
        {
            ConfigurationManager.RefreshSection("appSettings");
            authSecret = ConfigurationManager.AppSettings["AuthSecret"];
            basePath = ConfigurationManager.AppSettings["BasePath"];

            string productivo = ConfigurationManager.AppSettings["productivo"];
            if (productivo.Equals("true"))
            {
                config = new FirebaseConfig
                {
                    AuthSecret = "NGCahFpAuJg7xMF4PRVwN5XzTr0vcrBZVfwgOT2Q",
                    BasePath = "https://cobranzadigital2-a5ab6-default-rtdb.firebaseio.com/"
                };
            }
            else
            {
                config = new FirebaseConfig
                {
                    AuthSecret = "a57fe66830d8d2c4d4b69a8c9d711f479de89bcd",
                    BasePath = "https://cob20-99ee8-default-rtdb.firebaseio.com/"
                };
            }

            client = new FireSharp.FirebaseClient(config);
        }

        public bool termino = false;

        public DataTable consultarRecibosCobranza()
        {

            DataTable listado = new DataTable();
            listado.Columns.Add("tipoRecibo");
            listado.Columns.Add("bancoM1");
            listado.Columns.Add("bancoM2");
            listado.Columns.Add("cliente");
            listado.Columns.Add("concepto");
            listado.Columns.Add("contrato");
            listado.Columns.Add("ctaDestinoM1");
            listado.Columns.Add("ctaDestinoM2");
            listado.Columns.Add("descrip");
            listado.Columns.Add("flagContrato");
            listado.Columns.Add("fecha");
            listado.Columns.Add("folio");
            listado.Columns.Add("metodo1");
            listado.Columns.Add("metodo2");
            listado.Columns.Add("montoM1");
            listado.Columns.Add("montoM2");
            listado.Columns.Add("monto");
            listado.Columns.Add("montoTotal");
            listado.Columns.Add("nombreTM1");
            listado.Columns.Add("nombreTM2");
            listado.Columns.Add("numContSol");
            listado.Columns.Add("nota");
            listado.Columns.Add("recibio");
            listado.Columns.Add("sucursal");
            listado.Columns.Add("terminacionM1");
            listado.Columns.Add("terminacionM2");
            listado.Columns.Add("tipoPago");


            FirebaseResponse response = client.Get("Recibos/");
            var list = response.ResultAs<Dictionary<string, Dictionary<string, Dictionary<string, DataRecibosCaja>>>>()
                            ?? new Dictionary<string, Dictionary<string, Dictionary<string, DataRecibosCaja>>>();


            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, DataRecibosCaja>>> sucursales in list)
            {
                string _nameSucursal = sucursales.Key;

                foreach (KeyValuePair<string, Dictionary<string, DataRecibosCaja>> recibo in sucursales.Value)
                {
                    foreach (KeyValuePair<string, DataRecibosCaja> data in recibo.Value)
                    {
                        string _folioRecibo = data.Key;
                        DataRecibosCaja _r = data.Value;

                        DataRow dr = listado.NewRow();
                        if (!string.IsNullOrEmpty(recibo.Key)) dr["tipoRecibo"] = recibo.Key;
                        else dr["tipoRecibo"] = "";


                        if (!string.IsNullOrEmpty(_r.bancoM1)) dr["bancoM1"] = _r.bancoM1;
                        else dr["bancoM1"] = "";


                        if (!string.IsNullOrEmpty(_r.bancoM2)) dr["bancoM2"] = _r.bancoM2;
                        else dr["bancoM2"] = "";


                        if (!string.IsNullOrEmpty(_r.cliente)) dr["cliente"] = _r.cliente;
                        else dr["cliente"] = "";


                        if (!string.IsNullOrEmpty(_r.concepto)) dr["concepto"] = _r.concepto;
                        else dr["concepto"] = "";


                        if (!string.IsNullOrEmpty(_r.contrato)) dr["contrato"] = _r.contrato;
                        else dr["contrato"] = "";


                        if (!string.IsNullOrEmpty(_r.ctaDestinoM1)) dr["ctaDestinoM1"] = _r.ctaDestinoM1;
                        else dr["ctaDestinoM1"] = "";


                        if (!string.IsNullOrEmpty(_r.ctaDestinoM2)) dr["ctaDestinoM2"] = _r.ctaDestinoM2;
                        else dr["ctaDestinoM2"] = "";


                        if (!string.IsNullOrEmpty(_r.descrip)) dr["descrip"] = _r.descrip;
                        else dr["descrip"] = "";

                        if (!string.IsNullOrEmpty(_r.flagContrato)) dr["flagContrato"] = _r.flagContrato;
                        else dr["flagContrato"] = "";

                        if (!string.IsNullOrEmpty(_r.fecha)) dr["fecha"] = _r.fecha;
                        else dr["fecha"] = "";

                        if (!string.IsNullOrEmpty(_r.folio)) dr["folio"] = _r.folio;
                        else dr["folio"] = "";

                        if (!string.IsNullOrEmpty(_r.metodo1)) dr["metodo1"] = _r.metodo1;

                        else dr["metodo1"] = "";

                        if (!string.IsNullOrEmpty(_r.metodo2)) dr["metodo2"] = _r.metodo2;

                        else dr["metodo2"] = "";
                        if (!string.IsNullOrEmpty(_r.montoM1)) dr["montoM1"] = _r.montoM1;

                        else dr["montoM1"] = "";

                        if (!string.IsNullOrEmpty(_r.montoM2)) dr["montoM2"] = _r.montoM2;

                        else dr["montoM2"] = "";

                        if (!string.IsNullOrEmpty(_r.monto)) dr["monto"] = _r.monto;

                        else dr["monto"] = "";

                        if (!string.IsNullOrEmpty(_r.montoTotal)) dr["montoTotal"] = _r.montoTotal;

                        else dr["montoTotal"] = "";

                        if (!string.IsNullOrEmpty(_r.nombreTM1)) dr["nombreTM1"] = _r.nombreTM1;

                        else dr["nombreTM1"] = "";

                        if (!string.IsNullOrEmpty(_r.nombreTM2)) dr["nombreTM2"] = _r.nombreTM2;

                        else dr["nombreTM2"] = "";

                        if (!string.IsNullOrEmpty(_r.numContSol)) dr["numContSol"] = _r.numContSol;

                        else dr["numContSol"] = "";

                        if (!string.IsNullOrEmpty(_r.nota)) dr["nota"] = _r.nota;

                        else dr["nota"] = "";

                        if (!string.IsNullOrEmpty(_r.recibio)) dr["recibio"] = _r.recibio;

                        else dr["recibio"] = "";

                        if (!string.IsNullOrEmpty(_r.sucursal)) dr["sucursal"] = _r.sucursal;

                        else dr["sucursal"] = "";

                        if (!string.IsNullOrEmpty(_r.terminacionM1)) dr["terminacionM1"] = _r.terminacionM1;

                        else dr["terminacionM1"] = "";

                        if (!string.IsNullOrEmpty(_r.terminacionM2)) dr["terminacionM2"] = _r.terminacionM2;

                        else dr["terminacionM2"] = "";

                        if (!string.IsNullOrEmpty(_r.tipoPago)) dr["tipoPago"] = _r.tipoPago;

                        else dr["tipoPago"] = "";



                        listado.Rows.Add(dr);
                    }
                }
            }
            return listado;
        }
        public void deleteFoliosRecibosCobranza(string sucursal, string tipo, string folio)
        {
            FirebaseResponse response = client.Delete("Recibos/" + sucursal + "/" + tipo + "/" + folio);
        }
        public bool insertFoliosRecibosCobranza(string sucursal, string tipo, DataFolios sol)
        {
            SetResponse response = client.Set("FoliosRecibos/" + sucursal + "/" + tipo, sol);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        // L I S T A D O S   DE   C O B R A N Z A 
        public async void SincronizarListadoCobranza(int ruta, Dictionary<string, dataListadoCobranza> data)
        {
            FirebaseResponse r = await client.DeleteAsync("Cobranza/Listado/Ruta" + ruta+"/");
            SetResponse response = await client.SetAsync("Cobranza/Listado/Ruta" + ruta + "/" ,data);
            SetResponse response1 = await client.SetAsync("Canales/9903/" + ruta + "/borrar", "1");
            termino = true;
        }
        public async void SincronizarCoordenadasCobranza(int ruta, string contrato, string latitud, string longitud)
        {
            SetResponse response = await client.SetAsync("Cobranza/Listado/Ruta" + ruta + "/" + contrato + "/latitud", latitud);
            SetResponse response1 = await client.SetAsync("Cobranza/Listado/Ruta" + ruta + "/" + contrato + "/longitud", longitud);
        }
        public async void AgregarAListado(int ruta, Dictionary<string, dataListadoCobranza> data)
        {
            foreach(var a in data)
            {
                SetResponse response = await client.SetAsync("Cobranza/Listado/Ruta" + ruta + "/"+a.Key,a.Value);
            }
            termino = true;
        }

        public Dictionary<string, Dictionary<string, dataListadoCobranza>> ConsultarListadoCobranza()
        {
            FirebaseResponse response = client.Get("Cobranza/Listado/");
            Dictionary<string, Dictionary<string, dataListadoCobranza>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataListadoCobranza>>>();
            return listado;
        }
        public Dictionary<string, Dictionary<string, dataListadoSupervisores>> ConsultarListadoSupervisores()
        {
            FirebaseResponse response = client.Get("Supervision/Listados/");
            Dictionary<string, Dictionary<string, dataListadoSupervisores>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataListadoSupervisores>>>();
            return listado;
        }


        // B I T A C O R A   D E   C O B R A N Z A
        public Dictionary<string, Dictionary<string, dataRecibo>> ConsultarCobranza()
        {
            FirebaseResponse response = client.Get("Cobranza/Bitacora/");
            Dictionary<string, Dictionary<string, dataRecibo>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataRecibo>>>();
            return listado;
        }
        public Dictionary<string, Dictionary<string, dataSupervisiones>> ConsultarSupervisores()
        {
            FirebaseResponse response = client.Get("Supervision/Bitacoras/");
            Dictionary<string, Dictionary<string, dataSupervisiones>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataSupervisiones>>>();
            return listado;
        }

        public Dictionary<string, Dictionary<string, dataReimpresion>> ConsultarReimpresiones()
        {
            FirebaseResponse response = client.Get("Reimpresiones/");
            Dictionary<string, Dictionary<string, dataReimpresion>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataReimpresion>>>();
            return listado;
        }
        public async void EliminarReimpresion(string ruta, string folio)
        {
            FirebaseResponse response = await client.DeleteAsync($"Reimpresiones/Ruta{ruta}/{folio}/");

        }
        public int DepurarBitacoraCobranza(List<string> ids)
        {
            int c = 0;
            foreach (var i in ids) // Recorre RUTAS
            {
                this.BorrarReciboAsync(i);
                c++;
                
            }
            termino = true; // Pues esto no sirvio pa nada
            return c;
        }
        public async Task<int> DepurarBitacoraSupervisores(List<string> ids)
        {
            int c = 0;
            foreach (var i in ids) // Recorre RUTAS
            {
                try
                {
                    await BorrarSupervisionAsync(i); // Usa await en lugar de Wait
                    c++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar {i} de Firebase: {ex.Message}");
                }
            }
            return c;
        }


        public async Task BorrarSupervisionAsync(string idfirebase)
        {
            try
            {
                FirebaseResponse response = await client.DeleteAsync("Supervision/Bitacoras/" + idfirebase).ConfigureAwait(false);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"Bitácora {idfirebase} eliminada correctamente.");
                }
                else
                {
                    Console.WriteLine($"Error al eliminar la bitácora {idfirebase}: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al intentar eliminar {idfirebase}: {ex.Message}");
            }
        }

        public async void BorrarReciboAsync(string idfirebase)
        {
            FirebaseResponse response = await client.DeleteAsync("Cobranza/Bitacora/"+idfirebase);

        }
        public void BorrarCobrosCancelados(string directorio)
        {
            FirebaseResponse response = client.Delete("Cobranza/Bitacora/"+directorio);

        }


        // B I T A C O R A   D E   A V I S O S
        public Dictionary<string, Dictionary<string, dataAviso>> ConsultarAvisos()
        {
            FirebaseResponse response = client.Get("Avisos/Bitacora/");
            Dictionary<string, Dictionary<string, dataAviso>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataAviso>>>();

            return listado;
        }
        public async Task DepurarBitacoraAvisos(Dictionary<string, DataTable> bitacoras)
        {
            foreach (var i in bitacoras) // Recorre RUTAS
            {
                foreach (DataRow row in i.Value.Rows)
                {
                    string firebasePath = "Avisos/Bitacora/Ruta" + row["RUTA"] + "/" + row["idfirebase"] + "/";

                    try
                    {
                        await client.DeleteAsync(firebasePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al eliminar datos en Firebase: " + ex.Message);
                    }
                }
            }
            termino = true;
        }

        //public async void DepurarBitacoraAvisos(Dictionary<string, DataTable> bitacoras) 
        //{
        //    foreach (var i in bitacoras) // Recorre RUTAS
        //    {
        //        foreach (DataRow row in i.Value.Rows)
        //        {
        //            FirebaseResponse response = await client.DeleteAsync("Avisos/Bitacora/Ruta" + row["RUTA"] + "/" + row["idfirebase"] + "/");
        //        }
        //    }

        //    termino = true;
        //}


        // L I S T A D O   S O L I C I T U D E S
        public async void SincronizarListadoSolicitudes(Dictionary<string, Dictionary<string, dataListadoSolicitudes>> data)
        {

            FirebaseResponse r = await client.DeleteAsync("Verificaciones/Listado/");

            foreach (var a in data)
            {
                SetResponse response = await client.SetAsync("Verificaciones/Listado/Ruta" + a.Key + "/", a.Value);
            }

            termino = true;
        }

        public Dictionary<string, Dictionary<string, dataListadoSolicitudes>> ConsultarListadoSolicitudes()
        {
            FirebaseResponse response = client.Get("Verificaciones/Listado/");
            Dictionary<string, Dictionary<string, dataListadoSolicitudes>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataListadoSolicitudes>>>();
            return listado;
        }


        // BITACORA DE VERIFICACIONES
        public Dictionary<string, Dictionary<string, dataVerificacion>> ConsultarVerificaciones()
        {
            FirebaseResponse response = client.Get("Verificaciones/Bitacora/");
            Dictionary<string, Dictionary<string, dataVerificacion>> listado = response.ResultAs<Dictionary<string, Dictionary<string, dataVerificacion>>>();

            return listado;
        }
        public async void DepurarBitacoraVerificaciones(DataTable bitacoras)
        {
            foreach (DataRow row in bitacoras.Rows)
            {
                FirebaseResponse response = await client.DeleteAsync("Verificaciones/Bitacora/Ruta" + row["RUTA"] + "/" + row["idfirebase"] + "/");
            }
            termino = true;
        }


    }
    public class dataListadoCobranza
    {
        public string abono { get; set; }
        public string bandera { get; set; }
        public string cliente { get; set; }
        public string contrato { get; set; }
        public string cobroPendiente { get; set; }
        public string correo { get; set; }
        public string costo { get; set; }
        public string domicilio { get; set; }
        public string frecuenciaAbono { get; set; }
        public string latitud { get; set; }
        public string limInferior2 { get; set; }
        public string limSuperior1 { get; set; }
        public string limSuperior2 { get; set; }
        public string longitud { get; set; }
        public string numeroPago { get; set; }
        public string numQuincena { get; set; }
        public string observaciones { get; set; }
        public string pagoRequerido { get; set; }
        public string ruta { get; set; }
        public string saldo { get; set; }
        public string solicitud { get; set; }
        public string telefono { get; set; }
        public string ultimoPago { get; set; }
        public string ultimoPagoPendiente { get; set; }
        public string ultimoReciboCobrado { get; set; }
        public string vencido { get; set; }
        public string zona { get; set; }


    }
    public class dataListadoSupervisores
    {
        public string abonado { get; set; }
        public string cliente { get; set; }
        public string contrato { get; set; }
        public string domicilio { get; set; }
        public string fechaEnviado { get; set; }
        public string frecuencia { get; set; }
        public string nota { get; set; }
        public string frecuenciaAbono { get; set; }
        public string observaCob { get; set; }
        public string saldo { get; set; }
    }
    public class dataSupervisiones
    {
        public string acepta { get; set; }
        public string nombreSupervisor { get; set; }
        public string contrato { get; set; }
        public string fecha { get; set; }
        public string fechaEnv { get; set; }
        public string hora { get; set; }
        public string horaEnv { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string notasCob { get; set; }
        public string observaciones { get; set; }
        public string propuesta { get; set; }
        public string rutaCob { get; set; }
        public string rutaSup { get; set; }
        public string suspendido { get; set; }
        public string catSusp { get; set; }
        public string categoria { get; set; }
        public string asesor{ get; set; }
        public string promCentral { get; set; }
    }
    public class dataReimpresion
    {
        public string ruta { get; set; }
        public string cancelado { get; set; }
        public string contrato { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public double importe { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string nopago { get; set; }
        public string recibo { get; set; }

    }

    public class dataRecibo
    {
        public string calificacion { get; set; }
        public string cancelado { get; set; }
        public string contrato { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string nopago { get; set; }
        public string notas { get; set; }
        public string pago { get; set; }
        public string recibo { get; set; }
        public string ruta { get; set; }
    }
    public class dataAviso
    {
        public string contrato { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string observacion { get; set; }
        public string ruta { get; set; }
    }
    public class dataListadoSolicitudes
    {
        public string cliente { get; set; }
        public string tipoPlan { get; set; }
        public string costo { get; set; }
        public string zona { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string celAsesor { get; set; }
        public string pagoRequerido { get; set; }
        public string frecuenciaAbono { get; set; }
        public string asesorVenta { get; set; }
        public string reestructura { get; set; }

        public string latitud { get; set; }
        public string longitud { get; set; }
        public string observaciones { get; set; }

    }
    public class dataVerificacion
    {
        public string acepta { get; set; }
        public string comentario { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string id { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string observacion { get; set; }
        public string ruta { get; set; }
        public string solicitud { get; set; }
    }


}
