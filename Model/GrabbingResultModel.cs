using System;

namespace SteamInventoryNotifier.Model
{
    public class GrabbingResultModel: BaseModel
    {
        public int LastFetchItemsQuantity { get; set; }

        private string output;
        public string Output { get { return output; } private set { Set(ref output, value); } }

        public void WriteInOutput(string message)
        {
            Output = $"{DateTime.Now.ToString("dd.MM HH:mm:ss")} - {message}";
        }
    }
}
