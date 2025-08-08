using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PingViewerApp.Bussines.Entities
{
    public class PingItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("host")]
        public string Host { get; set; }
    }

    public class  PingDatabase
    {
        [JsonPropertyName("pings")]
        public List<PingItem> Pings { get; set; }
    }
}
