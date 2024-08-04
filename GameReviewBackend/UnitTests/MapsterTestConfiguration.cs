using Components.Mappings;
using Mapster;
using MapsterMapper;

namespace UnitTests;

public static class MapsterTestConfiguration
{
    public static Mapper GetMapper()
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(MappingConfig).Assembly);
        return new Mapper(config);
    }

}
