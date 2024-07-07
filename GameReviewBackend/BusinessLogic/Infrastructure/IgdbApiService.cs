using BusinessLogic.Abstractions;
using Components;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repositories;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.Infrastructure
{
    //https://api-docs.igdb.com/?java#game-video
    public class IgdbApiService : IIgdbApiService
    {
        GenericRepository<DockerDbContext> _genericRepository;
        private readonly IConfiguration _config;
        private JObject? _accessToken = null;
        private List<int[]> _childDlcGameIds;
        private List<int[]> _childExpansionGameIds;


        public IgdbApiService(GenericRepository<DockerDbContext> genericRepository, IConfiguration config)
        {
            _genericRepository = genericRepository;
            _config = config;
            _childDlcGameIds = new List<int[]>();
            _childExpansionGameIds = new List<int[]>();
        }

        public async Task QueryApi()
        {

            await insertGenres();
            await insertCompanies();
            await insertGames();
            await insertPlatforms();

            await insertDlcs();
            await insertExpansions();
            await insertGamePlatformLinks();
            await insertGameCompaniesLinks();

            await insertArtworks();
            await insertCovers();
        }

        public async Task<string> GetOneGame()
        {
            var limit = 1;
            string bodyParams = string.Concat(Constants.IgdbApi.GameBodyParams, $"limit {limit};");

            string gameJson = await GetGenericApiCall(Constants.IgdbApi.GameQueryUri, bodyParams);
            return gameJson;
        }

        private async Task<string> GetAccessToken()
        {

            var uri = $"https://id.twitch.tv/oauth2/token?client_id={_config["Igdb:ClientId"]}&client_secret={_config["Igdb:ClientSecret"]}&grant_type=client_credentials";
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(uri, null);
                var accessToken = await result.Content.ReadAsStringAsync();
                return accessToken;
            }
        }
        private async Task insertGenres()
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
                    Description = genre["slug"].ToString()
                };
                genres.Add(genresLookup);
            }

            _genericRepository.InsertRecordList(genres);
        }
        private async Task insertCompanies()
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
                    // ObsoleteFlag = false,
                    // ObsoleteDate = null,
                    // ModifiedBy = null,
                    // ModifiedDate = null
                };
                companies.Add(companyEntity);
            }
            _genericRepository.InsertRecordList(companies);
        }
        private async Task insertGames()
        {
            var offset = 0;
            var limit = 500;
            int gameCountOnPage = 0;
            var totalList = new List<JToken>();

            do
            {
                string paginatedGameBodyParams = string.Concat(Constants.IgdbApi.GameBodyParams, $"limit {limit};", $"offset {offset};");
                var gamesJson = await GetGenericApiCall(Constants.IgdbApi.GameQueryUri, paginatedGameBodyParams);
                var gameJArray = JsonConvert.DeserializeObject(gamesJson) != null ? ((JArray)JsonConvert.DeserializeObject(gamesJson)) : null;
                gameCountOnPage = gameJArray.Count();

                totalList.AddRange(gameJArray);
                offset += limit;
            }
            while (gameCountOnPage == limit);

            // var mySet = totalList.Where(o => !o["name"].ToString().Contains("(Archive)"));
            var count = totalList.Count();

            List<Games> games = new List<Games>();
            var now = DateTime.Now;

            foreach (var gameJToken in totalList)
            {
                var genreIds = gameJToken["genres"] != null ? ((JArray)gameJToken["genres"]).ToObject<int[]>() : null;
                var dlcGameIds = gameJToken["dlcs"] != null ? ((JArray)gameJToken["dlcs"]).ToObject<int[]>() : null;
                var expansionGameIds = gameJToken["expansions"] != null ? ((JArray)gameJToken["expansions"]).ToObject<int[]>() : null;

                if (dlcGameIds != null)
                {
                    foreach (int dlcGameId in dlcGameIds)
                    {
                        _childDlcGameIds.Add([gameJToken["id"]?.ToObject<int>() ?? 0, dlcGameId]);
                    }
                }

                if (expansionGameIds != null)
                {
                    foreach (int expansionGameId in expansionGameIds)
                    {
                        _childExpansionGameIds.Add([gameJToken["id"]?.ToObject<int>() ?? 0, expansionGameId]);
                    }
                }

                Games gameEntity = new Games()
                {
                    Id = gameJToken["id"]?.ToObject<int>() ?? 0,
                    Title = gameJToken["name"].ToString() ?? "",
                    ReleaseDate = UnixTimeStampToDateTime(gameJToken["first_release_date"]?.ToObject<long>()), //get enum value here
                    Description = gameJToken["summary"]?.ToString() ?? "PLACEHOLDER",
                };

                //If the game has associated genres, add the links to the game
                if (genreIds != null)
                {
                    List<GamesGenresLookupLink> genreLinks = new List<GamesGenresLookupLink>();
                    foreach (var genreId in genreIds)
                    {
                        GamesGenresLookupLink linkEntity = new GamesGenresLookupLink()
                        {
                            GameId = gameEntity.Id,
                            GenreLookupId = genreId
                        };
                        genreLinks.Add(linkEntity);
                    }
                    gameEntity.GamesGenresLookupLink = genreLinks;
                }
                games.Add(gameEntity);
            }

            // games = games.DistinctBy(g => g.Id).ToList();
            // _genericRepository.InsertRecordList(games);

        }

        private async Task insertCovers()
        {
            var offset = 0;
            var limit = 500;
            int coverCountOnPage = 0;
            var totalList = new List<JToken>();

            do
            {
                string paginatedCoverBodyParams = string.Concat(Constants.IgdbApi.CoverBodyParams, $"limit {limit};", $"offset {offset};");
                var coverJson = await GetGenericApiCall(Constants.IgdbApi.CoverQueryUri, paginatedCoverBodyParams);
                var coverJArray = JsonConvert.DeserializeObject(coverJson) != null ? ((JArray)JsonConvert.DeserializeObject(coverJson)) : null;
                coverCountOnPage = coverJArray.Count();

                totalList.AddRange(coverJArray);
                offset += limit;
            }
            while (coverCountOnPage == limit);

            var count = totalList.Count();
            var gameIdInDbList = _genericRepository.GetAll<Games>().Select(g => g.Id).ToList();

            List<Cover> covers = new List<Cover>();
            var now = DateTime.Now;

            foreach (var coverJToken in totalList)
            {
                var gameId = coverJToken["game"]?.ToObject<int>();

                if (gameId != null && gameIdInDbList.Contains(gameId.Value))
                {
                    Cover coverEntity = new Cover()
                    {
                        Id = coverJToken["id"]?.ToObject<int>() ?? 0,
                        GameId = coverJToken["game"]?.ToObject<int>() ?? 0,
                        AlphaChannelFlag = coverJToken["alpha_channel"]?.ToObject<bool>() ?? false,
                        AnimatedFlag = coverJToken["animated"]?.ToObject<bool>() ?? false,
                        Height = coverJToken["height"]?.ToObject<int>() ?? 0,
                        Width = coverJToken["width"]?.ToObject<int>() ?? 0,
                        ImageUrl = (coverJToken["url"]?.ToString() ?? "PLACEHOLDER").Replace("t_thumb", "t_1080p"),
                    };
                    covers.Add(coverEntity);
                }
            }
            covers = covers.DistinctBy(a => a.Id).ToList();
            _genericRepository.InsertRecordList(covers);
        }

        private async Task insertArtworks()
        {
            var offset = 0;
            var limit = 500;
            int artworkCountOnPage = 0;
            var totalList = new List<JToken>();

            do
            {
                string paginatedArtworkBodyParams = string.Concat(Constants.IgdbApi.ArtworkBodyParams, $"limit {limit};", $"offset {offset};");
                var artworkJson = await GetGenericApiCall(Constants.IgdbApi.ArtworkQueryUri, paginatedArtworkBodyParams);
                var artworkJArray = JsonConvert.DeserializeObject(artworkJson) != null ? ((JArray)JsonConvert.DeserializeObject(artworkJson)) : null;
                artworkCountOnPage = artworkJArray.Count();

                totalList.AddRange(artworkJArray);
                offset += limit;
            }
            while (artworkCountOnPage == limit);

            var count = totalList.Count();
            var gameIdInDbList = _genericRepository.GetAll<Games>().Select(g => g.Id).ToList();

            List<Artwork> artworks = new List<Artwork>();
            var now = DateTime.Now;

            foreach (var artworkJToken in totalList)
            {
                var gameId = artworkJToken["game"].ToObject<int>();

                if (gameIdInDbList.Contains(gameId))
                {
                    Artwork artworkEntity = new Artwork()
                    {
                        Id = artworkJToken["id"]?.ToObject<int>() ?? 0,
                        GameId = artworkJToken["game"]?.ToObject<int>() ?? 0,
                        AlphaChannelFlag = artworkJToken["alpha_channel"]?.ToObject<bool>() ?? false,
                        AnimatedFlag = artworkJToken["animated"]?.ToObject<bool>() ?? false,
                        Height = artworkJToken["height"]?.ToObject<int>() ?? 0,
                        Width = artworkJToken["width"]?.ToObject<int>() ?? 0,
                        ImageUrl = (artworkJToken["url"]?.ToString() ?? "PLACEHOLDER").Replace("t_thumb", "t_1080p"),
                    };
                    artworks.Add(artworkEntity);
                }
            }
            artworks = artworks.DistinctBy(a => a.Id).ToList();
            _genericRepository.InsertRecordList(artworks);
        }
        private async Task insertPlatforms()
        {
            var platformsJson = await GetGenericApiCall(Constants.IgdbApi.PlatformQueryUri, Constants.IgdbApi.PlatformBodyParams);

            List<Platforms> platforms = new List<Platforms>();
            var now = DateTime.Now;

            foreach (var platform in (JArray)JsonConvert.DeserializeObject(platformsJson))
            {
                Platforms platformEntity = new Platforms()
                {
                    Id = platform["id"].ToObject<int>(),
                    Name = platform["name"].ToString(),
                    ReleaseDate = default,
                    ImageFilePath = "PLACEHOLDER"
                };
                platforms.Add(platformEntity);
            }
            try
            {
                _genericRepository.InsertRecordList(platforms);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        private async Task insertDlcs()
        {
            if (_childDlcGameIds != null)
            {
                var gameIds = _genericRepository.GetAll<Games>().Select(g => g.Id);
                var dlcLookupId = _genericRepository.GetSingleNoTrack<GameSelfLinkTypeLookup>(g => g.Code == "DLC").Id;

                List<GameSelfLink> gameSelfLinks = new List<GameSelfLink>();
                foreach (int[] dlcGamePair in _childDlcGameIds)
                {
                    if (gameIds.Contains(dlcGamePair[0]) && gameIds.Contains(dlcGamePair[1]))
                    {

                        GameSelfLink linkEntity = new GameSelfLink()
                        {
                            ParentGameId = dlcGamePair[0],
                            ChildGameId = dlcGamePair[1],
                            GameSelfLinkTypeLookupId = dlcLookupId
                        };
                        gameSelfLinks.Add(linkEntity);
                    }
                }

                _genericRepository.InsertRecordList(gameSelfLinks);
            }
        }


        private async Task insertExpansions()
        {
            if (_childExpansionGameIds != null)
            {
                var gameIds = _genericRepository.GetAll<Games>().Select(g => g.Id);
                var expansionLookupId = _genericRepository.GetSingleNoTrack<GameSelfLinkTypeLookup>(g => g.Code == "EXPANS").Id;

                List<GameSelfLink> gameSelfLinks = new List<GameSelfLink>();
                foreach (int[] expansionGamePair in _childExpansionGameIds)
                {
                    if (gameIds.Contains(expansionGamePair[0]) && gameIds.Contains(expansionGamePair[1]))
                    {

                        GameSelfLink linkEntity = new GameSelfLink()
                        {
                            ParentGameId = expansionGamePair[0],
                            ChildGameId = expansionGamePair[1],
                            GameSelfLinkTypeLookupId = expansionLookupId
                        };
                        gameSelfLinks.Add(linkEntity);
                    }
                }
                _genericRepository.InsertRecordList(gameSelfLinks);
            }
        }

        private async Task insertGamePlatformLinks()
        {
            var offset = 0;
            var limit = 500;
            int gameCountOnPage = 0;
            var totalList = new List<JToken>();

            do
            {
                string paginatedGameBodyParams = string.Concat(Constants.IgdbApi.GameBodyParams, $"limit {limit};", $"offset {offset};");
                var gamesJson = await GetGenericApiCall(Constants.IgdbApi.GameQueryUri, paginatedGameBodyParams);
                var gameJArray = JsonConvert.DeserializeObject(gamesJson) != null ? ((JArray)JsonConvert.DeserializeObject(gamesJson)) : null;
                gameCountOnPage = gameJArray.Count();

                totalList.AddRange(gameJArray);
                offset += limit;
            }
            while (gameCountOnPage == limit);

            // var mySet = totalList.Where(o => !o["name"].ToString().Contains("(Archive)"));
            var count = totalList.Count();

            var now = DateTime.Now;
            List<GamesPlatformsLink> gamesPlatformsLinks = new List<GamesPlatformsLink>();
            var platformIdInDbList = _genericRepository.GetAll<Platforms>().Select(p => p.Id).ToList();
            var gameIdInDbList = _genericRepository.GetAll<Games>().Select(g => g.Id).ToList();

            foreach (var gameJToken in totalList)
            {
                var platformIds = gameJToken["platforms"] != null ? ((JArray)gameJToken["platforms"]).ToObject<int[]>() : null;
                var gameId = gameJToken["id"].ToObject<int>();

                //If the game has associated genres, add the links to the game
                if (platformIds != null)
                {
                    foreach (var platformId in platformIds)
                    {
                        if (gameIdInDbList.Contains(gameId) && platformIdInDbList.Contains(platformId))
                        {
                            GamesPlatformsLink linkEntity = new GamesPlatformsLink()
                            {
                                GameId = gameId,
                                PlatformId = platformId,
                                ReleaseDate = null
                            };
                            gamesPlatformsLinks.Add(linkEntity);
                        }

                    }
                }
            }

            try
            {
                _genericRepository.InsertRecordList(gamesPlatformsLinks);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

            }
        }
        private async Task insertGameCompaniesLinks()
        {
            var offset = 0;
            var limit = 500;
            int countOnPage = 0;
            var totalList = new List<JToken>();

            do
            {
                string paginatedBodyParams = string.Concat(Constants.IgdbApi.InvolvedCompaniesQueryBodyParams, $"limit {limit};", $"offset {offset};");
                var json = await GetGenericApiCall(Constants.IgdbApi.InvolvedCompanyQueryUri, paginatedBodyParams);
                var deserializedJson = JsonConvert.DeserializeObject(json);
                var jArray = deserializedJson != null ? ((JArray)deserializedJson) : null;
                countOnPage = jArray.Count();

                totalList.AddRange(jArray);
                offset += limit;
            }
            while (countOnPage == limit);

            // var mySet = totalList.Where(o => !o["name"].ToString().Contains("(Archive)"));
            var count = totalList.Count();

            var now = DateTime.Now;
            List<GamesCompaniesLink> gamesCompaniesLinks = new List<GamesCompaniesLink>();

            var companiesIdInDbList = _genericRepository.GetAll<Companies>().Select(c => c.Id).ToList();
            var gameIdInDbList = _genericRepository.GetAll<Games>().Select(g => g.Id).ToList();

            foreach (var jToken in totalList)
            {
                var companyId = jToken["company"].ToObject<int>();
                var gameId = jToken["game"].ToObject<int>();
                var developerFlag = jToken["developer"].ToObject<bool>();
                var publisherFlag = jToken["publisher"].ToObject<bool>();
                var portingFlag = jToken["porting"].ToObject<bool>();
                var supportingFlag = jToken["supporting"].ToObject<bool>();

                //If the game has associated genres, add the links to the game

                if (gameIdInDbList.Contains(gameId) && companiesIdInDbList.Contains(companyId))
                {
                    GamesCompaniesLink linkEntity = new GamesCompaniesLink()
                    {
                        GamesId = gameId,
                        CompaniesId = companyId,
                        DeveloperFlag = developerFlag,
                        PublisherFlag = publisherFlag,
                        PortingFlag = portingFlag,
                        SupportingFlag = supportingFlag
                    };
                    gamesCompaniesLinks.Add(linkEntity);
                }
            }
            try
            {
                _genericRepository.InsertRecordList(gamesCompaniesLinks);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

            }
        }

        public static DateOnly UnixTimeStampToDateTime(long? unixTimeStamp)
        {
            if (unixTimeStamp == null)
            {
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
            string? token = null;

            if (_accessToken == null) //Refresh the token
            {
                var tokenJson = await GetAccessToken();
                _accessToken = (JObject)JsonConvert.DeserializeObject(tokenJson);
                token = _accessToken["access_token"].Value<string>();
            }
            else
            {
                token = _accessToken["access_token"].Value<string>();
            }


            var contentData = new StringContent(bodyParams, Encoding.UTF8, "application/text");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Client-ID", _config["Igdb:ClientId"]);
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await httpClient.PostAsync(uri, contentData);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
