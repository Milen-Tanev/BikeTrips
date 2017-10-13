namespace BikeTrips.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using BikeTrips.Data.Models;

    public interface ICommentsService
    {
        void AddComment(string content, string tripUrl);

        IQueryable<Comment> GetAllAdmin();

        void DeleteAllComments(ICollection<Comment> comments);
    }
}
