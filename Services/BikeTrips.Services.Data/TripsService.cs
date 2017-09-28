using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Models;
using BikeTrips.Data.Common.Contracts;
using System.Linq;


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

        public void AddTrip(Trip trip)
        {
            this.trips.Add(trip);
            this.trips.Save();
        }

        public IQueryable<Trip> GetComingTrips(int count)
        {
            return this.trips.All()
                .Where(x => !x.IsPassed)
                .OrderBy(x => x.StartingTime)
                .Take(count);
        }
    }
}
