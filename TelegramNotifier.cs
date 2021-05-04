using Newtonsoft.Json;
using SteamInventoryNotifier.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier
{
    public class TelegramNotifier : INotifier
    {
        private readonly string _telegramUrl = "https://api.telegram.org";
        private readonly string _token;
        private readonly string _chatId;
        public TelegramNotifier()
        {
            //TODO: config implementation
        }

        public bool Notify(NotificationMessage message)
        {
            var msg = $"<b>You've got a new item: {message.ItemName}</b>";
            var imageLink = $"<a href=\"{message.ImageLink}\">&#8205;</a>";

            var requester = new Requester();
            var url = $"{_telegramUrl}/bot{_token}/sendMessage";

            var payload = new TelegramPayload
            {
                ChatId = _chatId,
                Text = $"{msg}{imageLink}",
                ParseMode = "html",
                ReplyMarkup = @"{""inline_keyboard"": [[{""text"":""Перейти к объявлению"", ""url"": """ + message.MarketLink + @"""}]]}"
            };

            string jsonString = JsonConvert.SerializeObject(payload);

            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
                };

                var response = client.Send(webRequest);

                using (var reader = new StreamReader(response.Content.ReadAsStream()))
                {
                    var result = reader.ReadToEnd();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured {ex.Message}");
                return false;
            }
           
        }
    }
}
