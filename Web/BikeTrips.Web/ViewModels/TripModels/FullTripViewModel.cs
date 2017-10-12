namespace BikeTrips.Web.ViewModels.TripModels
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    using CommentModels;
    using Data.Models;
    using Infrastructure.Mappings;
    using Services.Web.Contracts;
    using Services.Web;
    using UserModels;

    public class FullTripViewModel : IMapFrom<Trip>, IMapTo<Trip>, ICustomMapped
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public TripType Type { get; set; }

        public DateTime StartingTime { get; set; }

        public double Distance { get; set; }

        public double Denivelation { get; set; }

        public string Description { get; set; }

        public short LocalTimeOffsetMinutes { get; set; }

        public DateTime ServerTimeReservation { get; protected set; }

        public string User { get; set; }

        public User Creator { get; set; }

        public ICollection<ShortUserViewModel> Participants { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

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
            configuration.CreateMap<Trip, FullTripViewModel>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.Creator.UserName));
        }
    }
}