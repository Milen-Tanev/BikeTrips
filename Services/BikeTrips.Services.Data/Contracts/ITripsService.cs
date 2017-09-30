using BikeTrips.Data.Models;
using System;
using System.Linq;

namespace BikeTrips.Services.Data.Contracts
{
    public interface ITripsService
    {
        void AddTrip(Trip trip, DateTime tripDate, DateTime tripTime);
        
        Trip GetTripById(int id);

        Trip GetTripById(string id);

        IQueryable Search(string searchString);

    }
}
