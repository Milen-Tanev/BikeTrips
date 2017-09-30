using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Models;
using BikeTrips.Data.Common.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace BikeTrips.Services.Data
{
    public class TripsService : ITripsService
    {
        IBikeTripsDbRepository<Trip> trips;
        IUnitOfWork unitOfWork;

        public TripsService()
        {
        }

        public TripsService(IBikeTripsDbRepository<Trip> trips, IUnitOfWork unitOfWork)
        {
            this.trips = trips;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable Search(string sortOrder, string searchString)
        {
            var searchResult = this.trips.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                searchResult = searchResult.Where(t => t.TripName.ToLower().Contains(searchString)
                                                        || t.Creator.Name.ToLower().Contains(searchString)
                                                        || t.StartingPoint.ToLower().Contains(searchString));
            }

            return searchResult;
        }

        public void AddTrip(Trip trip)
        {
            var user = trip.Creator;
            user.AdministeredEvents.Add(trip);
            trip.Participants.Add(user);
            this.trips.Add(trip);
            this.unitOfWork.Commit();
        }

        public IQueryable<Trip> GetComingTrips(int count)
        {
            return this.trips.All()
                .Where(x => !x.IsPassed)
                .OrderBy(x => x.StartingTime)
                .Take(count);
        }

        public Trip GetTripById(int id)
        {
            return this.trips.GetById(id);
        }
    }
}
