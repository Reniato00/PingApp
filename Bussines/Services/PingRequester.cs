﻿using PingViewerApp.Bussines.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PingViewerApp.Bussines.Services
{
    public class PingRequester : IPingRequester
    {
        public async Task PingEachAsync(List<PingItem> pingItems, Action<PingResult> onPingCompleted)
        {
            var tasks = pingItems.Select(async item =>
            {
                PingResult result;
                using var pingSender = new Ping();
                try
                {
                    PingReply reply = await pingSender.SendPingAsync(item.Host).ConfigureAwait(false);

                    result = new PingResult
                    {
                        Name = item.Name,
                        Host = item.Host,
                        Status = reply.Status == IPStatus.Success ? "Success" : "Failed",
                        TimeMs = reply.Status == IPStatus.Success ? (int?)reply.RoundtripTime : null
                    };
                }
                catch
                {
                    result = new PingResult
                    {
                        Name = item.Name,
                        Host = item.Host,
                        Status = "Error",
                        TimeMs = 0
                    };
                }

                onPingCompleted(result);
            });

            await Task.WhenAll(tasks);
        }

        public async Task<PingResult> PingAsync(PingItem item)
        {
            using var pingSender = new Ping();
            try
            {
                PingReply reply = await pingSender.SendPingAsync(item.Host);
                return new PingResult
                {
                    Name = item.Name,
                    Host = item.Host,
                    TimeMs = (int)reply.RoundtripTime,
                    Status = reply.Status.ToString()
                };
            }
            catch (Exception ex)
            {
                return new PingResult
                {
                    Name = item.Name,
                    Host = item.Host,
                    TimeMs = -1,
                    Status = $"Error: {ex.Message}"
                };
            }
        }


    }

    public interface IPingRequester
    {
        Task PingEachAsync(List<PingItem> pingItems, Action<PingResult> onPingCompleted);
        Task<PingResult> PingAsync(PingItem item);
    }
}
