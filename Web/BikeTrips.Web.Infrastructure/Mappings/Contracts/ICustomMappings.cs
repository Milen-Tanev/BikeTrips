namespace BikeTrips.Web.Infrastructure.Mappings
{
    using AutoMapper;

    public interface ICustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
