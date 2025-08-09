using PingViewerApp.Bussines.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PingViewerApp.Bussines.Services
{
    public interface  IFileManager
    {
        PingDatabase? ExtractPings();
        void UpdatePings(PingDatabase? pings);
    }

    public class FileManager : IFileManager
    {
        public PingDatabase? ExtractPings()
        {
            var json = File.ReadAllText("db.json");
            return JsonSerializer.Deserialize<PingDatabase>(json);
        }

        public void UpdatePings(PingDatabase? pings)
        {
            var updatedJson = JsonSerializer.Serialize(pings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("db.json", updatedJson);
        }

    }
}
