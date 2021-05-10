using Newtonsoft.Json;
using SteamInventoryNotifier.Http;
using SteamInventoryNotifier.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier
{
    public class SteamInventoryGrabber
    {
        private readonly string _apiUrl = "https://steamcommunity.com/inventory";
        
        public InventoryResponse GetInventory(long userId, int appId)
        {
            var url = $"{_apiUrl}/{userId}/{appId}/2";
            var response = new HttpRequestHelper().SendGet(url);

            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<InventoryResponse>(response);
            }
            else
            {
                //TOOD: log it
                Console.WriteLine("An error occured while inventory fetch");
                return null;
            }
        }
    }
}
