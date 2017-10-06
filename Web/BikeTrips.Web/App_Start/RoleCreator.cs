namespace BikeTrips.Web.App_Start
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    
    using Data.Models;

    public class RoleCreator // BikeTripsDbContext context
    {
        private DbContext context;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;

        public RoleCreator()
        {
        }

        public RoleCreator(DbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.context));
            var UserManager = new UserManager<User>(new UserStore<User>(this.context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }

    }
}