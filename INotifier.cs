using SteamInventoryNotifier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier
{
    public interface INotifier
    {
        bool Notify(NotificationMessage message);
    }
}
