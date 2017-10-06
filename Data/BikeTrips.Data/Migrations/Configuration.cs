namespace BikeTrips.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    using global::Common.Constants;
    using Models;
    
    public sealed class Configuration : DbMigrationsConfiguration<BikeTripsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BikeTripsDbContext context)
        {
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
            var user = new User
            {
                UserName = SeedConstants.AdminUserName,
                Email = SeedConstants.AdminEmail,
                EmailConfirmed = true
            };

            userManager.Create(user, SeedConstants.AdminPassowrd);
            userManager.AddToRole(user.Id, adminRoleName);
        }
    }
}
