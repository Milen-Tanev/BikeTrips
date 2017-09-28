using BikeTrips.Data.Models;
using System.Linq;

namespace BikeTrips.Services.Data.Contracts
{
    public interface ITripsService
    {
        void AddTrip(Trip trip);

        IQueryable<Trip> GetComingTrips(int count);
    }
}
