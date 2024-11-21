using Mapster;
using MapsterMapper;
using System.Reflection;

namespace VicStelmak.Sma.ProductMicroservice.Application
{
    public static class MapsterInvoker
    {
        public static Mapper GetMapper()
        {
            var mapperConfiguration = TypeAdapterConfig.GlobalSettings;
            mapperConfiguration.Scan(Assembly.GetExecutingAssembly());

            return new Mapper(mapperConfiguration);
        }
    }
}
