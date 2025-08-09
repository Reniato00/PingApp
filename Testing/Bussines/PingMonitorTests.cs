using AutoFixture;
using Moq;
using PingViewerApp;
using PingViewerApp.Bussines.Entities;
using PingViewerApp.Bussines.Services;
using PingViewerApp.Utils.Factories;
using Xunit;

namespace Testing.Bussines
{
    [TestClass]
    public class PingMonitorTests
    {
        private PingMonitor pingMonitor;
        private readonly Mock<IPingRequester> pingRequester = new();
        private readonly Mock<IFileManager> fileManager = new();
        private readonly Mock<IItemFactory> itemFactory = new();

        private readonly Fixture fixture = new();
        public PingMonitorTests()
        {
            pingMonitor = new PingMonitor(pingRequester.Object, fileManager.Object,itemFactory.Object);
        }

        [TestMethod]
        public async Task PingAllAsync_Test() 
        {
            var actionMock = new Mock<Action<PingResult>>();
            await pingMonitor.PingAllAsync(actionMock.Object);
            pingRequester.Verify(pr => pr.PingEachAsync(It.IsAny<List<PingItem>>(), actionMock.Object), Times.Once);
        }

        [TestMethod]
        public async Task AddNewHost_Test()
        {
            var item = fixture.Create<PingItem>();
            var pingDatabase = fixture.Create<PingDatabase>();
            fileManager.Setup(x => x.ExtractPings()).Returns(pingDatabase);
            pingMonitor.AddNewHost(item);
            fileManager.Verify(x => x.ExtractPings(), Times.AtLeastOnce);
            fileManager.Verify(x => x.UpdatePings(It.IsAny<PingDatabase?>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void DeleteHost_Test()
        {
            var item = fixture.Create<PingItem>();
            var pingDatabase = fixture.Create<PingDatabase>();
            pingDatabase.Pings.Add(item);
            fileManager.Setup(x => x.ExtractPings()).Returns(pingDatabase);
            pingMonitor.DeleteHost(item);
            fileManager.Verify(x => x.ExtractPings(), Times.AtLeastOnce);
            fileManager.Verify(x => x.UpdatePings(It.IsAny<PingDatabase?>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task RepingOneAsync_Test()
        {
            pingRequester.Setup(pr => pr.PingAsync(It.IsAny<PingItem>())).ReturnsAsync(fixture.Create<PingResult>());
            var result = await pingMonitor.RepingOneAsync(fixture.Create<PingItem>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<PingResult>(result);
        }

        [TestMethod]
        public async Task RepingAllAsync_Test()
        {
            var actionMock = new Mock<Action<PingResult>>();
            var pingDatabase = fixture.Create<PingDatabase>();
            pingDatabase.Pings.Add(fixture.Create<PingItem>());

            fileManager.Setup(x => x.ExtractPings()).Returns(pingDatabase);
            itemFactory.Setup(x => x.ExtractListItems(pingDatabase)).Returns(pingDatabase.Pings);
            pingRequester.Setup(pr => pr.PingEachAsync(It.IsAny<List<PingItem>>(), actionMock.Object))
                         .Returns(Task.CompletedTask);
            await pingMonitor.RepingAllAsync(new List<PingResult>(), actionMock.Object);
            pingRequester.Verify(pr => pr.PingEachAsync(It.IsAny<List<PingItem>>(), actionMock.Object), Times.Never);
        }
    }
}
