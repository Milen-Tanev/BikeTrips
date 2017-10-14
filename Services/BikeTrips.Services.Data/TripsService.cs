namespace BikeTrips.Services.Data
{
    using System;
    using System.Linq;

    using BikeTrips.Data.Models;
    using BikeTrips.Data.Common.Contracts;
    using Common.Constants;
    using Contracts;
    using Utils;
    using Web.Contracts;

    public class TripsService : ITripsService
    {
        private IBikeTripsDbRepository<Trip> trips;
        private IUserService users;
        private ICommentsService comments;
        private IUnitOfWork unitOfWork;
        private IDateTimeConverter converter;
        private IIdentifierProvider identifierProvider;

        public TripsService()
        {
        }

        public TripsService(
            IBikeTripsDbRepository<Trip> trips,
            IUserService users,
            ICommentsService comments,
            IUnitOfWork unitOfWork,
            IDateTimeConverter converter, 
            IIdentifierProvider identifierProvider)
        {
            Guard.ThrowIfNull(trips, "Trips");
            Guard.ThrowIfNull(users, "Users");
            Guard.ThrowIfNull(comments, "Comments");
            Guard.ThrowIfNull(unitOfWork, "Unif of work");
            Guard.ThrowIfNull(converter, "Converter");
            Guard.ThrowIfNull(identifierProvider, "Identifier provider");

            this.trips = trips;
            this.users = users;
            this.comments = comments;
            this.unitOfWork = unitOfWork;
            this.converter = converter;
            this.identifierProvider = identifierProvider;
        }

        public IQueryable GetAll()
        {
            var allTrips = this.trips.All()
                .OrderBy(t => t.StartingTime);
            
            return allTrips;
        }
        
        public void AddTrip(Trip trip, DateTime tripDate, DateTime tripTime)
        {
            var startingTime = converter.Convert(tripDate, tripTime);
            var currentUserTime = DateTime.UtcNow.AddMinutes(trip.LocalTimeOffsetMinutes);

            Guard.ThrowIfDateEarlier(startingTime, currentUserTime, ErrorMessageConstants.TripHasPassed);

            trip.StartingTime = startingTime;
            var user = this.users.GetCurrentUser();
            user.AdministeredEvents.Add(trip);
            trip.Creator = user;
            trip.Participants.Add(user);
            this.trips.Add(trip);
            this.unitOfWork.Commit();
        }

        public void AddParticipantTo(Trip trip)
        {
            var user = this.users.GetCurrentUser();
            Guard.ThrowIfNull(user, "User");

            trip.Participants.Add(user);
            this.unitOfWork.Commit();
        }

        public void LeaveTrip(Trip trip)
        {
            var user = this.users.GetCurrentUser();
            trip.Participants.Remove(user);
            this.unitOfWork.Commit();
        }

        public void DeleteTrip(Trip trip)
        {
            var user = this.users.GetCurrentUser();
            Guard.ThrowIfDifferent(user, trip.Creator, ErrorMessageConstants.NotCreator);

            var comments = trip.Comments;
            if (comments.Count() > 0)
            {
                this.comments.DeleteAllComments(comments);
            }

            trip.IsDeleted = true;
            this.unitOfWork.Commit();
        }

        public Trip GetTripById(int id)
        {
            var trip = this.trips.GetById(id);
            Guard.ThrowIfNull(trip, "Trip");

            return trip;
        }

        public Trip GetTripById(string urlId)
        {
            var id = this.identifierProvider.GetId(urlId);
            var trip = this.trips.GetById(id);
            Guard.ThrowIfNull(trip, "Trip");

            return trip;
        }

        public Trip GetTripByIdAdmin(string urlId)
        {
            var id = this.identifierProvider.GetId(urlId);
            var trip = this.trips.GetByIdWithDeleted(id);
            Guard.ThrowIfNull(trip, "Trip");

            return trip;
        }

        public Trip GetTripByName(string tripName)
        {
            var trip = this.trips.All()
                .Where(t => t.TripName == tripName).FirstOrDefault();
            return trip;
        }

        public IQueryable<Trip> GetAllAdmin()
        {
            var trips = this.trips.AdminAll()
                .OrderBy(t => t.StartingTime);

            return trips;
        }
    }
}
