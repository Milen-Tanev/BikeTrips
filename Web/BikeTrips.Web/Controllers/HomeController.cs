using BikeTrips.Services.Data.Contracts;
using BikeTrips.Web.Infrastructure.Mapping;
using BikeTrips.Web.ViewModels.Home;
using System.Linq;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITripsService trips;

        public HomeController(ITripsService trips)
        {
            this.trips = trips;
        }
        
        public ActionResult Index()
        {
            var trips = this.trips.GetComingTrips(5)
                .To<TripViewModel>().ToList();

            return this.View(trips);
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}