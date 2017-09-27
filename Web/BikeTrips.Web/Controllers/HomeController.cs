using BikeTrips.Data.Common.Contracts;
using BikeTrips.Data.Models;
using System.Linq;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBikeTripsDbRepository<Trip> trips;
        public HomeController(IBikeTripsDbRepository<Trip> trips)
        {
            this.trips = trips;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var trips = this.trips.All().Where(x => !x.IsPassed).OrderBy(x => x.TripDate).Take(5);

            return View(trips);
        }
    }
}