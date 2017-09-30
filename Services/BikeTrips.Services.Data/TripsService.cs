using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Models;
using BikeTrips.Data.Common.Contracts;
using System.Linq;
using System;
using BikeTrips.Services.Web.Contracts;

namespace BikeTrips.Services.Data
{
    public class TripsService : ITripsService
    {
        private IBikeTripsDbRepository<Trip> trips;
        private IUserService users;
        private IUnitOfWork unitOfWork;
        private IDateTimeConverter converter;
        private IIdentifierProvider identifierProvider;

        public TripsService()
        {
        }

        public TripsService(IBikeTripsDbRepository<Trip> trips, IUserService users, IUnitOfWork unitOfWork, IDateTimeConverter converter, IIdentifierProvider identifierProvider)
        {
            this.trips = trips;
            this.users = users;
            this.unitOfWork = unitOfWork;
            this.converter = converter;
            this.identifierProvider = identifierProvider;
        }

        public IQueryable Search(string searchString)
        {
            var searchResult = this.trips.All()
                .Where(x => x.IsDeleted == false)
                .OrderBy(t => t.StartingTime);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                searchResult = searchResult.Where(t => t.TripName.ToLower().Contains(searchString)
                                                    || t.Creator.Name.ToLower().Contains(searchString)
                                                    || t.StartingPoint.ToLower().Contains(searchString))
                                                    .OrderBy(t => t.StartingTime);
            }

            return searchResult;
        }

        public void AddTrip(Trip trip, DateTime tripDate, DateTime tripTime)
        {
            if (trip.StartingTime < DateTime.UtcNow)
            {
                throw new ArgumentException("Starting time cannot be les than the current time!");
            }
            trip.StartingTime = converter.Convert(tripDate, tripTime);
            var user = this.users.GetCurrentUser();
            user.AdministeredEvents.Add(trip);
            trip.Creator = user;
            trip.Participants.Add(user);
            this.trips.Add(trip);
            this.unitOfWork.Commit();
        }

        public Trip GetTripById(int id)
        {
            return this.trips.GetById(id);
        }

        public Trip GetTripById(string urlId)
        {
            var id = this.identifierProvider.GetId(urlId);
            return this.trips.GetById(id);
        }
    }
}
