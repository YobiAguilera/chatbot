using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Chatbot_WhatsApp_Cobranza
{
    public class configurationService
    {
        private readonly IConfiguration _configuration;

        public configurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetTwilioAccountSid()
        {
            return _configuration["Twilio:AccountSid"];
        }

        public string GetTwilioAuthToken()
        {
            return _configuration["Twilio:AuthToken"];
        }
    }
}
