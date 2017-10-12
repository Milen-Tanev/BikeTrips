namespace BikeTrips.Web.Infrastructure.Mappings
{
    using AutoMapper;

    public interface ICustomMapped
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
