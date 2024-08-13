namespace Components
{
    public static class Constants
    {

        public static int minimumReleaseYear = 1955;
        public static int maximumReleaseYear = 3000;


        public static class IgdbApi
        {
            public static string ArtworkQueryUri = "https://api.igdb.com/v4/artworks";
            public static string ArtworkBodyParams = "fields alpha_channel,animated,checksum,game,height,image_id,url,width;";
            public static string CoverQueryUri = "https://api.igdb.com/v4/covers";
            public static string CoverBodyParams = "fields alpha_channel,animated,checksum,game,height,image_id,url,width;";
            public static string GameQueryUri = "https://api.igdb.com/v4/games";
            public static string GameBodyParams = "fields age_ratings,aggregated_rating,aggregated_rating_count,alternative_names,artworks,bundles,category,checksum,collection,collections,cover,created_at,dlcs,expanded_games,expansions,external_games,first_release_date,follows,forks,franchise,franchises,game_engines,game_localizations,game_modes,genres,hypes,involved_companies,keywords,language_supports,multiplayer_modes,name,parent_game,platforms,player_perspectives,ports,rating,rating_count,release_dates,remakes,remasters,screenshots,similar_games,slug,standalone_expansions,status,storyline,summary,tags,themes,total_rating,total_rating_count,updated_at,url,version_parent,version_title,videos,websites;";
            public static string GenreQueryUri = "https://api.igdb.com/v4/genres";
            public static string GenreBodyParams = "fields checksum,created_at,name,slug,updated_at,url; limit 500;";
            public static string CompaniesQueryUri = "https://api.igdb.com/v4/companies";
            public static string CompaniesBodyParams = "fields change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;";
            public static string DevelopersQueryUri = "https://api.igdb.com/v4/involved_companies";
            public static string DevelopersQueryBodyParams = "fields checksum,company,created_at,developer,game,porting,publisher,supporting,updated_at;";
            public static string ReleaseDateUri = "https://api.igdb.com/v4/release_dates";
            public static string ReleaseDateBodyParams = "fields category,checksum,created_at,date,game,human,m,platform,region,status,updated_at,y;";
            public static string PlatformQueryUri = "https://api.igdb.com/v4/platforms";
            public static string PlatformBodyParams = "fields abbreviation,alternative_name,category,checksum,created_at,generation,name,platform_family,platform_logo,slug,summary,updated_at,url,versions,websites; limit 500;";
            public static string InvolvedCompanyQueryUri = "https://api.igdb.com/v4/involved_companies";
            public static string InvolvedCompaniesQueryBodyParams = "fields checksum,company,created_at,developer,game,porting,publisher,supporting,updated_at;";


        }

        public static class MicrosoftGraph
        {
            public static string[] GraphUserQueryParams = {"id", "displayName", "givenName", "country", "identities"};
        }
        
        public static class LookupCodes
        {
            public static class UserRelationshipTypeLookup
            {
                public static string FriendCode = "FRIEND";
                public static string IgnoreCode = "IGNORE";
            }
        }
    
    }
}
