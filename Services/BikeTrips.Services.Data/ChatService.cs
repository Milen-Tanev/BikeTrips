using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Common.Contracts;
using BikeTrips.Services.Web.Contracts;
using System;
using BikeTrips.Utils;

namespace BikeTrips.Services.Data
{
    public class ChatService : IChatService
    {
        private IBikeTripsDbRepository<Comment> comments;
        private IBikeTripsDbRepository<Trip> trips;
        private IUserService users;
        private IIdentifierProvider identifierProvider;
        private IUnitOfWork unitOfWork;

        public ChatService()
        {
        }

        public ChatService(IBikeTripsDbRepository<Comment> comments,
                                IBikeTripsDbRepository<Trip> trips,
                                IUserService users,
                                IIdentifierProvider identifierProvider,
                                IUnitOfWork unitOfWork)
        {
            Guard.ThrowIfNull(comments, "Comments");
            Guard.ThrowIfNull(trips, "Trips");
            Guard.ThrowIfNull(users, "Users");
            Guard.ThrowIfNull(identifierProvider, "Identifier provider");
            Guard.ThrowIfNull(unitOfWork, "Unif of work");

            this.comments = comments;
            this.trips = trips;
            this.users = users;
            this.identifierProvider = identifierProvider;
            this.unitOfWork = unitOfWork;
        }

        public void AddComment(string content, string tripUrl)
        {
            var tripId = this.identifierProvider.GetId(tripUrl);
            var trip = this.trips.GetById(tripId);
            var author = this.users.GetCurrentUser();
            if (trip == null)
            {
                throw new ArgumentException("No trip with such Id exists");
            }

            if (author == null)
            {
                throw new ArgumentException("The author of the comment cannot be null");
            }

            var comment = new Comment
            {
                Author = author,
                Content = content,
                Subject = trip,
                LocalTimeOffsetMinutes = trip.LocalTimeOffsetMinutes
            };

            trip.Comments.Add(comment);
            comment.Subject = trip;
            this.comments.Add(comment);
            this.unitOfWork.Commit();
        }
    }
}
