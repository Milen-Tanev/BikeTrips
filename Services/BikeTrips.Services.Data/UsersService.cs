﻿using BikeTrips.Data.Common.Contracts;
using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Utils;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;

namespace BikeTrips.Services.Data
{
    public class UsersService : IUserService
    {
        private IBikeTripsDbRepository<User> users;
        private IUnitOfWork unitOfWork;

        public UsersService()
        {
        }

        public UsersService(IBikeTripsDbRepository<User> users, IUnitOfWork unitOfWork)
        {
            Guard.ThrowIfNull(users, "Users");
            Guard.ThrowIfNull(unitOfWork, "Users");

            this.users = users;
            this.unitOfWork = unitOfWork;
        }

        public User GetCurrentUser()
        {
            var currentUserUsername = HttpContext.Current.User.Identity.GetUserName();
            var currentUser = users.All().Where(u => u.UserName == currentUserUsername).FirstOrDefault();

            return currentUser;
        }
    }
}
