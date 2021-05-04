using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier.Model
{
    public class NotificationMessage
    {
        public NotificationMessage(string itemName, string marketHash, string imageHash, DateTime fetchDate)
        {
            ItemName = itemName;
            this.imageHash = imageHash;
            this.marketHash = marketHash;
            FetchDate = fetchDate;
        }

        public string ItemName { get; set; }

        public DateTime FetchDate { get; set; }

        private string imageHash;
        public string ImageLink { get { return $"https://community.cloudflare.steamstatic.com/economy/image/{imageHash}"; } }

        private string marketHash;

        //TODO: replace 730 to game id
        public string MarketLink { get { return $"https://steamcommunity.com/market/listings/730/{marketHash}"; } }
    }
}
