using Components.Models;
using DataAccess.Models.DockerDb;
using Mapster;

namespace Components.Mappings
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PlayRecords, PlayRecord_GetSelf_Dto>()
            .Map(dest => dest.CoverImageUrl, src => src.Game.Cover.SingleOrDefault() != null ? src.Game.Cover.Single().ImageUrl : null);
        }
    }
}