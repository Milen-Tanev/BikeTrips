using BikeTrips.Data.Models;

namespace BikeTrips.Services.Data.Contracts
{
    public interface IChatService
    {
        void Add(Comment comment, string tripUrl);
    }
}
