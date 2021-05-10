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
        public GrabbingParametersModel GrabbingModel { get; set; } = new GrabbingParametersModel();

        public GrabbingResultModel GrabbingResultModel { get; set; } = new GrabbingResultModel();

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
            InventoryRequestTimer.Elapsed += (object sender, ElapsedEventArgs e) => SendRequest(false);

            SendRequest(false);
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

        private void SendRequest(bool notifyValidation = true)
        {
            var validationResult = GrabbingModel.Validate();
            if (!string.IsNullOrEmpty(validationResult))
            {
                if (notifyValidation)
                {
                    MessageBox.Show(validationResult);
                }
                return;
            }            

            var inventory = _loader.GetInventory(GrabbingModel.ProfileId, GrabbingModel.AppId);
            if (inventory == null)
            {
                GrabbingResultModel.WriteInOutput("Failed to request inventory");
                return;
            } 
            else
            {
                GrabbingResultModel.WriteInOutput($"Successfully fetched {inventory.Total} items");
            }

            if (inventory.Total != GrabbingResultModel.LastFetchItemsQuantity)
            {
                SendNotification(inventory);
            }
            GrabbingResultModel.LastFetchItemsQuantity = inventory.Total;
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
                var isSuccessful = string.IsNullOrEmpty(notificationError);
                if (!isSuccessful)
                {
                    MessageBox.Show(notificationError);
                }
                return isSuccessful;
            }
            return false;
        }
    }
}
