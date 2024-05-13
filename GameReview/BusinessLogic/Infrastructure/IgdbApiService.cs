using BusinessLogic.Abstractions;
using Components;
using DataAccess.Contexts;
using DataAccess.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repositories;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.Infrastructure
{
    public class IgdbApiService : IIgdbApiService
    {
        GenericRepository<NickDbContext> _genericRepository;
        private string CLIENT_ID = "0w2j0mktoxkvehox8hl42p15tladu0";
        private string CLIENT_SECRET = "gpptkmravp51lkhl5d22msu8i2rs2f";

        public IgdbApiService(GenericRepository<NickDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        private async Task<string> GetAccessToken()
        {

            var uri = $"https://id.twitch.tv/oauth2/token?client_id={CLIENT_ID}&client_secret={CLIENT_SECRET}&grant_type=client_credentials";
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(uri, null);
                var accessToken = await result.Content.ReadAsStringAsync();
                return accessToken;
            }
        }

        public async Task<string> QueryApi()
        {

            var gamesJson = await GetGenericApiCall(Constants.IgdbApi.GameQueryUri, Constants.IgdbApi.GameBodyParams);
            var genresJson = await GetGenericApiCall(Constants.IgdbApi.GenreQueryUri, Constants.IgdbApi.GenreBodyParams);

            return "";
        }

        private async Task<string> GetGenericApiCall(string uri, string bodyParams)
        {
            var accessToken = await GetAccessToken();
            var data = (JObject)JsonConvert.DeserializeObject(accessToken);
            string token = data["access_token"].Value<string>();

            var contentData = new StringContent(bodyParams, Encoding.UTF8, "application/text");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Client-ID", CLIENT_ID);
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await httpClient.PostAsync(uri, contentData);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
