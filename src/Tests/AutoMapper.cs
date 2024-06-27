using System.Reflection;
using AutoMapper;

namespace Tests;

internal static class AutoMapper
{
    private static IMapper? mapper;
    public static IMapper GetMapper()
    {
        if (mapper is null)
        {
            var configuration = new MapperConfiguration(config => config.AddMaps(typeof(Domain.AutoMapperProfiles.CustomerProfile).GetTypeInfo().Assembly));
            mapper = new Mapper(configuration);
        }

        return mapper;
    }
}
