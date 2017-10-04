using System;
using AutoMapper;
using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using System.Collections.Generic;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Services.Web;
using BikeTrips.Web.ViewModels.UserModels;

namespace BikeTrips.Web.ViewModels.TripModels
{
    public class FullTripViewModel : IMapFrom<Trip>, IMapTo<Trip>, ICustomMappings
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public TripType Type { get; set; }

        public DateTime StartingTime { get; set; }

        public double Distance { get; set; }

        public double Denivelation { get; set; }

        public string Description { get; set; }

        public int LocalTimeOffsetMinutes { get; set; }

        public DateTime ServerTimeReservation { get; protected set; }

        public string User { get; set; }

        public User Creator { get; set; }

        public ICollection<ShortUserViewModel> Participants { get; set; }

        public ICollection<Comment> Comments { get; set; }

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
            //configuration.CreateMap<Trip, FullTripViewModel>()
            //    .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments
            //    .Select(p => p.Id).ToList()));
            configuration.CreateMap<Trip, FullTripViewModel>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.Creator.UserName));
        }
    }
}