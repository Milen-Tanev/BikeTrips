namespace BikeTrips.Services.Data.Contracts
{
    using BikeTrips.Data.Models;
    using System;
    using System.Linq;

    public interface ITripsService
    {
        void AddTrip(Trip trip, DateTime tripDate, DateTime tripTime);
        
        Trip GetTripById(int id);

        Trip GetTripById(string id);

        IQueryable GetAll();

        void AddParticipantTo(Trip trip);

        void RemoveParticipantFrom(Trip trip);

        void DeleteTrip(Trip trip);

        Trip GetTripByName(string tripName);

        IQueryable<Trip> GetAllAdmin();
    }
}
