namespace BikeTrips.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using global::Common.Constants;
    using Models;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BikeTripsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BikeTripsDbContext context)
        {
            // Create admin
            var userRoleName = SeedConstants.UserRoleName;
            var adminRoleName = SeedConstants.AdminRoleName;
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userRole = new IdentityRole { Name = userRoleName };
            roleManager.Create(userRole);

            var adminRole = new IdentityRole { Name = adminRoleName };
            roleManager.Create(adminRole);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var adminUser = new User
            {
                UserName = SeedConstants.AdminUserName,
                Email = SeedConstants.AdminEmail,
                EmailConfirmed = true
            };

            userManager.Create(adminUser, SeedConstants.AdminPassowrd);
            userManager.AddToRole(adminUser.Id, adminRoleName);

            // Seed users
            var userNames = new List<string>() { "Ivan", "Pesho", "User1", "Minka", "User6237", "Atanas", "Niki" };
            var isDeleted = new List<bool>() { false, true, false, false, false, true, false };
            var users = new List<User>();
            for (int i = 0; i < userNames.Count; i++)
            {
                var user = new User
                {
                    UserName = userNames[i],
                    Email = $"{userNames[i]}@user.com",
                    EmailConfirmed = true,
                    IsDeleted = false
                };
                users.Add(user);
                userManager.Create(user, SeedConstants.DefaultPassword);
                userManager.AddToRole(user.Id, userRoleName);
            }
            var trips = new List<Trip>();

            SeedTrips(userNames, users, isDeleted, context, trips);
            SeedComments(users, trips, isDeleted, userManager, context);
        }

        private void SeedTrips(List<string> userNames, List<User> users, List<bool> isDeleted, BikeTripsDbContext context, List<Trip> trips)
        {
            var tripNames = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                tripNames.Add($"Trip{i}");
            }

            for (int i = 0; i < userNames.Count; i++)
            {
                tripNames.Add($"{userNames[i]}'s event");
            }

            var cities = new List<string>() { "Sofia", "Plovdiv", "Varna", "Burgas", "Pleven", "Russe", "London", "Moscow", "Prague", "Parais", "Amsterdam", "Lyulin", "Ihtiman", "New york", "Vitosha" };

            for (int i = 0; i < cities.Count; i++)
            {
                tripNames.Add($"Bikes in {cities[i]}");
            }
            var random = new Random();
            var types = Enum.GetValues(typeof(TripType));
            DateTime start = DateTime.Now.AddHours(30);
            DateTime end = new DateTime(2020, 12, 12, 8, 8, 8);
            int range = (end - start).Hours;

            for (int i = 0; i < tripNames.Count; i++)
            {
                var userIndex = random.Next(users.Count - 1);
                var user = users[userIndex];
                if (user.IsDeleted == true)
                {
                    if (userIndex < users.Count - 1)
                    {
                        user = users[userIndex + 1];
                    }
                    else
                    {
                        user = users[userIndex - 1];
                    }
                }

                var trip = new Trip
                {
                    TripName = tripNames[i],
                    Creator = user,
                    Denivelation = random.Next(5, 1000),
                    Distance = random.Next(10, 1000),
                    Type = (TripType)types.GetValue(random.Next(types.Length)),
                    StartingPoint = cities[random.Next(cities.Count - 1)],
                    StartingTime = start.AddHours(random.Next(0, range)),
                    LocalTimeOffsetMinutes = (short)random.Next(-300, 300),
                    IsDeleted = false,
                    Description = "Meaningful description"
                };
                var max = random.Next(users.Count - 1);
                for (int j = 0; j < max; j++)
                {
                    trip.Participants.Add(users[random.Next(users.Count - 1)]);
                }
                trips.Add(trip);
            }
            context.Trips.AddOrUpdate(trips.ToArray());
            context.SaveChanges();
        }

        private void SeedComments(List<User> users, List<Trip> trips, List<bool> isDeleted, UserManager<User> userManager, BikeTripsDbContext context)
        {
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var userIndex = random.Next(users.Count - 1);
                var user = users[userIndex];
                if (user.IsDeleted == true)
                {
                    if (userIndex < users.Count - 1)
                    {
                        user = users[userIndex + 1];
                    }
                    else
                    {
                        user = users[userIndex - 1];
                    }
                }
                var dbUsers = context.Users;
                var us = dbUsers.Where(x => x.UserName == user.UserName).First();

                var tripIndex = random.Next(trips.Count - 1);
                var trip = trips[tripIndex];
                if (trip.IsDeleted == true)
                {
                    if (tripIndex < trips.Count - 1)
                    {
                        trip = trips[tripIndex + 1];
                    }
                    else
                    {
                        trip = trips[tripIndex - 1];
                    }
                }
                var dbTrips = context.Trips;
                var tr = dbTrips.Where(x => x.TripName == trip.TripName).First();

                var comment = new Comment()
                {
                    Content = $"Comment {i}",
                    Author = us,
                    LocalTimeOffsetMinutes = (short)random.Next(-300, 300),
                    Subject = tr,
                    IsDeleted = false
                };
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }
    }
}
