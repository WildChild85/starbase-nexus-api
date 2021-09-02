using starbase_nexus_api.Models.Authentication.Discord;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace starbase_nexus_api.Services.Authentication
{
    public class DiscordService : IDiscordService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public DiscordService(IOptions<DiscordOptions> options)
        {
            _clientId = options.Value.ClientId;
            _clientSecret = options.Value.ClientSecret;
        }

        public async Task<string> GetAccessToken(string code, string redirectUrl)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/oauth2/token");
            webRequest.Method = "POST";
            string parameters = "client_id=" + _clientId + "&client_secret=" + _clientSecret + "&grant_type=authorization_code&code=" + code + "&redirect_uri=" + redirectUrl + "";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(parameters);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream postStream = await webRequest.GetRequestStreamAsync();

            await postStream.WriteAsync(byteArray, 0, byteArray.Length);
            postStream.Close();

            WebResponse response;
            try
            {
                response = await webRequest.GetResponseAsync();
            } catch (WebException webException)
            {
                throw webException;
            }
            postStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(postStream);

            string responseFromServer = reader.ReadToEnd();
            DiscordTokenResponse deserialized = JsonConvert.DeserializeObject<DiscordTokenResponse>(responseFromServer);

            return deserialized.access_token;
        }

        public async Task<DiscordMeResponse> GetUserInfo(string accessToken)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/users/@me");
            webRequest.Method = "Get";
            webRequest.ContentLength = 0;
            webRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            string apiResponse = "";
            using (HttpWebResponse response = await webRequest.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exceptions.AccessDeniedException("");
                }
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = await reader.ReadToEndAsync();
            }
            DiscordMeResponse deserialized = JsonConvert.DeserializeObject<DiscordMeResponse>(apiResponse);
            return deserialized;
        }
    }
}
