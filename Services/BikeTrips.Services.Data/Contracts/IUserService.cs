using BikeTrips.Data.Models;

namespace BikeTrips.Services.Data.Contracts
{
    public interface IUserService
    {
        User GetCurrentUser();
    }
}
