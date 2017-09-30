using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mapping;
using BikeTrips.Web.ViewModels.TripModels;
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
        
        public ActionResult Index(string sortOrder, string searchString)
        {
            var trips =  this.trips.Search(sortOrder, searchString)
                .To<TripViewModel>()
                .ToList();
            
            return this.View(trips);
        }

        public ActionResult About()
        {
            return this.View();
        }


    }
}