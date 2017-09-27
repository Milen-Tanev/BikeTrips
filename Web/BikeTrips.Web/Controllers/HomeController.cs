using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mapping;
using BikeTrips.Web.ViewModels.Home;
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
        
        public ActionResult Index()
        {
            var trips = this.cacheService.Get("trips", () =>
                this.trips.GetComingTrips(5)
                .To<TripViewModel>().ToList(), 15 * 60);

            return this.View(trips);
        }

        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult CreateTrip()
        {
            return this.View();
        }
    }
}