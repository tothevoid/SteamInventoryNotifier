using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier.Model
{
    public class ItemDescription
    {
        [JsonProperty("market_name")]
        public string Name { get; set; }

        [JsonProperty("icon_url")]
        public string IconHash { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }
    }
}
