using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MyWebsite.Services
{
    public interface IPushService
    {
        void SendAll(string message);
        void SendTo(IEnumerable<string> tokens);

        void SendToTelegram(string message);
    }

    public class PushService : IPushService
    {
        public void SendAll(string message)
        {
            SendTo(new List<string> { "95ff4eaf1127720d2f27318522753fe54430cc54e5d9a8ff611263ced6977135" });
        }

        public void SendTo(IEnumerable<string> tokens)
        {
			
        }

		public void SendToTelegram(string message) 
		{
			using (var client = new HttpClient())
			{
				var values = new Dictionary<string, string>
				{
					{ "token", "217717405:AAFg9Y0IRxP2s1_u0XHTGhCs53Q1e8RPOws" },
					{ "message", message },
					{ "chatid", "@dmytrochan" }
				};

				var content = new FormUrlEncodedContent(values);
				
				client.PostAsync("https://push.dbogatov.org/api/push/telegram", content).Wait();
			}
		}
    }
}