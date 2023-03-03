using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Ping
{
    public class NetworkService
    {
        private readonly IDNS _DNS;

        public NetworkService(IDNS DNS)
        {
            _DNS = DNS;
        }

        public string SendPing()
        {
            var dnsSuccess = _DNS.SendDNS();

            if (!dnsSuccess)
                return "Failed: Ping not Sent!";

            return "Success: Ping Sent!";
        }

        public int PingTimeout(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public PingOptions GetPingOptions()
        {
            return new PingOptions()
            {
                DontFragment = true,
                Ttl = 1,
            };
        }

        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pings = new[]
            {
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1,
                },

                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1,
                },

                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1,
                }
            };

            return pings;
        }
    }
}
