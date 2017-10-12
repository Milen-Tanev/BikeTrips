namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;

    using Common.Constants;
    using Services.Data.Contracts;
    using System.Web;
    using Utils;
    using ViewModels;
    using System.Threading.Tasks;
    using Data.Models;
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        private IUserService users;
        private ITripsService trips;
        private ICommentsService comments;
        private ApplicationUserManager userManager;

        public HomeController(
            IUserService users,
            ITripsService trips,
            ICommentsService comments,
            ApplicationUserManager userManager)
        {
            Guard.ThrowIfNull(users, "Users service");
            Guard.ThrowIfNull(trips, "Trips service");
            Guard.ThrowIfNull(comments, "Comments service");
            Guard.ThrowIfNull(userManager, "User manager");

            this.users = users;
            this.trips = trips;
            this.comments = comments;
            this.UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var users = this.users.GetAllUsersAdmin().ToList();
            var trips = this.trips.GetAllAdmin();
            var comments = this.comments.GetAllAdmin();
            var usersInUserRole = new List<User>();
            foreach (var user in users)
            {
                if (await this.UserManager.IsInRoleAsync(user.Id, SeedConstants.UserRoleName))
                {
                    usersInUserRole.Add(user);
                }
            }

            var allParticipants = (from trip in trips select trip.Participants.Count()).Sum();

            var model = new HomeIndexViewModel
            {
                UsersCount = usersInUserRole.Count(),
                UsersCountNotDeleted = usersInUserRole.Where(u => u.IsDeleted == false).Count(),
                UsersCountDeleted = usersInUserRole.Where(u => u.IsDeleted == true).Count(),
                AverageTripsPerUser = (double)trips.Count() / usersInUserRole.Count(),
                AverageCommentsPerUser = (double)comments.Count() / usersInUserRole.Count(),
                TripsCount = trips.Count(),
                TripsCountNotDeleted = trips.Where(t => t.IsDeleted == false).Count(),
                TripsCountDeleted = trips.Where(t => t.IsDeleted == true).Count(),
                AverageParticipantsInTrip = (double)allParticipants / trips.Count(),
                AverageCommentsPerTrip = (double)comments.Count() / trips.Count(),
                CommentsCount = comments.Count(),
                CommentsCountNotDeleted = comments.Where(c => c.IsDeleted == false).Count(),
                CommentsCountDeleted = comments.Where(c => c.IsDeleted == true).Count(),
            };

            return this.View(model);
        }
        
        public ActionResult About()
        {
            return this.View();
        }
    }
}