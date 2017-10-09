namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using Utils;

    public class TripsController : Controller
    {
        private ITripsService trips;
        public TripsController(ITripsService trips)
        {
            Guard.ThrowIfNull(trips, "Trips");

            this.trips = trips;
        }
        // GET: Admin/Trips
        public ActionResult Index()
        {
            var trips = this.trips.GetAllAdmin()
                .OrderBy(u => u.TripName);
            return View(trips);
        }
    }
}