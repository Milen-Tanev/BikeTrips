namespace BikeTrips.Services.Data
{
    using System.Linq;

    using BikeTrips.Data.Models;
    using BikeTrips.Data.Common.Contracts;
    using Contracts;
    using Utils;
    using Web.Contracts;

    public class CommentsService : ICommentsService
    {
        private IBikeTripsDbRepository<Comment> comments;
        private IBikeTripsDbRepository<Trip> trips;
        private IUserService users;
        private IIdentifierProvider identifierProvider;
        private IUnitOfWork unitOfWork;

        public CommentsService()
        {
        }

        public CommentsService(IBikeTripsDbRepository<Comment> comments,
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
            Guard.ThrowIfNull(content, "Comment content");
            Guard.ThrowIfNull(tripUrl, "Subject url");

            var tripId = this.identifierProvider.GetId(tripUrl);
            var trip = this.trips.GetById(tripId);
            var author = this.users.GetCurrentUser();

            Guard.ThrowIfNull(trip, "Trip");
            Guard.ThrowIfNull(author, "Author");

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

        public IQueryable<Comment> GetAllAdmin()
        {
            var comments = this.comments.AdminAll();

            return comments;
        }
    }
}
