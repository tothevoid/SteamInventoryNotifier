using SteamInventoryNotifier.Extensions;
using System;

namespace SteamInventoryNotifier.Model
{
    public class GrabbingParametersModel: BaseModel
    {
        #region Events
        public event Action<int> OnFrequencyChanged;
        #endregion

        private long profileId = 76561198016083607;
        public long ProfileId { get { return profileId; } set { Set(ref profileId, value); } }

        private int frequency = 30;
        public int Frequency 
        { 
            get { return frequency; } 
            set 
            { 
                Set(ref frequency, value); 
                OnFrequencyChanged?.Invoke(value); 
            } 
        }

        private int appId = 730;
        public int AppId { get { return appId; } set { Set(ref appId, value); } }

        public string Validate()
        {
            var appIdDigits = appId.GetDigitsQuantity();
            if (ProfileId.GetDigitsQuantity() != 17)
            {
                return "Profile Id must to contain of 17 digits";
            }
            else if (frequency < 5)
            {
                return "Frequency must to be positive and more than 5 seconds";
            }
            else if (appIdDigits < 3 || appIdDigits > 6)
            {
                return "App Id must to be between 3 and 6 digits";
            }
            return string.Empty;
        }
    }
}
