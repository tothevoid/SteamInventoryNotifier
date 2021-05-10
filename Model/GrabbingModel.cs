using System;

namespace SteamInventoryNotifier.Model
{
    public class GrabbingModel: BaseModel
    {
        #region Events
        public event Action<int> OnFrequencyChanged;
        #endregion

        private long profileId = 76561198016083607;
        public long ProfileId { get { return profileId; } set { Set(ref profileId, value); } }

        private int frequency = 30;
        public int Frequency { get { return frequency; } set { Set(ref frequency, value); OnFrequencyChanged?.Invoke(value); } }

        private int appId = 730;
        public int AppId { get { return appId; } set { Set(ref appId, value); } }

        private int lastFetchItemsQuantity;
        public int LastFetchItemsQuantity { get { return lastFetchItemsQuantity; } set { Set(ref lastFetchItemsQuantity, value); } }

        private string output;
        public string Output { get { return output; } private set { Set(ref output, value); } }
    
        public void WriteInOutput(string message)
        {
            Output = $"{DateTime.Now.ToString("dd.MM HH:mm:ss")} - {message}";
        }
    }
}
