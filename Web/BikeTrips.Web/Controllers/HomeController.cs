using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mapping;
using BikeTrips.Web.ViewModels.TripModels;
using Common.Constants;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITripsService trips;
        private ICacheService cacheService;

        public HomeController(ITripsService trips, ICacheService cacheService)
        {
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