using BikeTrips.Data.Common.Contracts;
using BikeTrips.Data.Models;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IBikeTripsDbRepository<Trip> trips)
        {
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}