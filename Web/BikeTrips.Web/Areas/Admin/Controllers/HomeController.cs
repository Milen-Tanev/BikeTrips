namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using PagedList;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using Services.Web.Contracts;
    using Utils;
    using ViewModels.TripModels;

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
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;

            var trips = this.trips.GetAllAdmin()
                .OrderBy(t => t.TripName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .To<TripViewModel>()
                .ToList();
            var comments = this.comments.GetAllAdmin()
                .OrderBy(c => c.Author)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return this.View(trips);
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