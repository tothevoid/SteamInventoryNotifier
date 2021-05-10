using SteamInventoryNotifier.Model;

namespace SteamInventoryNotifier.Interfaces
{
    public interface INotifier
    {
        string Notify(NotificationMessage message);
    }
}
