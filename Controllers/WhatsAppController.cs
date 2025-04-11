using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using cap_Comunes;
using System;
using Microsoft.AspNetCore.Http;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Chatbot_WhatsApp_Cobranza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly ccFirebase20 _firebase;

        public WhatsAppController(ccFirebase20 firebase)
        {
            _firebase = firebase; // Inyecta ccFirebase20
        }
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("Bot en línea");


        [HttpPost]
        [HttpPost]
        public IActionResult ReceiveMessage([FromForm] string From, [FromForm] string Body)
        {
            Console.WriteLine($"MENSAJE RECIBIDO DE {From}: {Body}");

            var messagingResponse = new MessagingResponse();

            // Estado de la conversación (usamos el número del usuario como clave de sesión)
            var sessionKey = From;
            if (!HttpContext.Session.TryGetValue(sessionKey, out var sessionData))
            {
                HttpContext.Session.SetString(sessionKey, "step1");
            }

            var step = HttpContext.Session.GetString(sessionKey);

            switch (step)
            {
                case "step1":
                    messagingResponse.Message("¡Hola! Indica tu número de ruta:");
                    HttpContext.Session.SetString(sessionKey, "step2");
                    break;

                case "step2":
                    HttpContext.Session.SetString("ruta", Body);
                    messagingResponse.Message("Si necesitas permiso para reimpresión, responde 1. Si necesitas permiso para cancelación, responde 2:");
                    HttpContext.Session.SetString(sessionKey, "step3");
                    break;

                case "step3":
                    if (Body != "1" && Body != "2")
                    {
                        messagingResponse.Message("Opción no válida. Responde 1 para reimpresión o 2 para cancelación.");
                        return Content(messagingResponse.ToString(), "application/xml");
                    }

                    string accion = (Body == "1") ? "reimpresión" : "cancelación";
                    HttpContext.Session.SetString("accion", accion);
                    messagingResponse.Message("Indica el motivo:");
                    HttpContext.Session.SetString(sessionKey, "step4");
                    break;

                case "step4":
                    var ruta = HttpContext.Session.GetString("ruta");
                    var accionFinal = HttpContext.Session.GetString("accion");
                    var motivo = Body;

                    bool success = GuardarSolicitudFirebase(ruta, accionFinal, motivo);
                    if (success)
                    {
                        messagingResponse.Message("Permiso otorgado. ¡Hasta luego!");
                    }
                    else
                    {
                        messagingResponse.Message("Hubo un error al guardar el permiso.");
                    }

                    HttpContext.Session.Remove(sessionKey);
                    break;
            }

            return Content(messagingResponse.ToString(), "application/xml");
        }


        private bool GuardarSolicitudFirebase(string ruta, string accion, string motivo)
        {
            try
            {
                var solicitud = new
                {
                    ruta,
                    accion,
                    motivo,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    permisoOtorgado = true
                };

                // Usa ccFirebase20 para guardar la solicitud en Firebase
                var response = _firebase.client.SetAsync($"Permisos/Rutas/{ruta}", solicitud).Result;
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la solicitud en Firebase: {ex.Message}");
                return false;
            }
        }
    }
}