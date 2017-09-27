using BikeTrips.Data.Models;
using System.Linq;

namespace BikeTrips.Services.Data.Contracts
{
    public interface ITripsService
    {
        IQueryable<Trip> GetComingTrips(int count);
    }
}
