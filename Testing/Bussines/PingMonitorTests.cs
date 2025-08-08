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
        public PingMonitorTests()
        {
            pingMonitor = new PingMonitor(pingRequester.Object);
        }

        [TestMethod]
        public async Task PingAllAsync_Test() 
        {
            var actionMock = new Mock<Action<PingResult>>();
            await pingMonitor.PingAllAsync(actionMock.Object);
            pingRequester.Verify(pr => pr.PingEachAsync(It.IsAny<List<PingItem>>(), actionMock.Object), Times.Once);
        }
    }
}
