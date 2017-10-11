namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    
    using Services.Data.Contracts;
    using Utils;
    using ViewModels;

    public class HomeController : Controller
    {
        private IUserService users;
        private ITripsService trips;
        private ICommentsService comments;

        public HomeController(IUserService users, ITripsService trips, ICommentsService comments)
        {
            Guard.ThrowIfNull(users, "Users service");
            Guard.ThrowIfNull(trips, "Trips service");
            Guard.ThrowIfNull(comments, "Comments service");

            this.users = users;
            this.trips = trips;
            this.comments = comments;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var users = this.users.GetAllAdmin();
            var trips = this.trips.GetAllAdmin();
            var comments = this.comments.GetAllAdmin();

            var allParticipants = (from trip in trips select trip.Participants.Count()).Sum();

            var model = new HomeIndexViewModel
            {
                UsersCount = users.Count(),
                UsersCountNotDeleted = users.Where(u => u.IsDeleted == false).Count(),
                UsersCountDeleted = users.Where(u => u.IsDeleted == true).Count(),
                AverageTripsPerUser = (double)trips.Count() / users.Count(),
                AverageCommentsPerUser = (double)comments.Count() / users.Count(),
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

        //[HttpPost]
        //public PartialViewResult Search(string searchString, int? page)
        //{

        //}

        public ActionResult About()
        {
            return this.View();
        }
    }
}