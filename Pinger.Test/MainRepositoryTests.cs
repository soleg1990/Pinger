using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinger.DAL;
using Pinger.DAL.DTOs;
using Pinger.DAL.EF;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinger.Test
{
    [TestClass]
    public class MainRepositoryTests
    {
        private MockRepository mockRepository;

        private Mock<IDbRepository> mockDbRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);

            mockDbRepository = mockRepository.Create<IDbRepository>();
            mockDbRepository.Setup(r => r.GetSitesAsync())
                .Returns(Task.FromResult<ICollection<DbSiteOptions>>(new List<DbSiteOptions> { new DbSiteOptions { Id = Guid.NewGuid(), Host = "test", PingFrequency = 15 } }));
            mockDbRepository.Setup(r => r.CreateAsync(It.IsAny<DbSiteOptions>()))
                .Returns(Task.FromResult<DbSiteOptions>(new DbSiteOptions { Id = Guid.NewGuid(), Host = "test2", PingFrequency = 20 }));
            mockDbRepository.Setup(r => r.UpdateAsync(It.IsAny<DbSiteOptions>()))
                .Returns(Task.FromResult(true));
            mockDbRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //this.mockRepository.VerifyAll();
        }

        private MainRepository CreateMainRepository()
        {
            return new MainRepository(
                mockDbRepository.Object);
        }

        [TestMethod]
        public async Task CreateAsync_SomeSite_AddedToTheCache()
        {
            var repo = CreateMainRepository();

            SiteOptionsDto site = new SiteOptionsDto { Host = "test2", PingFrequency = 20 };

            var result = await repo.CreateAsync(site);
            SiteOptions cacheResult = null;
            foreach (var s in repo.GetSites())
            {
                if (s.Host == "test2")
                {
                    cacheResult = s;
                    break;
                }
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == cacheResult.Id);
        }

        [TestMethod]
        public async Task UpdateAsync_SomeSite_CacheUpdated()
        {
            var repo = CreateMainRepository();

            var site = repo.GetSites().First();
            var dto = new SiteOptionsDto { Id = site.Id, Host = "test2.com", PingFrequency = 25 };
            await repo.UpdateAsync(dto);
            var updatedSite = repo.GetSites().First();

            Assert.IsTrue(site.Id == updatedSite.Id && updatedSite.Host == dto.Host && updatedSite.PingFrequency == dto.PingFrequency);
        }

        [TestMethod]
        public async Task DeleteAsync_SomeSite_DeletedFromCache()
        {
            var repo = this.CreateMainRepository();

            var site = repo.GetSites().First();
            await repo.DeleteAsync(site.Id);
            var deletedSite = repo.GetSites().Where(s => s.Id == site.Id).FirstOrDefault();

            Assert.IsNull(deletedSite);
        }
    }
}
