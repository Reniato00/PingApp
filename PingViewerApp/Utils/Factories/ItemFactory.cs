using PingViewerApp.Bussines.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PingViewerApp.Utils.Factories
{
    public interface IItemFactory
    {
        List<PingItem> ExtractListItems(PingDatabase? pings);
    }
    public class ItemFactory : IItemFactory
    {
        public List<PingItem> ExtractListItems(PingDatabase? pings)
        {
            return pings?.Pings
                .Select(x => new PingItem { Name = x.Name, Host = x.Host })
                .ToList() ?? new List<PingItem>();
        }
    }
}
