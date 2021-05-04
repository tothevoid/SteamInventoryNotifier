using SteamInventoryNotifier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SteamInventoryNotifier
{
    public partial class MainWindow : Window
    {
        private readonly INotifier _notifier;
        private readonly SteamInventoryLoader _loader;

        public MainWindow()
        {
            InitializeComponent();
            //TODO: call it async
            _notifier = new TelegramNotifier();
            _loader = new SteamInventoryLoader();
        }

        private void OnSubmitClick(object sender, RoutedEventArgs e)
        {
            var userId = steam_id.Text;
            if (!string.IsNullOrEmpty(userId))
            {
                var inventory = _loader.GetInventory(userId, "730");
                SendNotification(inventory);
            }
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
    }
}
