using AutoMapper;

namespace BikeTrips.Web.Infrastructure.Mappings
{
    public interface ICustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
