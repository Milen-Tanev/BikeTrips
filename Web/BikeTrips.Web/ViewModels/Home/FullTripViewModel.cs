using System;
using AutoMapper;
using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using System.Collections.Generic;

namespace BikeTrips.Web.ViewModels.Home
{
    public class FullTripViewModel : IMapFrom<Trip>, ICustomMappings
    {
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

        public virtual ICollection<User> Participants { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Trip, FullTripViewModel>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.Creator.Name));
        }
    }
}