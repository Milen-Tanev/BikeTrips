using BikeTrips.Data.Common.Contracts;
using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using Microsoft.AspNet.Identity;
using System.Web;

namespace BikeTrips.Services.Data
{
    public class UserService : IUserService
    {
        IBikeTripsDbRepository<User> users;
        public UserService()
        {
        }

        public UserService(IBikeTripsDbRepository<User> users)
        {
            this.users = users;
        }

        public User GetCurrentUser()
        {
            var currentUserId = int.Parse(HttpContext.Current.User.Identity.GetUserId());
            return this.users.GetById(currentUserId);
        }
    }
}
