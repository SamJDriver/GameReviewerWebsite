using Components.Models;
using DataAccess.Models.DockerDb;
using Mapster;
using System.Linq;

namespace Components.Mappings
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Games, Game_Get_ById_Dto>()
                .Map(dest => dest.ArtworkUrls, src => src.Artwork != null ? src.Artwork.Select(a => a.ImageUrl) : null)
                .Map(dest => dest.CoverImageUrl, src => src.Cover.FirstOrDefault() != null ? src.Cover.First().ImageUrl : null)
                .Map(dest => dest.Genres, src => src.GamesGenresLookupLink.Select(g => g.GenreLookup))
                .Map(dest => dest.Platforms, src => src.GamesPlatformsLink.Select(g => g.Platform))
                .Map(dest => dest.Companies, src => src.GamesCompaniesLink);

            config.NewConfig<Games, Game_Get_VanillaGame_Dto>()
                .Map(dest => dest.CoverImageUrl, src => src != null && src.Cover != null && src.Cover.FirstOrDefault() != null ? src.Cover.First().ImageUrl : "");

            config.NewConfig<GamesCompaniesLink, Game_Get_ById_CompanyLink_Dto>()
                .Map(dest => dest.CompanyId, src => src.CompaniesId)
                .Map(dest => dest.CompanyName, src => src.Companies.Name)
                .Map(dest => dest.CompanyImageFilePath, src => src.Companies.ImageFilePath)
                .Map(dest => dest.DeveloperFlag, src => src.DeveloperFlag)
                .Map(dest => dest.PublisherFlag, src => src.PublisherFlag)
                .Map(dest => dest.PortingFlag, src => src.PortingFlag)
                .Map(dest => dest.SupportingFlag, src => src.SupportingFlag);

            config.NewConfig<PlayRecords, PlayRecord_GetSelf_Dto>()
                .Map(dest => dest.CoverImageUrl, src => src.Game.Cover.SingleOrDefault() != null ? src.Game.Cover.Single().ImageUrl : null);

            config.NewConfig<PlayRecords, PlayRecord_GetById_Dto>()
                .Map(dest => dest.CoverImageUrl, src => src.Game.Cover.SingleOrDefault() != null ? src.Game.Cover.Single().ImageUrl : null)
                .Map(dest => dest.PlayRecordComments, src => src.PlayRecordComments);

            config.NewConfig<PlayRecordComments, PlayRecordCommentDto>()
                .Map(dest => dest.NumericalValue, src => src.PlayRecordCommentVote.Sum(p => p.NumericalValue));

            config.NewConfig<Games, Game_GetList_Dto>()
                .Map(dest => dest.GameId, src => src.Id)
                .Map(dest => dest.PlayRecordId, src => src.PlayRecords.Single().Id)
                .Map(dest => dest.CoverImageUrl, src => src.Cover.SingleOrDefault() != null ? src.Cover.Single().ImageUrl : null)
                .Map(dest => dest.Rating, src => src.PlayRecords.SingleOrDefault() != null ? src.PlayRecords.Single().Rating : null)
                .Map(dest => dest.ReviewerId, src => src.PlayRecords.Single().CreatedBy)
                .Map(dest => dest.ReviewDate, src => src.PlayRecords.Single().CreatedDate);
        }
    }
}
