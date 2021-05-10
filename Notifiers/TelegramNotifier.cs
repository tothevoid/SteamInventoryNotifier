using Newtonsoft.Json;
using SteamInventoryNotifier.Http;
using SteamInventoryNotifier.Interfaces;
using SteamInventoryNotifier.Model;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SteamInventoryNotifier.Notifiers
{
    public class TelegramNotifier : INotifier
    {
        private readonly string _telegramUrl = "https://api.telegram.org";
        private readonly string _token;
        private readonly string _chatId;
        public TelegramNotifier()
        {
            _token = ConfigurationManager.AppSettings["TelegramToken"].ToString();
            _chatId = ConfigurationManager.AppSettings["TeelgramBotId"].ToString();

            if (string.IsNullOrEmpty(_token) || string.IsNullOrEmpty(_chatId))
            {
                throw new ConfigurationErrorsException("There is no telegram api key or chat id");
            }
        }

        public string Notify(NotificationMessage message)
        {
            var msg = $"<b>You've got a new item: {message.ItemName}</b>";
            var imageLink = $"<a href=\"{message.ImageLink}\">&#8205;</a>";
            var url = $"{_telegramUrl}/bot{_token}/sendMessage";

            var payload = new TelegramPayload
            {
                ChatId = _chatId,
                Text = $"{msg}{imageLink}",
                ParseMode = "html",
                ReplyMarkup = @"{""inline_keyboard"": [[{""text"":""Перейти к объявлению"", ""url"": """ + message.MarketLink + @"""}]]}"
            };

            string jsonString = JsonConvert.SerializeObject(payload);

            return new HttpRequestHelper().SendPost(_telegramUrl, jsonString);
        }
    }
}
