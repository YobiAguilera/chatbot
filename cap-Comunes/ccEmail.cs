using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

namespace cap_Comunes
{
    public class ccEmail
    {
        private string _from;
        private string _pass;
        private string _host;
        private int _port;
        private bool _enableSsl;

        public List<string> destinos { get; set; }
        public List<string> copias { get; set; }
        public List<string> archivos { get; set; }
        public string result { get; set; }

        public ccEmail()
        {
            this._from = "notificacionesmorehgdl@gmail.com";
            this._pass = "kdpxtogcsjmyvsxe";
            this._host = "smtp.gmail.com";
            this._port = 587;
            this._enableSsl = true;

            destinos = new List<string>();
            copias = new List<string>();
            archivos = new List<string>();
        }

        public void enviarCorreo(string mensaje, string asunto)
        {
            try
            {
                MailMessage email = new MailMessage
                {
                    From = new MailAddress(_from),
                    Subject = asunto,
                    Body = mensaje,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };

                // Agregar destinatarios
                foreach (string item in destinos)
                {
                    email.To.Add(new MailAddress(item));
                }

                // Agregar copias si hay
                foreach (string item in copias)
                {
                    email.CC.Add(new MailAddress(item));
                }

                // Adjuntar archivos
                foreach (string archivo in archivos)
                {
                    if (!string.IsNullOrEmpty(archivo) && System.IO.File.Exists(archivo))
                    {
                        email.Attachments.Add(new Attachment(archivo, MediaTypeNames.Application.Octet));
                    }
                }

                // Configuración SMTP
                using (SmtpClient smtp = new SmtpClient(_host, _port))
                {
                    smtp.Credentials = new NetworkCredential(_from, _pass);
                    smtp.EnableSsl = _enableSsl;
                    smtp.Send(email);
                }

                result = "Correo enviado exitosamente.";
                MessageBox.Show(result, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                result = "Error enviando correo: " + ex.Message;
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
