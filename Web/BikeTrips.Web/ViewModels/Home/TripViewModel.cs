using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;

namespace BikeTrips.Web.ViewModels.Home
{
    public class TripViewModel : IMapFrom<Trip>
    {
        public string TripName { get; set; }

        public string TripDate { get; set; }

        public User User { get; set; }
    }
}