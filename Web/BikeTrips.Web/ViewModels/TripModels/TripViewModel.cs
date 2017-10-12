namespace BikeTrips.Web.ViewModels.TripModels
{
    using System;

    using Data.Models;
    using Infrastructure.Mappings;
    using Services.Web;
    using Services.Web.Contracts;
    using UserModels;

    public class TripViewModel : IMapFrom<Trip>
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public TripType Type { get; set; }

        public DateTime StartingTime { get; set; }

        public ShortUserViewModel Creator { get; set; }

        public short LocalTimeOffsetMinutes { get; set; }

        public string UrlId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return identifier.GetUrlId(this.Id);
            }
        }
    }
}