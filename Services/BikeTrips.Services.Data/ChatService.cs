using System;
using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Common.Contracts;
using BikeTrips.Services.Web.Contracts;

namespace BikeTrips.Services.Data
{
    public class ChatService : IChatService
    {
        private IBikeTripsDbRepository<Comment> comments;
        private IBikeTripsDbRepository<Trip> trips;
        private IUserService users;
        private IIdentifierProvider provider;
        private IUnitOfWork unitOfWork;

        public ChatService()
        {
        }

        public ChatService(IBikeTripsDbRepository<Comment> comments,
                                IBikeTripsDbRepository<Trip> trips,
                                IUserService users,
                                IIdentifierProvider provider,
                                IUnitOfWork unitOfWork)
        {
            this.comments = comments;
            this.trips = trips;
            this.users = users;
            this.provider = provider;
            this.unitOfWork = unitOfWork;
        }

        public void AddComment(string content, string tripUrl)
        {
            var tripId = this.provider.GetId(tripUrl);
            var trip = this.trips.GetById(tripId);

            var comment = new Comment
            {
                Author = this.users.GetCurrentUser(),
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
