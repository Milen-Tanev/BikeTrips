using AutoMapper;
using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;

namespace BikeTrips.Web.ViewModels.Home
{
    public class TripViewModel : IMapFrom<Trip>, ICustomMappings
    {
        public string TripName { get; set; }

        public string TripDate { get; set; }

        public string User { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Trip, TripViewModel>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.Creator.Name));
        }
    }
}