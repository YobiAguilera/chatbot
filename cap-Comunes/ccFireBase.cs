using Firebase.Database;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Storage;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cap_Entidades;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace cap_Comunes
{
    public class ccFireBase
    {
        private string authSecret { get; set; }
        private string basePath { get; set; }

        public DataTable listado;


        IFirebaseClient client;
        IFirebaseConfig config;


        public ccFireBase()
        {
            ConfigurationManager.RefreshSection("appSettings");
            authSecret = ConfigurationManager.AppSettings["AuthSecret"];
            basePath = ConfigurationManager.AppSettings["BasePath"];

            string productivo = ConfigurationManager.AppSettings["productivo"];
            if (productivo.Equals("true"))
            {

                config = new FirebaseConfig
                {
                    AuthSecret = "7JaElBGeSLJ6TqBad9F6aCtLm5UrEGFnza58hKqN",
                    BasePath = "https://supermoreh-399bc.firebaseio.com/"
                };
            }
            else
            {
                config = new FirebaseConfig
                {
                    AuthSecret = "g9ABN8rH2XSf8NUJa9ie399MbLZNzIlAn3c5A5u8",
                    BasePath = "https://pruebasmoreh-default-rtdb.firebaseio.com/"
                };
            }

            config = new FirebaseConfig
            {
                AuthSecret = "7JaElBGeSLJ6TqBad9F6aCtLm5UrEGFnza58hKqN",
                BasePath = "https://supermoreh-399bc.firebaseio.com/"
            };
            client = new FireSharp.FirebaseClient(config);
        }
        public int probarConexion(string secret, string path)
        {
            config = new FirebaseConfig
            {
                AuthSecret = secret,
                BasePath = path
            };
            client = new FireSharp.FirebaseClient(config);
            if (config.Host != null)
                return 1;
            else
                return 0;
        }
        private FirestoreDb firestore;
        //private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"supermoreh-399bc-firebase-adminsdk-v1y2b-f07998eab2.json";


        #region COMPRAS


        public class DataOrdenesCompraVehiculos
        {
            public string estatus { get; set; }
            public DateTime? fecha { get; set; }
            public DateTime? fechaRev { get; set; }
            public string folioSol { get; set; }
            public string hora { get; set; }
            public string kilometraje { get; set; }
            public string observaciones { get; set; }
            public string placas { get; set; }
            public string proveedor { get; set; }
            public string fechaEntrega { get; set; }
            public string revisa { get; set; }
            public string solicitante { get; set; }
            public string uuid { get; set; }
            public string serie { get; set; }
            public string marca { get; set; }
            public string modelo { get; set; }
            public string sucursal { get; set; }
            public string fotoCarro { get; set; }
            public string usuarioAutorizo { get; set;}
            public string usuarioRechazo { get; set; }
            public string fotoPresupuesto { get; set; }
            public DateTime? fechaIng { get; set; }
            public string flagPagado { get; set; }
            public DateTime? fechaAtendidaCancelada { get; set; }
            public List<DataDetalleCompraVehiculos> Detalles { get; set; }

            public DataOrdenesCompraVehiculos()
            {
                Detalles = new List<DataDetalleCompraVehiculos>();
            }
        }

        public class DataDetalleCompraVehiculos
        {
            public int id { get; set; }
            public string concepto { get; set; }
            public double costo { get; set; }
            public string folioSol { get; set; }
            public int cantidad { get; set; }
            public string marca { get; set; }
        }
        public class DataManoObra
        {
            public int id { get; set; }
            public string concepto { get; set; }
            public double costo { get; set; }
            public string folioSol { get; set; }
        }

        public class DataCostosAdicionales
        {
            public int id { get; set; }
            public double costo { get; set; }
            public string concepto { get; set; }
            public string folioSol { get; set; }
        }
        public class DataRevisionesTaller
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
        public class DataCompra
        {
            public string comprador { get; set; }
            public string estatus { get; set; }
            public string fecha { get; set; }
            public string fechaRecepcion { get; set; }
            public string folio { get; set; }
            public string horaRecepcion { get; set; }
            public string observaciones { get; set; }
            public string proveedor { get; set; }
            public string recibio { get; set; }
            public string inventariable { get; set; }
            public string iva { get; set; }
            public int idProveedor { get; set; }
            public int id_usuario_autoriza { get; set; }
            public int id_usuario_baja { get; set; }
            public int id_usuario { get; set; }

        }
        public class DataArtCompra
        {
            public string articulo { get; set; }
            public string cantidad { get; set; }
            public string categoria { get; set; }
            public string folio { get; set; }
            public string recibido { get; set; }
            public string sku { get; set; }
            public string transito { get; set; }
            public decimal precio { get; set; }
        }

        public bool insertDetCompras(string idFirestore, List<DataArtCompra> datCompra, bool modificacion)
        {
            bool ret = false;
            int contadorReg = 1;
            if (modificacion == true)
            {
                FirebaseResponse response = client.Delete("Control/Compras/Detalle/D" + idFirestore);
                if (!response.StatusCode.ToString().Equals("OK")) return false;
            }

            foreach (var oDetalle in datCompra)
            {

                SetResponse response2 = client.Set("Control/Compras/Detalle/D" + idFirestore + "/" + contadorReg, oDetalle);

                if (response2.StatusCode.ToString().Equals("OK")) ret = true;
                else
                {
                    ret = false;
                    break;
                }
                contadorReg++;
            }
            return ret;
        }

        public bool insertOrdCompras(string idFirestore, DataCompra datCompra)

        {
            SetResponse response = client.Set("Control/Compras/Encabezados/E" + idFirestore, datCompra);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public DataCompra getOrdCompraSpecific(string idFirestore)
        {
            FirebaseResponse response;

            response = client.Get("Control/Compras/Encabezados/E" + idFirestore);

            DataCompra data = response.ResultAs<DataCompra>();

            if (data != null)
            {

                return data;
            }
            data = null;
            return data;

        }
        public Dictionary<string, (DataCompra encabezado, List<DataArtCompra> detalles)> obtenerOrdenesConDetallesFirebase()
        {
            try
            {
                Dictionary<string, (DataCompra, List<DataArtCompra>)> ordenes = new Dictionary<string, (DataCompra, List<DataArtCompra>)>();

                FirebaseResponse responseEncabezados = client.Get("Control/Compras/Encabezados/");
                Dictionary<string, DataCompra> encabezados = responseEncabezados.ResultAs<Dictionary<string, DataCompra>>();

                if (encabezados != null)
                {
                    foreach (var entry in encabezados)
                    {
                        string idOrden = entry.Key;

                        FirebaseResponse responseDetalles = client.Get($"Control/Compras/Detalle/D{idOrden}/");
                        List<DataArtCompra> detalles = responseDetalles.ResultAs<List<DataArtCompra>>() ?? new List<DataArtCompra>();

                        ordenes[idOrden] = (entry.Value, detalles);
                    }
                }

                return ordenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las órdenes de Firebase: " + ex.Message);
            }
        }


        public void getOrdCompra()
        {
            FirebaseResponse response;
            listado = new DataTable();
            listado.Columns.Add("comprador");
            listado.Columns.Add("estatus");
            listado.Columns.Add("fecha");
            listado.Columns.Add("fechaRecepcion");
            listado.Columns.Add("folio");
            listado.Columns.Add("horaRecepcion");
            listado.Columns.Add("observaciones");
            listado.Columns.Add("proveedor");
            listado.Columns.Add("recibio");
            listado.Columns.Add("inventariable");

            // Obtengo la rama correspondiente a la ruta seleccionada

            response = client.Get("Control/Compras/Encabezados/");

            Dictionary<string, DataCompra> data = response.ResultAs<Dictionary<string, DataCompra>>();
            if (data != null)
            {
                foreach (var entry in data)
                {

                    DataRow dr = listado.NewRow();

                    if (!string.IsNullOrEmpty(data[entry.Key].comprador)) dr["comprador"] = data[entry.Key].comprador;
                    else dr["comprador"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].estatus)) dr["estatus"] = data[entry.Key].estatus;
                    else dr["estatus"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].fecha)) dr["fecha"] = data[entry.Key].fecha;
                    else dr["fecha"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].fechaRecepcion)) dr["fechaRecepcion"] = data[entry.Key].fechaRecepcion;
                    else dr["fechaRecepcion"] = "";

                    if (!string.IsNullOrEmpty(entry.Key)) dr["folio"] = entry.Key;
                    else dr["folio"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].horaRecepcion)) dr["horaRecepcion"] = data[entry.Key].horaRecepcion;
                    else dr["horaRecepcion"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].observaciones)) dr["observaciones"] = data[entry.Key].observaciones;
                    else dr["observaciones"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].proveedor)) dr["proveedor"] = data[entry.Key].proveedor;
                    else dr["proveedor"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].recibio)) dr["recibio"] = data[entry.Key].recibio;
                    else dr["recibio"] = "";

                    if (!string.IsNullOrEmpty(data[entry.Key].inventariable)) dr["inventariable"] = data[entry.Key].inventariable;
                    else dr["inventariable"] = "";

                    listado.Rows.Add(dr);
                }
            }
            else
            {
                listado = new DataTable();
            }
        }
        public bool eliminarOrdenFirebase(string idOrden)
        {
            try
            {
                FirebaseResponse response = client.Delete("Control/Compras/Encabezados/" + idOrden);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string numeroOrden = idOrden.TrimStart('E');

                    client.Delete("Control/Compras/Detalle/D" + numeroOrden);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la orden de Firebase: " + ex.Message);
            }
        }


        #endregion



        #region INVENTARIO

        [FirestoreData]
        public class DataInventario
        {
            [FirestoreProperty]
            public string almacen { get; set; }
            [FirestoreProperty]
            public string articulo { get; set; }
            [FirestoreProperty]
            public string cantidad { get; set; }
            [FirestoreProperty]
            public string categoria { get; set; }
            [FirestoreProperty]
            public string fechaRec { get; set; }
            [FirestoreProperty]
            public string firestoreID { get; set; }
            [FirestoreProperty]
            public string folioOC { get; set; }
            [FirestoreProperty]
            public string proveedor { get; set; }
            [FirestoreProperty]
            public string recibio { get; set; }
            [FirestoreProperty]
            public string sku { get; set; }
            [FirestoreProperty]
            public string transito { get; set; }
            [FirestoreProperty]
            public string traslados { get; set; }
            [FirestoreProperty]
            public string danio { get; set; }
            [FirestoreProperty]
            public string nota { get; set; }
            [FirestoreProperty]
            public string estatus { get; set; }
        }

        [FirestoreData]
        public class DataMovInventario
        {
            [FirestoreProperty]
            public string actividad { get; set; }
            [FirestoreProperty]
            public string fecha { get; set; }
            [FirestoreProperty]
            public string hora { get; set; }
            [FirestoreProperty]
            public string observacion { get; set; }
            [FirestoreProperty]
            public string usuario { get; set; }
        }
        [FirestoreData]
        public class DataBitacoraMovInv
        {
            [FirestoreProperty]
            public string almacen { get; set; }
            [FirestoreProperty]
            public string articulo { get; set; }
            [FirestoreProperty]
            public string cantidad { get; set; }
            [FirestoreProperty]
            public string categoria { get; set; }
            [FirestoreProperty]
            public string fechaMov { get; set; }
            [FirestoreProperty]
            public string fechaRec { get; set; }
            [FirestoreProperty]
            public string fidArt { get; set; }
            [FirestoreProperty]
            public string fidMov { get; set; }
            [FirestoreProperty]
            public string folioOC { get; set; }
            [FirestoreProperty]
            public string horaMov { get; set; }
            [FirestoreProperty]
            public string proveedor { get; set; }
            [FirestoreProperty]
            public string recibio { get; set; }
            [FirestoreProperty]
            public string registro { get; set; }
            [FirestoreProperty]
            public string sku { get; set; }
            [FirestoreProperty]
            public string tipoMov { get; set; }
            [FirestoreProperty]
            public string nota { get; set; }
        }
        public async Task<List<DataInventario>> getInventarios()
        {
            List<ccFireBase.DataInventario> lista = new List<ccFireBase.DataInventario>();
            try
            {
                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                FirestoreDb database = FirestoreDb.Create("supermoreh-399bc");

                Query qref = database.Collection("Inventario");
                QuerySnapshot snap = await qref.GetSnapshotAsync();

                foreach (DocumentSnapshot docsnap in snap)
                {
                    DataInventario inventario = docsnap.ConvertTo<DataInventario>();
                    lista.Add(inventario);
                }
                return lista;
            }
            catch
            {
                return lista;
            }
        }

        public async Task<List<DataMovInventario>> getMovInventario()
        {
            List<ccFireBase.DataMovInventario> lista = new List<ccFireBase.DataMovInventario>();
            try
            {

                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                FirestoreDb database = FirestoreDb.Create("supermoreh-399bc");

                Query qref = database.Collection("BitacoraInv");
                QuerySnapshot snap = await qref.GetSnapshotAsync();


                foreach (DocumentSnapshot docsnap in snap)
                {
                    DataMovInventario inventario = docsnap.ConvertTo<DataMovInventario>();
                    lista.Add(inventario);
                }

                return lista;

            }
            catch
            {
                return lista;
            }
        }
        public async Task<List<DataBitacoraMovInv>> getBitacoraMovInv()
        {
            List<ccFireBase.DataBitacoraMovInv> lista = new List<ccFireBase.DataBitacoraMovInv>();
            try
            {

                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                FirestoreDb database = FirestoreDb.Create("supermoreh-399bc");

                Query qref = database.Collection("BitacoraMovsInv");
                QuerySnapshot snap = await qref.GetSnapshotAsync();

                foreach (DocumentSnapshot docsnap in snap)
                {
                    DataBitacoraMovInv inventario = docsnap.ConvertTo<DataBitacoraMovInv>();
                    lista.Add(inventario);
                }

                return lista;

            }
            catch
            {
                return lista;
            }
        }

        public async void deleteMovInventario()
        {

            try
            {
                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                FirestoreDb database = FirestoreDb.Create("supermoreh-399bc");

                CollectionReference reference = database.Collection("BitacoraInv");

                QuerySnapshot snapshot = await reference.GetSnapshotAsync();

                foreach (DocumentSnapshot docsnap in snapshot.Documents)
                {
                    await docsnap.Reference.DeleteAsync();
                }

            }
            catch (Exception ex)
            {

            }

        }
        public async void deleteBitacoraMovInv()
        {
            try
            {
                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                FirestoreDb database = FirestoreDb.Create("supermoreh-399bc");

                CollectionReference reference = database.Collection("BitacoraMovsInv");

                QuerySnapshot snapshot = await reference.GetSnapshotAsync();

                foreach (DocumentSnapshot docsnap in snapshot.Documents)
                {
                    await docsnap.Reference.DeleteAsync();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<bool> deleteArticulo(string sku)
        {

            try
            {
                FirestoreDb database = FirestoreDb.Create("supermoreh-399bc");

                DocumentReference DOC = database.Collection("Articulos").Document(sku);

                DOC.DeleteAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool insertArticulosFirestore(string categoria, string descripcion, string idunico, string minimo, string nombre,
          string proveedor, string transito, string unidad, string sku)
        {

            try
            {
                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
                //FirestoreDb database =  FirestoreDb.Create("supermoreh-399bc");
                firestore = FirestoreDb.Create("supermoreh-399bc");

                DocumentReference DOC = firestore.Collection("Articulos").Document(sku);
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"alerta","0"},
                {"categoria", categoria},
                {"descripcion", descripcion},
                {"idunico", idunico},
                {"minimo", minimo},
                {"nombre", nombre},
                {"proveedor", proveedor},
                {"transito", transito},
                {"unidad", unidad}
            };

                DOC.SetAsync(data);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo salió mal... \n" + ex.Message, "Error con Firestore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion


        #region VEHICULOS / ORDENES COMPRA VEHICULOS
        public async Task<bool> deleteVehiculo(string serie)
        {
            if (string.IsNullOrEmpty(serie))
            {
                MessageBox.Show("El identificador del vehículo está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                FirebaseResponse response = await client.DeleteAsync($"Vehiculos/{serie}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"No se pudo eliminar el vehículo. StatusCode: {response.StatusCode}", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar vehículo en Firebase: {ex.Message}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> GuardarVehiculosEnFirebase(DataTable vehiculos)
        {
            bool todosLosRegistrosGuardados = true;
            foreach (DataRow vehiculo in vehiculos.Rows)
            {
                try
                {
                    var data = new
                    {
                        anio = vehiculo["anio"].ToString(),
                        color = vehiculo["color"].ToString(),
                        foto = vehiculo["foto"].ToString(),
                        marca = vehiculo["marca"].ToString(),
                        modelo = vehiculo["modelo"].ToString(),
                        placas = vehiculo["placas"].ToString(),
                        serie = vehiculo["serie"].ToString(),
                        sucursal = vehiculo["sucursal"].ToString()
                    };

                    var response = await client.SetAsync($"Vehiculos/{vehiculo["serie"].ToString()}", data);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        todosLosRegistrosGuardados = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el vehículo {vehiculo["serie"]}: {ex.Message}");
                    todosLosRegistrosGuardados = false;
                }
            }
            return todosLosRegistrosGuardados;
        }
        

        public async Task<bool> GuardarPersonalEnFirebase(DataTable personal)
        {
            bool todosLosRegistrosGuardados = true;

            foreach (DataRow persona in personal.Rows)
            {
                try
                {
                    string identificador = string.IsNullOrEmpty(persona["numeroEmpleado"].ToString()) ? Guid.NewGuid().ToString() : persona["numeroEmpleado"].ToString();

                    var data = new
                    {
                        nombre = persona["nombre"].ToString(),
                        numeroEmpleado = persona["numeroEmpleado"] != null ? persona["numeroEmpleado"].ToString() : null,
                        sucursal = persona["sucursal"].ToString(),
                        email = persona["email"].ToString(),
                    };

                    var response = await client.SetAsync($"Catalogos/Operativos/{identificador}", data);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        todosLosRegistrosGuardados = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el registro de personal con identificador {persona["id"]}: {ex.Message}");
                    todosLosRegistrosGuardados = false;
                }
            }
            return todosLosRegistrosGuardados;
        }

        public async Task<bool> InsertarVehiculoFirebase(string anio, string color, string foto, string marca, string modelo, string placas, 
            string serie, string sucursal, int activo, int asesor)
        {
            try
            {
                var vehiculo = new
                {
                    anio,
                    color,
                    foto,
                    marca,
                    modelo,
                    placas,
                    serie,
                    sucursal,
                    activo,
                    asesor
                };

                var response = await client.SetAsync($"Vehiculos/{serie}", vehiculo);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar vehículo en Firebase: {ex.Message}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> ActualizarVehiculoFirebase(string anio, string color, string foto, string marca, string modelo,
            string placas, string serie, string sucursal, int activo, int asesor)
        {
            try
            {
                var vehiculo = new
                {
                    anio,
                    color,
                    foto,
                    marca,
                    modelo,
                    placas,
                    serie,
                    sucursal,
                    activo,
                    asesor
                };

                FirebaseResponse response = await client.UpdateAsync($"Vehiculos/{serie}", vehiculo);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar vehículo en Firebase: {ex.Message}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<List<dataVehiculos>> GetAllVehiculosAsync()
        {
            List<dataVehiculos> lista = new List<dataVehiculos>();
            try
            {
                FirebaseResponse response = await client.GetAsync("Vehiculos");
                if (response.Body != "null")
                {
                    var vehiculos = response.ResultAs<Dictionary<string, Dictionary<string, string>>>();
                    foreach (var item in vehiculos)
                    {
                        dataVehiculos vehiculo = new dataVehiculos
                        {
                            anio = item.Value.ContainsKey("anio") ? item.Value["anio"] : "Desconocido",
                            color = item.Value.ContainsKey("color") ? item.Value["color"] : "Desconocido",
                            foto = item.Value.ContainsKey("foto") ? item.Value["foto"] : "Sin foto",
                            marca = item.Value.ContainsKey("marca") ? item.Value["marca"] : "Desconocido",
                            modelo = item.Value.ContainsKey("modelo") ? item.Value["modelo"] : "Desconocido",
                            placas = item.Value.ContainsKey("placas") ? item.Value["placas"] : "Desconocido",
                            serie = item.Value.ContainsKey("serie") ? item.Value["serie"] : "Desconocido",
                            sucursal = item.Value.ContainsKey("sucursal") ? item.Value["sucursal"] : "Desconocido"
                        };
                        lista.Add(vehiculo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener vehículos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public async Task<List<DataOrdenesCompraVehiculos>> GetAllOrdenesCompraVehiculosAsync()
        {
            List<DataOrdenesCompraVehiculos> lista = new List<DataOrdenesCompraVehiculos>();
            try
            {
                FirebaseResponse response = await client.GetAsync("SolicitudesRep/Encabezados/");
                if (response.Body != "null")
                {
                    var ordenesCompra = response.ResultAs<Dictionary<string, Dictionary<string, string>>>();
                    foreach (var item in ordenesCompra)
                    {
                        DateTime? fecha = null;
                        DateTime? fechaRev = null;
                        DateTime? fechaIng = null;
                        string dateFormat = "dd-MM-yyyy";

                        if (!string.IsNullOrEmpty(item.Value["fecha"]))
                        {
                            if (DateTime.TryParseExact(item.Value["fecha"], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedFecha))
                            {
                                fecha = parsedFecha;
                            }
                        }

                        if (!string.IsNullOrEmpty(item.Value["fechaRev"]))
                        {
                            if (DateTime.TryParseExact(item.Value["fechaRev"], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedFechaRev))
                            {
                                fechaRev = parsedFechaRev;
                            }
                        }

                        if (!string.IsNullOrEmpty(item.Value["fechaIng"]))
                        {
                            if (DateTime.TryParseExact(item.Value["fechaIng"], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedFechaIng))
                            {
                                fechaIng = parsedFechaIng;
                            }
                        }

                        DataOrdenesCompraVehiculos ordenCompra = new DataOrdenesCompraVehiculos
                        {
                            estatus = item.Value["estatus"],
                            fecha = fecha ?? (DateTime?)null,
                            fechaRev = fechaRev ?? (DateTime?)null,
                            fechaEntrega = item.Value["fechaEntrega"],
                            folioSol = item.Value["folioSol"],
                            hora = item.Value["hora"],
                            kilometraje = item.Value["kilometraje"],
                            observaciones = item.Value["observaciones"],
                            placas = item.Value["placas"],
                            proveedor = item.Value["proveedor"],
                            revisa = item.Value["revisa"],
                            solicitante = item.Value["solicitante"],
                            uuid = item.Value["uuid"],
                            fotoCarro = item.Value["fotoCarro"],
                            fotoPresupuesto = item.Value["fotoPresupuesto"],
                            fechaIng = fechaIng ?? (DateTime?)null,
                            flagPagado = item.Value["flagPagado"],
                            modelo = item.Value["modelo"]
                        };
                        lista.Add(ordenCompra);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las órdenes de compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public async Task<List<DataDetalleCompraVehiculos>> GetAllOrdenesCompraDetalleVehiculosAsync()
        {
            List<DataDetalleCompraVehiculos> detallesLista = new List<DataDetalleCompraVehiculos>();
            try
            {
                FirebaseResponse response = await client.GetAsync($"SolicitudesRep/Detalle/");
                if (response.Body != "null")
                {
                    var detallesJson = JToken.Parse(response.Body);

                    foreach (var grupo in detallesJson.Children<JProperty>())
                    {
                        int id = 0;
                        foreach (var detalle in grupo.Value.Children())
                        {
                            if (detalle.Type == JTokenType.Null)
                            {
                                id++;
                                continue; 
                            }

                            if (detalle == null || !detalle.HasValues)
                            {
                                id++;
                                continue;
                            }

                            DataDetalleCompraVehiculos detalleCompra = new DataDetalleCompraVehiculos
                            {
                                id = id, 
                                concepto = detalle["concepto"]?.ToString(),
                                costo = double.TryParse(detalle["costo"]?.ToString(), out double parsedCost) ? parsedCost : 0,
                                folioSol = detalle["folioSol"]?.ToString(),
                                cantidad = int.TryParse(detalle["cantidad"]?.ToString(), out int parsedCantidad) ? parsedCantidad : 0,
                                marca = detalle["marca"]?.ToString()
                            };
                            detallesLista.Add(detalleCompra);
                            id++; 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar detalles: " + e.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return detallesLista;
        }

        public async Task<List<DataManoObra>> GetAllManoObraAsync()
        {
            List<DataManoObra> detallesListaMO = new List<DataManoObra>();
            try
            {
                FirebaseResponse response = await client.GetAsync($"SolicitudesRep/DetalleMO/");
                if (response.Body != "null")
                {
                    var detallesMOJson = JToken.Parse(response.Body);

                    if (detallesMOJson.Type == JTokenType.Object) 
                    {
                        foreach (var grupo in detallesMOJson.Children<JProperty>())
                        {
                            int id = 0;
                            foreach (var detalle in grupo.Value.Children())
                            {
                                if (detalle.Type == JTokenType.Null)
                                {
                                    id++;
                                    continue;
                                }

                                if (detalle == null || !detalle.HasValues)
                                {
                                    id++;
                                    continue;
                                }

                                DataManoObra detalleCompraMO = new DataManoObra
                                {
                                    id = id,
                                    concepto = detalle["concepto"]?.ToString(),
                                    costo = double.TryParse(detalle["costo"]?.ToString(), out double parsedCost) ? parsedCost : 0,
                                    folioSol = detalle["folioSol"]?.ToString()
                                };
                                detallesListaMO.Add(detalleCompraMO);
                                id++;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El formato del JSON recibido no es válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"StackTrace: {e.StackTrace}");
                MessageBox.Show("Error al cargar detalles: " + e.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return detallesListaMO;
        }
        public async Task<List<DataCostosAdicionales>> GetAllCostosAdicionalesAsync()
        {
            List<DataCostosAdicionales> detallesListaCA = new List<DataCostosAdicionales>();
            try
            {
                FirebaseResponse response = await client.GetAsync($"SolicitudesRep/DetalleCA/");
                if (response.Body != "null")
                {
                    var detallesCAJson = JToken.Parse(response.Body);

                    foreach (var grupo in detallesCAJson.Children<JProperty>())
                    {
                        int id = 0;
                        foreach (var detalle in grupo.Value.Children())
                        {
                            if (detalle.Type == JTokenType.Null)
                            {
                                id++;
                                continue;
                            }

                            if (detalle == null || !detalle.HasValues)
                            {
                                id++;
                                continue;
                            }

                            DataCostosAdicionales detalleCompraCA = new DataCostosAdicionales
                            {
                                id = id,
                                concepto = detalle["concepto"]?.ToString(),
                                costo = double.TryParse(detalle["costo"]?.ToString(), out double parsedCost) ? parsedCost : 0,
                                folioSol = detalle["folioSol"]?.ToString()
                            };
                            detallesListaCA.Add(detalleCompraCA);
                            id++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar detalles: " + e.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return detallesListaCA;
        }
        public async Task<List<DataRevisionesTaller>> GetAllRevisionesAsync()
        {
            List<DataRevisionesTaller> revisionesLista = new List<DataRevisionesTaller>();
            try
            {
                FirebaseResponse response = await client.GetAsync($"SolicitudesRep/Revisiones/");
                if (response.Body != "null")
                {
                    var revisionesJson = JToken.Parse(response.Body);

                    foreach (var grupo in revisionesJson.Children<JProperty>())
                    {
                        int id = 0;
                        foreach (var revision in grupo.Value.Children())
                        {
                            if (revision.Type == JTokenType.Null)
                            {
                                id++;
                                continue;
                            }

                            if (revision == null || !revision.HasValues)
                            {
                                id++;
                                continue;
                            }

                            DataRevisionesTaller revisionCompra = new DataRevisionesTaller
                            {
                                id = id,
                                fecha = revision["fecha"]?.ToString(),
                                folioSol = revision["folioSol"]?.ToString(),
                                foto1 = revision["foto1"]?.ToString(),
                                foto2 = revision["foto2"]?.ToString(),
                                foto3 = revision["foto3"]?.ToString(),
                                hora = revision["hora"]?.ToString(),
                                observaciones = revision["observaciones"]?.ToString(),
                                usuario = revision["usuario"]?.ToString()
                            };
                            revisionesLista.Add(revisionCompra);
                            id++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar las revisiones: " + e.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return revisionesLista;
        }

        public bool actualizarEstatus(string idFirestore, String estatus)

        {
            SetResponse response = client.Set("Control/Compras/Encabezados/E" + idFirestore + "/estatus", estatus);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public bool actualizarEstatusVehiculos(string idFirestore, String estatus)
        {
            SetResponse response = client.Set("SolicitudesRep/Encabezados/E" + idFirestore + "/estatus", estatus);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public bool actualizarPagadoVehiculos(string folioSol, String flagPagado)
        {
            SetResponse response = client.Set("SolicitudesRep/Encabezados/E" + folioSol + "/flagPagado", flagPagado);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public bool actualizarObservacionesVehiculos(string folioSol, string observaciones)
        {
            SetResponse response = client.Set("SolicitudesRep/Encabezados/E" + folioSol + "/observaciones", observaciones);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public async Task EliminarOrdenFirebase(string folioSol)
        {
            try
            {
                await client.DeleteAsync($"SolicitudesRep/Encabezados/E{folioSol}");
                await client.DeleteAsync($"SolicitudesRep/Detalle/D{folioSol}");
                await client.DeleteAsync($"SolicitudesRep/DetalleMO/D{folioSol}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la orden {folioSol} en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<bool> EliminarDetalleOrdenFirebase(string folioSol, int id)
        {
            try
            {
                string path = $"SolicitudesRep/Detalle/D{folioSol}/{id}";
                FirebaseResponse response = await client.DeleteAsync(path);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error al eliminar detalle en Firebase: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar detalle en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> EliminarDetalleMOOrdenFirebase(string folioSol, int id)
        {
            try
            {
                string path = $"SolicitudesRep/DetalleMO/D{folioSol}/{id}";
                FirebaseResponse response = await client.DeleteAsync(path);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error al eliminar detalle mano de obra en Firebase: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar detalle mano de obra en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<bool> EliminarDetalleCAOrdenFirebase(string folioSol, int id)
        {
            try
            {
                string path = $"SolicitudesRep/DetalleCA/D{folioSol}/{id}";
                FirebaseResponse response = await client.DeleteAsync(path);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error al eliminar costo adicional en Firebase: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar costo adicional en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> ActualizarDetalleOrdenFirebase(DataDetalleCompraVehiculos detalle)
        {
            try
            {
                string path = $"SolicitudesRep/Detalle/D{detalle.folioSol}/{detalle.id}";
                FirebaseResponse response = await client.UpdateAsync(path, detalle);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar detalle en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> ActualizarDetalleMOOrdenFirebase(DataManoObra detalleMO)
        {
            try
            {
                string path = $"SolicitudesRep/DetalleMO/D{detalleMO.folioSol}/{detalleMO.id}";
                FirebaseResponse response = await client.UpdateAsync(path, detalleMO);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar detalle mano de obra en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<bool> ActualizarDetalleCAOrdenFirebase(DataCostosAdicionales detalleCA)
        {
            try
            {
                string path = $"SolicitudesRep/DetalleCA/D{detalleCA.folioSol}/{detalleCA.id}";
                FirebaseResponse response = await client.UpdateAsync(path, detalleCA);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los costos adicionales  en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public async Task<bool> InsertarDetalleOrdenFirebase(DataDetalleCompraVehiculos detalle)
        {
            try
            {
                string path = $"SolicitudesRep/Detalle/D{detalle.folioSol}/{detalle.id}";
                FirebaseResponse response = await client.SetAsync(path, detalle);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar detalle en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> InsertarDetalleMOOrdenFirebase(DataManoObra detalleMO)
        {
            try
            {
                string path = $"SolicitudesRep/DetalleMO/D{detalleMO.folioSol}/{detalleMO.id}";
                FirebaseResponse response = await client.SetAsync(path, detalleMO);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar detalle mano de obra en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<bool> InsertarDetalleCAOrdenFirebase(DataCostosAdicionales detalleCA)
        {
            try
            {
                string path = $"SolicitudesRep/DetalleCA/D{detalleCA.folioSol}/{detalleCA.id}";
                FirebaseResponse response = await client.SetAsync(path, detalleCA);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar costo adicional en Firebase: {ex.Message}", "Error en Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        #endregion

        #region CATEGORÍAS
        [FirestoreData]
        public class dataCategorias
        {
            [FirestoreProperty]
            public string id { get; set; }

            [FirestoreProperty]
            public string nombre { get; set; }

        }
        public async Task<bool> GuardarCategoriasEnFirestore(DataTable categorias)
        {

            bool todosLosRegistrosGuardados = true;
            FirestoreDb firestore = InicializarFirestore();
            if (firestore == null)
            {
                MessageBox.Show("No se pudo inicializar Firestore. Verifique la configuración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            foreach (DataRow categoria in categorias.Rows)
            {
                try
                {
                    var data = new dataCategorias
                    {
                        id = categoria["id"].ToString(),
                        nombre = categoria["nombre"].ToString(),
                    };

                    DocumentReference docRef = firestore.Collection("Categorias").Document(data.id.ToString());
                    await docRef.SetAsync(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar la categoría {categoria["id"]}: {ex.Message}");
                    todosLosRegistrosGuardados = false;
                }
            }

            return todosLosRegistrosGuardados;
        }


        public async Task<bool> AgregarCategoriaAFirestore(dataCategorias categoria)
        {
            try
            {
                FirestoreDb firestore = InicializarFirestore();
                if (firestore == null)
                {
                    throw new NullReferenceException("No se pudo inicializar Firestore. Verifique la configuración.");
                }

                DocumentReference docRef = firestore.Collection("Categorias").Document(categoria.id.ToString());
                await docRef.SetAsync(categoria);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar categoría en Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public async Task<bool> ActualizarCategoriaEnFirestore(dataCategorias categoria)
        {

            try
            {
                FirestoreDb firestore = InicializarFirestore();
                if (firestore == null)
                {
                    throw new NullReferenceException("No se pudo inicializar Firestore. Verifique la configuración.");
                }

                DocumentReference docRef = firestore.Collection("Categorias").Document(categoria.id.ToString());
                await docRef.SetAsync(categoria, SetOptions.MergeAll); 
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar categoría en Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<bool> EliminarCategoriaDeFirestore(int id)
        {
            try
            {
                FirestoreDb firestore = InicializarFirestore();
                if (firestore == null)
                {
                    throw new NullReferenceException("No se pudo inicializar Firestore. Verifique la configuración.");
                }

                DocumentReference docRef = firestore.Collection("Categorias").Document(id.ToString());
                await docRef.DeleteAsync(); 
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar categoría en Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        #endregion

        #region ALMACENES

        public class dataAlmacenes
        {
            public string id { get; set; }
            public string idFirebase { get; set; }
            public string nombre { get; set; }
            public string direccion { get; set; }
            public string activo { get; set; }
        }

        public async Task<List<dataAlmacenes>> GetAllAlmacenesAsync()
        {
            List<dataAlmacenes> lista = new List<dataAlmacenes>();

            try
            {
                FirebaseResponse response = await client.GetAsync("Catalogos/Almacenes");

                if (response.Body != "null")
                {
                    var almacenes = response.ResultAs<Dictionary<string, Dictionary<string, string>>>();

                    lista = almacenes.Select(item => new dataAlmacenes
                    {
                        id = item.Key, 
                        nombre = item.Value.ContainsKey("nombre") ? item.Value["nombre"] : null,
                        direccion = item.Value.ContainsKey("direccion") ? item.Value["direccion"] : null
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener almacenes desde Firebase: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lista;
        }
        public bool AgregarAlmacenAFirebase(dataAlmacenes almacen)
        {
            try
            {
                var almacenData = new
                {
                    nombre = almacen.nombre,
                    direccion = almacen.direccion
                };

                var response = client.Set($"Catalogos/Almacenes/{almacen.id}", almacenData);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Error al agregar el almacén a Firebase.", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar almacén a Firebase: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool ActualizarAlmacenEnFirebase(dataAlmacenes almacen)
        {
            try
            {
                if (string.IsNullOrEmpty(almacen.idFirebase))
                {
                    MessageBox.Show("El identificador de Firebase está vacío. No se puede actualizar el almacén.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var almacenData = new
                {
                    nombre = almacen.nombre,
                    direccion = almacen.direccion
                };

                FirebaseResponse response = client.Update($"Catalogos/Almacenes/{almacen.idFirebase}", almacenData);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true; 
                }
                else
                {
                    MessageBox.Show($"Error al actualizar el almacén en Firebase. Código de estado: {response.StatusCode}",
                                    "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar almacén en Firebase: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<bool> EliminarAlmacenDeFirebase(string idFirebase)
        {
            try
            {
                FirebaseResponse response = await client.DeleteAsync($"Catalogos/Almacenes/{idFirebase}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error al eliminar almacén en Firebase. Código de estado: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar almacén en Firebase: {ex.Message}");
                return false;
            }
        }



        #endregion

        #region PERSONAL

        public async Task<bool> eliminarPersonal(string numeroEmpleado)
        {
            if (string.IsNullOrEmpty(numeroEmpleado))
            {
                MessageBox.Show("El identificador del vehículo está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                FirebaseResponse response = await client.DeleteAsync($"Catalogos/Operativos/{numeroEmpleado}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"No se pudo eliminar al Personal Operativo. StatusCode: {response.StatusCode}", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar Personal en Firebase: {ex.Message}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> insertarPersonalFirebase(string nombre, string numEmpleado, string sucursal, string email, string puesto, string coordinador)
        {
            numEmpleado = Regex.Replace(numEmpleado ?? "", @"[^a-zA-Z0-9]", "").Trim();

            if (string.IsNullOrWhiteSpace(numEmpleado))
            {
                MessageBox.Show("El número de empleado no puede estar vacío o contener caracteres inválidos al actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                var empleado = new
                {
                    nombre,
                    numEmpleado,
                    sucursal,
                    email,
                    puesto,
                    coordinador
                };

                var response = await client.SetAsync($"Catalogos/Operativos/{numEmpleado}", empleado);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar empleado en Firebase: {ex.Message}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> actualizarPersonalFirebase(string nombre, string numEmpleado, string sucursal, string email, string puesto, string coordinador)
        {
            numEmpleado = Regex.Replace(numEmpleado ?? "", @"[^a-zA-Z0-9]", "").Trim();

            if (string.IsNullOrWhiteSpace(numEmpleado))
            {
                MessageBox.Show("El número de empleado no puede estar vacío o contener caracteres inválidos al actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                var empleado = new
                {
                    nombre,
                    numEmpleado,
                    sucursal,
                    email,
                    puesto, 
                    coordinador
                };

                FirebaseResponse response = await client.UpdateAsync($"Catalogos/Operativos/{numEmpleado}", empleado);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"No se pudo actualizar el empleado en Firebase. StatusCode: {response.StatusCode}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar empleado en Firebase: {ex.Message}", "Error de Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        #endregion

        #region PROVEEDORES
        [FirestoreData]
        public class dataProveedores
        {
            [FirestoreProperty]
            public int id { get; set; }

            [FirestoreProperty]
            public string rfcContribuyente { get; set; }

            [FirestoreProperty]
            public string razonSocialContribuyente { get; set; }

            [FirestoreProperty]
            public string domicilio { get; set; }

            [FirestoreProperty]
            public string cpContribuyente { get; set; }

            [FirestoreProperty]
            public string colonia { get; set; }

            [FirestoreProperty]
            public string telefono { get; set; }

            [FirestoreProperty]
            public string correo { get; set; }

            [FirestoreProperty]
            public string estado { get; set; }

            [FirestoreProperty]
            public string ciudad { get; set; }

            [FirestoreProperty]
            public string numeroCuentaExterna { get; set; }

            [FirestoreProperty]
            public int idBancoCuentaExterna { get; set; }
            [FirestoreProperty]
            public string numeroCuentaExterna2 { get; set; }

            [FirestoreProperty]
            public int idBancoCuentaExterna2 { get; set; }
            [FirestoreProperty]
            public string numeroCuentaExterna3 { get; set; }

            [FirestoreProperty]
            public int idBancoCuentaExterna3 { get; set; }

            [FirestoreProperty]
            public string activo { get; set; }

            [FirestoreProperty]
            public string fiscal { get; set; }

            [FirestoreProperty]
            public string clave { get; set; }
            [FirestoreProperty]
            public string giro { get; set; }
            [FirestoreProperty]
            public string regimen { get; set; }
        }
        private FirestoreDb InicializarFirestore()
        {
            try
            {
                string path = @"\\1.1.4.23\barcode\supermoreh.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                return FirestoreDb.Create("supermoreh-399bc");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task<bool> GuardarProveedoresEnFirestore(DataTable proveedores)
        {
            bool todosLosRegistrosGuardados = true;

            FirestoreDb firestore = InicializarFirestore();
            if (firestore == null)
            {
                MessageBox.Show("No se pudo inicializar Firestore. Verifique la configuración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (DataRow proveedor in proveedores.Rows)
            {
                try
                {
                    string fiscal = proveedor["fiscal"] == DBNull.Value || string.IsNullOrEmpty(proveedor["fiscal"].ToString())
                        ? "0"
                        : proveedor["fiscal"].ToString() == "True" ? "1" : "0";

                    var data = new Dictionary<string, object>
            {
                { "id", proveedor["id"].ToString() },
                { "rfcContribuyente", proveedor["rfcContribuyente"].ToString() },
                { "razonSocialContribuyente", proveedor["razonSocialContribuyente"].ToString() },
                { "domicilio", proveedor["domicilio"].ToString() },
                { "cpContribuyente", proveedor["cpContribuyente"].ToString() },
                { "colonia", proveedor["colonia"].ToString() },
                { "telefono", proveedor["telefono"].ToString() },
                { "correo", proveedor["correo"].ToString() },
                { "estado", proveedor["estado"].ToString() },
                { "ciudad", proveedor["ciudad"].ToString() },
                { "fiscal", fiscal },
                { "clave", proveedor["clave"].ToString() },
                { "giro", proveedor["giro"].ToString() }
            };

                    DocumentReference docRef = firestore.Collection("Proveedores").Document(proveedor["id"].ToString());
                    await docRef.SetAsync(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el proveedor {proveedor["id"]}: {ex.Message}");
                    todosLosRegistrosGuardados = false;
                }
            }

            return todosLosRegistrosGuardados;
        }


        public async Task<bool> AgregarProveedorAFirestore(dataProveedores proveedor)
        {
            try
            {
                FirestoreDb firestore = InicializarFirestore();
                if (firestore == null)
                {
                    throw new NullReferenceException("No se pudo inicializar Firestore. Verifique la configuración.");
                }

                DocumentReference docRef = firestore.Collection("Proveedores").Document(proveedor.id.ToString());
                await docRef.SetAsync(proveedor);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar proveedor en Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> ActualizarProveedorEnFirestore(dataProveedores proveedor)
        {
            try
            {
                FirestoreDb firestore = InicializarFirestore();
                if (firestore == null)
                {
                    throw new NullReferenceException("No se pudo inicializar Firestore. Verifique la configuración.");
                }

                DocumentReference docRef = firestore.Collection("Proveedores").Document(proveedor.id.ToString());
                await docRef.SetAsync(proveedor, SetOptions.MergeAll);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar proveedor en Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> EliminarProveedorFirestore(int id)
        {
            try
            {
                FirestoreDb firestore = InicializarFirestore();
                if (firestore == null)
                {
                    throw new NullReferenceException("No se pudo inicializar Firestore. Verifique la configuración.");
                }

                DocumentReference docRef = firestore.Collection("Proveedores").Document(id.ToString());
                await docRef.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar proveedor en Firestore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        #endregion

        #region MÁS METODOS
        public bool consultarServicio()
        {
            bool servicioActivo = false;

            try
            {
                IFirebaseClient _cliente;
                IFirebaseConfig _configuracion = new FirebaseConfig
                {
                    AuthSecret = "41RCUA4CqBZopd77xOtRHYMGex4cTZShyEJukLh3",
                    BasePath = "https://sammy-84146.firebaseio.com/"
                };
                _cliente = new FireSharp.FirebaseClient(_configuracion);

                FirebaseResponse response = _cliente.Get("Canales/9901");
                dataServicio servicio = response.ResultAs<dataServicio>();

                if (servicio.valor.Equals(1))
                    servicioActivo = true;
                else

                    ///CAMBIAR A FALSE 
                    servicioActivo = false;
            }
            catch
            {
                ///CAMBIAR A FALSE 
                servicioActivo = true;
            }

            return servicioActivo;
        }

        public void insertSolicitud(DataSolicitudes sol, int ruta, string folio)
        {
            SetResponse response = client.Set("Verificaciones/Listado/Ruta" + ruta + "/" + folio, sol);

            //    Verificaciones/Listado/Ruta6/5"
        }

        public void deleteSolicitud(int ruta, string folio)
        {
            FirebaseResponse response = client.Delete("Verificaciones/Listado/Ruta" + ruta + "/" + folio);
        }

        public void insertSucursal(DataSucursal sol, int id)
        {
            SetResponse response = client.Set("Sucursales/" + id, sol);
        }

        public void deletesucursal(int id)
        {
            FirebaseResponse response = client.Delete("Sucursales/" + id);
        }

        public bool insertFoliosRecibos(string sucursal, string tipo, DataFolios sol)
        {
            SetResponse response = client.Set("FoliosRecibos/" + sucursal + "/" + tipo, sol);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public bool insertFoliosRecibosServiciosAlt(DataFoliosSA sol)
        {
            SetResponse response = client.Set("FoliosRecibos/ServiciosAlternos/", sol);
            if (response.StatusCode.ToString().Equals("OK")) return true;
            else return false;
        }

        public void deleteFoliosRecibos(string sucursal, string tipo, string folio)
        {
            FirebaseResponse response = client.Delete("Recibos/" + sucursal + "/" + tipo + "/" + folio);
        }

        public DataTable consultarRecibos()
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
            //Dictionary<string, Dictionary<string, Dictionary<string, DataRecibosCaja>>> list = response.ResultAs<Dictionary<string, Dictionary<string, Dictionary<string, DataRecibosCaja>>>>();
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

        public DataTable consultarFolios(string sucursal, string tipoRecibo)
        {
            FirebaseResponse response;
            DataTable listado = new DataTable();
            listado.Columns.Add("LimInf1");
            listado.Columns.Add("LimInf2");
            listado.Columns.Add("LimSup1");
            listado.Columns.Add("LimSup2");
            listado.Columns.Add("UltFolio");

            // Obtengo la rama correspondiente a la ruta seleccionada
            if (tipoRecibo != "")
            {
                response = client.Get("FoliosRecibos/" + sucursal + "/" + tipoRecibo + "/");
            }
            else
            {
                response = client.Get("FoliosRecibos/" + sucursal);
            }

            DataFolios result = response.ResultAs<DataFolios>();


            if (result != null)
            {
                DataRow dr = listado.NewRow();

                if (!string.IsNullOrEmpty(result.LimInf1)) dr["LimInf1"] = result.LimInf1;
                else dr["LimInf1"] = "0";

                if (!string.IsNullOrEmpty(result.LimInf2)) dr["LimInf2"] = result.LimInf2;
                else dr["LimInf2"] = "0";

                if (!string.IsNullOrEmpty(result.LimSup1)) dr["LimSup1"] = result.LimSup1;
                else dr["LimSup1"] = "0";

                if (!string.IsNullOrEmpty(result.LimSup2)) dr["LimSup2"] = result.LimSup2;
                else dr["LimSup2"] = "0";

                if (!string.IsNullOrEmpty(result.UltFolio)) dr["UltFolio"] = result.UltFolio;
                else dr["UltFolio"] = "0";

                listado.Rows.Add(dr);
                return listado;
            }
            else
            {
                listado = new DataTable();
                return listado;
            }
        }
        #endregion

        #region CLASES MODELO
        public class dataVehiculos
        {
            public string anio { get; set; }
            public string color { get; set; }
            public string foto { get; set; }
            public string marca { get; set; }
            public string modelo { get; set; }
            public string placas { get; set; }
            public string serie { get; set; }
            public string sucursal { get; set; }
        }

        internal class dataServicio
        {
            public int valor { get; set; }
        }

        public class DataSolicitudes
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
        }

        public class DataSucursal
        {
            public string nombre { get; set; }
        }

        public class DataFoliosSA
        {
            public string UltFolio { get; set; }
        }

        public class DataFolios
        {
            public string LimInf1 { get; set; }
            public string LimInf2 { get; set; }
            public string LimSup1 { get; set; }
            public string LimSup2 { get; set; }
            public string UltFolio { get; set; }
        }

        public class DataRecibosCaja
        {
            public string bancoM1 { get; set; }
            public string bancoM2 { get; set; }
            public string cliente { get; set; }
            public string concepto { get; set; }
            public string contrato { get; set; }

            public string ctaDestinoM1 { get; set; }
            public string ctaDestinoM2 { get; set; }
            public string descrip { get; set; }
            public string fecha { get; set; }
            public string folio { get; set; }

            public string metodo1 { get; set; }
            public string metodo2 { get; set; }
            public string montoM1 { get; set; }
            public string montoM2 { get; set; }
            public string montoTotal { get; set; }
            public string nombreTM1 { get; set; }
            public string nombreTM2 { get; set; }
            public string nota { get; set; }
            public string recibio { get; set; }
            public string sucursal { get; set; }
            public string terminacionM1 { get; set; }
            public string terminacionM2 { get; set; }
            public string tipoPago { get; set; }

            public string numContSol { get; set; }
            public string monto { get; set; }
            public string flagContrato { get; set; }

        }
        #endregion
    }
}

