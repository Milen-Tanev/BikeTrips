namespace BikeTrips.Web.Controllers
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
        private ITripsService trips;
        private ICacheService cacheService;

        public HomeController(ITripsService trips, ICacheService cacheService)
        {
            Guard.ThrowIfNull(trips, "Trips service");
            Guard.ThrowIfNull(cacheService, "HTTP cache service");

            this.trips = trips;
            this.cacheService = cacheService;
        }
        
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var trips = this.cacheService.Get("trips", () =>
            this.trips.GetAll()
            .To<TripViewModel>().ToList()
            .Where(t => t.StartingTime.AddMinutes(t.LocalTimeOffsetMinutes) > DateTime.UtcNow),
            CommonNumericConstants.TwoHoursSpan);
            var pageNumber = page ?? 1;
            var onePageOfTrips = trips.ToPagedList(pageNumber, 5);
            
            return this.View(onePageOfTrips);
        }

        [HttpPost]
        public PartialViewResult Search(string searchString, int? page)
        {
            searchString = searchString.ToLower();

            var trips = this.cacheService.Get("trips", () =>
            this.trips.GetAll()
            .To<TripViewModel>().ToList()
            .Where(t => t.StartingTime.AddMinutes(t.LocalTimeOffsetMinutes) > DateTime.UtcNow),
            CommonNumericConstants.TwoHoursSpan)
            .Where(t => t.StartingPoint.ToLower().Contains(searchString)
                            || t.User.ToLower().Contains(searchString)
                            || t.TripName.ToLower().Contains(searchString));
            
            var pageNumber = page ?? 1;
            var onePageOfTrips = trips.ToPagedList(pageNumber, 5);

            return PartialView("_TripsResults", onePageOfTrips);
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}