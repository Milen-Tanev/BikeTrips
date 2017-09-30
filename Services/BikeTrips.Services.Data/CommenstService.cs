using System;
using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Common.Contracts;
using BikeTrips.Services.Web.Contracts;

namespace BikeTrips.Services.Data
{
    public class CommenstService : ICommentsService
    {
        private IBikeTripsDbRepository<Comment> comments;
        private IBikeTripsDbRepository<Trip> trips;
        private IIdentifierProvider provider;

        public CommenstService()
        {
        }

        public CommenstService(IBikeTripsDbRepository<Comment> comments, IBikeTripsDbRepository<Trip> trips, IIdentifierProvider provider)
        {
            this.comments = comments;
            this.trips = trips;
            this.provider = provider;
        }

        public void AddComment(Comment comment, string tripUrl)
        {
            var tripId = this.provider.GetId(tripUrl);
            var trip = this.trips.GetById(tripId);
            trip.Comments.Add(comment);
            comment.Subject = trip;
            this.comments.Add(comment);
        }
    }
}
