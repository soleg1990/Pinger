using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Pinger.BL
{
    public class HostPinger : IHostPinger
    {
        public bool Ping(string host)
        {
            var pingSender = new Ping();
            try
            {
                var reply = pingSender.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                return false;
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}
