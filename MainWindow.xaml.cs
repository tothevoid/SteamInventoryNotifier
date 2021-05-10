using SteamInventoryNotifier.ViewModel;
using System.Windows;

namespace SteamInventoryNotifier
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
