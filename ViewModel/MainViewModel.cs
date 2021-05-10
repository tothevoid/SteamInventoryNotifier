using SteamInventoryNotifier.Interfaces;
using SteamInventoryNotifier.Model;
using SteamInventoryNotifier.Notifiers;
using System;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace SteamInventoryNotifier.ViewModel
{
    public class MainViewModel
    {
        #region private API
        private readonly INotifier _notifier = new TelegramNotifier();
        private readonly SteamInventoryGrabber _loader = new SteamInventoryGrabber();
        #endregion

        #region UI
        public GrabbingModel GrabbingModel { get; set; } = new GrabbingModel();

        public ICommand SubmitParametersCommand => new BaseCommand((object data) => SendRequest());
        #endregion

        private readonly Timer InventoryRequestTimer;

        public MainViewModel()
        {
            GrabbingModel.OnFrequencyChanged += OnFrequencyChanged;

            InventoryRequestTimer = new Timer(GrabbingModel.Frequency * 1000)
            {
                AutoReset = true,
                Enabled = true
            };
            InventoryRequestTimer.Elapsed += (object sender, ElapsedEventArgs e) => SendRequest();
        }

        private void OnFrequencyChanged(int frequency)
        {
            if (InventoryRequestTimer.Enabled)
            {
                InventoryRequestTimer.Stop();
            }
            InventoryRequestTimer.Interval = frequency * 1000;
            InventoryRequestTimer.Start();
        }

        private void SendRequest()
        {
            if (GrabbingModel.ProfileId != default)
            {
                //TODO: validation
                return;
            }

            var inventory = _loader.GetInventory(GrabbingModel.ProfileId, GrabbingModel.AppId);
            if (inventory.Total != GrabbingModel.LastFetchItemsQuantity && GrabbingModel.ProfileId != default)
            {
                SendNotification(inventory);
            }
            GrabbingModel.UpdateDisplayedDate(DateTime.Now);
            GrabbingModel.LastFetchItemsQuantity = inventory.Total;
        }

        private bool SendNotification(InventoryResponse inventoryResponse)
        {
            var newestItem = inventoryResponse.Descriptions.FirstOrDefault();
            if (newestItem != null)
            {
                var message = new NotificationMessage(newestItem.Name,
                    newestItem.MarketHashName,
                    newestItem.IconHash,
                    DateTime.Now);
                var notificationError = _notifier.Notify(message);
                var hasErrors = string.IsNullOrEmpty(notificationError);
                if (hasErrors)
                {
                    MessageBox.Show(notificationError);
                }
                return !hasErrors;
            }
            return true;
        }
    }
}
