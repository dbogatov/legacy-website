using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MyWebsite.Services
{
    public interface IPushService
    {
        void SendAll(string message);
        void SendTo(IEnumerable<string> tokens);
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
    }


}