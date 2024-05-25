using BusinessLogic.Abstractions;
using Components;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repositories;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.Infrastructure
{
    //https://api-docs.igdb.com/?java#game-video
    public class IgdbApiService : IIgdbApiService
    {
        GenericRepository<DockerDbContext> _genericRepository;
        private string CLIENT_ID = "0w2j0mktoxkvehox8hl42p15tladu0";
        private string CLIENT_SECRET = "gpptkmravp51lkhl5d22msu8i2rs2f";

        public IgdbApiService(GenericRepository<DockerDbContext> genericRepository)
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
            var releaseDateJson = await GetGenericApiCall(Constants.IgdbApi.ReleaseDateUri, Constants.IgdbApi.ReleaseDateBodyParams);

            insertGenres();
            insertCompanies();
            

            //Game
            //List<GenresLookup> genresLookups = _genericRepository.GetAll<GenresLookup>().ToList();
            //List<Games> games = new List<Games>();
            //var now = DateTime.Now;

            //foreach (var gameJToken in (JArray)JsonConvert.DeserializeObject(gamesJson))
            //{
            //    var genreIds = gameJToken["genres"] != null ? ((JArray)gameJToken["genres"]).ToObject<int[]>() : null;

            //    Games gameEntity = new Games()
            //    {
            //        Id = gameJToken["id"].ToObject<int>(),
            //        Title = gameJToken["name"].ToString(),
            //        ReleaseDate = UnixTimeStampToDateTime(gameJToken["first_release_date"]?.ToObject<int>()), //get enum value here
            //        ImageFilePath = "PLACEHOLDER_PATH",
            //        Description = gameJToken["summary"]?.ToString() ?? "PLACEHOLDER",
            //        CreatedBy = "SamJDriver",
            //        CreatedDate = now,
            //        ModifiedBy = null,
            //        ModifiedDate = null,
            //        ObsoleteFlag = false,
            //        ObsoleteDate = null
            //    };

            //    List<GamesGenresLookupLink> genreLinks = new List<GamesGenresLookupLink>();
            //    //If the game has associated genres, add the links to the game
            //    if (genreIds != null)
            //    {
            //        foreach (var genreId in genreIds)
            //        {
            //            GamesGenresLookupLink linkEntity = new GamesGenresLookupLink()
            //            {
            //                GameId = gameEntity.Id,
            //                GenreLookupId = genreId,
            //                CreatedBy = "SamJDriver",
            //                CreatedDate = now,
            //                ModifiedBy = null,
            //                ModifiedDate = null,
            //                ObsoleteFlag = false,
            //                ObsoleteDate = null,
            //            };
            //            genreLinks.Add(linkEntity);
            //        }
            //        gameEntity.GamesGenresLookupLink = genreLinks;
            //    }
            //    games.Add(gameEntity);
            //}

            //_genericRepository.InsertRecordList(games);

            return "";
        }

        private async void insertGenres()
        {
            var genresJson = await GetGenericApiCall(Constants.IgdbApi.GenreQueryUri, Constants.IgdbApi.GenreBodyParams);

            List<GenresLookup> genres = new List<GenresLookup>();    
            var now = DateTime.Now;

            foreach (var genre in (JArray)JsonConvert.DeserializeObject(genresJson)) 
            {
               string name = genre["name"].ToString();
               string code = new string(ReplaceWhitespace(name, "").Take(8).ToArray()).ToUpper();
               GenresLookup genresLookup = new GenresLookup()
               {
                   Id = genre["id"].ToObject<int>(),
                   Name = name,
                   Code = code,
                   Description = genre["slug"].ToString(),
                   ObsoleteFlag = false,
                   ObsoleteDate = null,
                   ModifiedBy = null,
                   ModifiedDate = null,
                   CreatedBy = "SamJDriver",
                   CreatedDate = now,
               };
               genres.Add(genresLookup);
            }

            _genericRepository.InsertRecordList(genres);
        }
        private async void insertCompanies()
        {
            var offset = 0;
            var limit = 500;
            int companyCountOnPage = 0;
            var totalList = new List<JToken>();

            do
            {
                string paginatedCompanyBodyParams = string.Concat(Constants.IgdbApi.CompaniesBodyParams, $"limit {limit};", $"offset {offset};");
                var companyJson = await GetGenericApiCall(Constants.IgdbApi.CompaniesQueryUri, paginatedCompanyBodyParams);
                var companyJArray = JsonConvert.DeserializeObject(companyJson) != null ? ((JArray)JsonConvert.DeserializeObject(companyJson)) : null;
                companyCountOnPage = companyJArray.Count();

                totalList.AddRange(companyJArray);

                offset += limit;
            }
            while (companyCountOnPage == limit);

            var mySet = totalList.OrderBy(o => o["id"].ToObject<int>()).Where(o => !o["name"].ToString().Contains("(Archive)"));
            var count = mySet.Count();

                        List<Companies> companies = new List<Companies>();
            var now = DateTime.Now;

            foreach (var companyJsonElement in mySet)
            {
                Companies companyEntity = new Companies()
                {
                    Id = companyJsonElement["id"].ToObject<int>(),
                    Name = companyJsonElement["name"].ToString(),
                    FoundedDate = UnixTimeStampToDateTime(companyJsonElement["start_date"]?.ToObject<long>()),
                    ImageFilePath = "PLACEHOLDER",
                    DeveloperFlag = companyJsonElement["developed"] != null,
                    PublisherFlag = companyJsonElement["published"] != null,
                    ObsoleteFlag = false,
                    ObsoleteDate = null,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    CreatedBy = "SamJDriver",
                    CreatedDate = now,
                };
                companies.Add(companyEntity);
            }
            _genericRepository.InsertRecordList(companies);
        }

        public static DateOnly UnixTimeStampToDateTime(long? unixTimeStamp)
        {
            if (unixTimeStamp == null){
                return new DateOnly();
            }

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (unixTimeStamp.Value < -62135596800)
            {
                unixTimeStamp = -62135596800;
            }
            else if (unixTimeStamp.Value > 253402300799)
            {
                unixTimeStamp = 253402300799;
            }

            dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp.Value).UtcDateTime;
            return DateOnly.FromDateTime(dateTime);
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
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
