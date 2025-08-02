using PingViewerApp.Bussines.Entities;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PingViewerApp.Bussines.Services
{
    public class PingRequester
    {
        public async Task<List<PingResult>> GetPingsHealth(List<PingItem> pingItems)
        {
            Ping pingSender = new Ping();
            List<PingResult> pingResults = new List<PingResult>();
            foreach (var item in pingItems)
            {
                try
                {
                    PingReply reply = await pingSender.SendPingAsync(item.Host).ConfigureAwait(false);
                    if (reply.Status == IPStatus.Success)
                    {
                        pingResults.Add(new PingResult
                        {
                            Name = item.Name,
                            Host = item.Host,
                            Status = "Success",
                            TimeMs = (int)reply.RoundtripTime
                        });
                    }
                    else
                    {
                        pingResults.Add(new PingResult
                        {
                            Name = item.Name,
                            Host = item.Host,
                            Status = "Failed",
                            TimeMs = null
                        });
                    }
                }
                catch
                {
                    pingResults.Add(new PingResult
                    {
                        Name = item.Name,
                        Host = item.Host,
                        Status = "Error",
                        TimeMs = null
                    });
                    continue;
                }
            }
            return pingResults;
        }
    }
}
