using BikeTrips.Services.Data.Contracts;
using System.Linq;
using BikeTrips.Data.Models;
using BikeTrips.Data.Common.Contracts;

namespace BikeTrips.Services.Data
{
    public class TripsService : ITripsService
    {
        IBikeTripsDbRepository<Trip> trips;
        public TripsService()
        {
        }

        public TripsService(IBikeTripsDbRepository<Trip> trips)
        {
            this.trips = trips;
        }

        public int MyProperty { get; set; }
        public IQueryable<Trip> GetComingTrips(int count)
        {
            return this.trips.All()
                .Where(x => !x.IsPassed)
                .OrderBy(x => x.TripDate).Take(5);
        }
    }
}
