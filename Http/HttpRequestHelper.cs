using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SteamInventoryNotifier.Http
{
    public class HttpRequestHelper
    {
        public string SendGet(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    return reader.ReadToEnd();
                }
            }
        }

        public string SendPost(string url, string json)
        {
            try
            {
                var client = new HttpClient();
                var webRequest = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                var response = client.Send(webRequest);

                using (var reader = new StreamReader(response.Content.ReadAsStream()))
                {
                    var result = reader.ReadToEnd();
                    return string.Empty;
                }
            }
            catch
            {
                //TODO: log it
                return "An exception occured while telegram notification";
            }
        }
    }
}
