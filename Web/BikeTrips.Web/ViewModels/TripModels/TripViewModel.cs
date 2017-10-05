namespace BikeTrips.Web.ViewModels.TripModels
{
    using AutoMapper;
    using System;

    using Data.Models;
    using Services.Web;
    using Services.Web.Contracts;
    using Infrastructure.Mappings;

    public class TripViewModel : IMapFrom<Trip>, ICustomMappings
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public TripType Type { get; set; }

        public DateTime StartingTime { get; set; }

        public string User { get; set; }

        public short LocalTimeOffsetMinutes { get; set; }

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