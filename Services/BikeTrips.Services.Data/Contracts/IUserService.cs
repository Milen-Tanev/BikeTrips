namespace BikeTrips.Services.Data.Contracts
{
    using System.Linq;

    using BikeTrips.Data.Models;

    public interface IUserService
    {
        User GetCurrentUser();

        IQueryable<User> GetAllAdmin();
    }
}
