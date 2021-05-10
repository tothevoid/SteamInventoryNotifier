using SteamInventoryNotifier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamInventoryNotifier.ViewModel
{ 
    public class MainViewModel
    {
        #region private API
        private readonly INotifier _notifier = new TelegramNotifier();
        private readonly SteamInventoryLoader _loader = new SteamInventoryLoader();
        #endregion

        public GrabbingModel GrabbingModel { get; set; } = new GrabbingModel();

        public ICommand SubmitParametersCommand => new BaseCommand(OnSubmitParametersClick);

        public MainViewModel()
        {
            var timer = new System.Timers.Timer(30000)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += OnTimerTick;
        }

        private void SendRequest()
        {
            var inventory = _loader.GetInventory(GrabbingModel.ProfileId, "730");
            if (inventory.Total != GrabbingModel.LastFetchItemsQuantity && !string.IsNullOrEmpty(GrabbingModel.ProfileId))
            {
                SendNotification(inventory);
            }
            GrabbingModel.UpdateDisplayedDate(DateTime.Now);
            GrabbingModel.LastFetchItemsQuantity = inventory.Total;
        }

        private void SendNotification(InventoryResponse inventoryResponse)
        {
            var newestItem = inventoryResponse.Descriptions.FirstOrDefault();
            if (newestItem != null)
            {
                var message = new NotificationMessage(newestItem.Name,
                    newestItem.MarketHashName,
                    newestItem.IconHash,
                    DateTime.Now);
                _notifier.Notify(message);
            }
        }

        #region events implementation
        private void OnTimerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendRequest();
        }

        private void OnSubmitParametersClick(object eventData)
        {
            if (!string.IsNullOrEmpty(GrabbingModel.ProfileId))
            {
                SendRequest();
            }
        }
        #endregion
    }
}
