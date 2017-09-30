using BikeTrips.Data.Models;

namespace BikeTrips.Services.Data.Contracts
{
    public interface ICommentsService
    {
        void AddComment(Comment comment, string tripUrl);
    }
}
