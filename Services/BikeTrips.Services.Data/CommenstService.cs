using System;
using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Data.Common.Contracts;

namespace BikeTrips.Services.Data
{
    public class CommenstService : ICommentsService
    {
        private IBikeTripsDbRepository<Comment> comments;

        public CommenstService()
        {
        }

        public CommenstService(IBikeTripsDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);
        }
    }
}
