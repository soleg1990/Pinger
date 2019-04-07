using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinger.BL;
using Pinger.DAL;
using Pinger.DAL.DTOs;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Pinger.Test
{
    [TestClass]
    public class BackgroundWorkTests
    {
        private MainRepository CreateRepository()
        {
            return new MainRepository(new FakeDbRepository());
        }

        private BackgroundWork CreateBackgroundWork(IBackgroundRepository repo)
        {
            return new BackgroundWork(repo, new HostPinger());
        }

        [TestMethod]
        public void WorkPing_IfLastPingPlusFrequencyLessCurrTimeThenPingElseNotPing()
        {
            var repo = CreateRepository();
            var bgWork = CreateBackgroundWork(repo);
            var yandex = repo.GetSites().Where(s => s.Host == "yandex.ru").First();
            repo.UpdateSiteLastPing(yandex.Id, false, DateTime.Now.AddDays(1));

            CancellationTokenSource tokenSorc = new CancellationTokenSource();
            var token = tokenSorc.Token;
            bgWork.StartAsync(token);
            Thread.Sleep(300);
            tokenSorc.Cancel();

            var yandexAfterWork = repo.GetSites().Where(s => s.Id == yandex.Id).First();
            var googleAfterWork = repo.GetSites().Where(s => s.Host == "google.com").First();

            Assert.IsTrue(googleAfterWork.IsAvailable);
            Assert.IsTrue(googleAfterWork.LastPing > DateTime.Now.AddMinutes(-1));
            Assert.IsFalse(yandexAfterWork.IsAvailable);
            Assert.IsTrue(yandexAfterWork.LastPing > DateTime.Now);
        }
    }
}
