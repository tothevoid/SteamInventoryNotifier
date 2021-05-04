using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier.Model
{
    public class InventoryResponse
    {
        [JsonProperty("descriptions")]
        public IEnumerable<ItemDescription> Descriptions { get; set; }

        [JsonProperty("total_inventory_count")]
        public int Total { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
