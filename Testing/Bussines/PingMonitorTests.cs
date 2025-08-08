using Moq;
using PingViewerApp.Bussines.Services;

namespace Testing.Bussines
{
    public class PingMonitorTests
    {
        private PingMonitor pingMonitor;
        private readonly Mock<IPingRequester> pingRequester = new();
        public PingMonitorTests()
        {
            pingMonitor = new PingMonitor(pingRequester.Object);
        }
    }
}
