using Newtonsoft.Json;
using SteamInventoryNotifier.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier
{
    public class SteamInventoryLoader
    {
        private readonly string _apiUrl = "https://steamcommunity.com/inventory";
        
        public InventoryResponse GetInventory(string userId, string appId)
        {
            var url = $"{_apiUrl}/{userId}/{appId}/2";

            var requester = new Requester();
            var response = requester.SendDefaultRequest(url);

            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<InventoryResponse>(response);
            }
            else
            {
                Console.WriteLine("An error occured while inventory fetch");
                return null;
            }
        }
    }
}
