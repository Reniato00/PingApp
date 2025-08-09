using AutoFixture;
using Moq;
using PingViewerApp;
using PingViewerApp.Bussines.Entities;
using PingViewerApp.Bussines.Services;
using Xunit;

namespace Testing.Bussines
{
    [TestClass]
    public class PingMonitorTests
    {
        private PingMonitor pingMonitor;
        private readonly Mock<IPingRequester> pingRequester = new();
        private readonly Mock<IFileManager> fileManager = new();

        private readonly Fixture fixture = new();
        public PingMonitorTests()
        {
            pingMonitor = new PingMonitor(pingRequester.Object, fileManager.Object);
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
    }
}
