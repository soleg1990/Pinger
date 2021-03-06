﻿using Microsoft.Extensions.Hosting;
using Pinger.DAL;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Pinger.BL
{
    public class BackgroundWork : IBackgroundWork, IHostedService
    {
        private IBackgroundRepository repository;
        private IHostPinger pinger;

        public BackgroundWork(IBackgroundRepository repository, IHostPinger pinger)
        {
            this.repository = repository;
            this.pinger = pinger;
        }

        public async Task StartAsync(CancellationToken token)
        {
            await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Parallel.ForEach(repository.GetSites(), site =>
                    {
                        if (site.LastPing.AddSeconds(site.PingFrequency) < DateTime.Now)
                        {
                            var isAvailable = pinger.Ping(site.Host);
                            repository.UpdateSiteLastPing(site.Id, isAvailable, DateTime.Now);
                        }
                    });
                    Thread.Sleep(100);
                }
            }, token);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
