using AutoMapper;
using BikeTrips.Data.Models;
using BikeTrips.Services.Web;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mappings;
using System;

namespace BikeTrips.Web.ViewModels.TripModels
{
    public class TripViewModel : IMapFrom<Trip>, ICustomMappings
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public TripType Type { get; set; }

        public DateTime StartingTime { get; set; }

        public string User { get; set; }

        public string UrlId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return identifier.GetUrlId(this.Id);
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Trip, TripViewModel>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.Creator.UserName));
        }
    }
}