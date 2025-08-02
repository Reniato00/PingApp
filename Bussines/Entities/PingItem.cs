using System.Collections.Generic;

namespace PingViewerApp.Bussines.Entities
{
    public class PingItem
    {
        public string Name { get; set; }
        public string Host { get; set; }
    }

    public class  PingDatabase
    {
        public List<PingItem> Pings { get; set; }
    }
}
