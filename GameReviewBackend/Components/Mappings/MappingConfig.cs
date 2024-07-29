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