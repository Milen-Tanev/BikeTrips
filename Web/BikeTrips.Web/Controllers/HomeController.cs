using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mapping;
using BikeTrips.Web.ViewModels.TripModels;
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
        public ActionResult Index()
        {
            var trips = this.cacheService.Get("trips", () =>
            this.trips.GetAll()
            .To<TripViewModel>().ToList(), 2 * 60 * 60);
            
            return this.View(trips);
        }

        [HttpPost]
        public PartialViewResult Search(string searchString)
        {
            searchString = searchString.ToLower();

            var trips = this.cacheService.Get("trips", () =>
            this.trips.GetAll()
            .To<TripViewModel>().ToList(), 2 * 60 * 60)
            .Where(t => t.StartingPoint.ToLower().Contains(searchString)
                            || t.User.ToLower().Contains(searchString)
                            || t.StartingPoint.ToLower().Contains(searchString));

            return PartialView("_TripsResults", trips);
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}