namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using Utils;
    using Infrastructure.Mapping;
    using ViewModels;
    using Infrastructure.Mappings;
    using Web.ViewModels.TripModels;

    public class TripsController : Controller
    {
        private ITripsService trips;

        public TripsController()
        {

        }
        public TripsController(ITripsService trips)
        {
            Guard.ThrowIfNull(trips, "Trips");

            this.trips = trips;
        }
        // GET: Admin/Trips
        public ActionResult Index()
        {
            var trips = this.trips.GetAllAdmin()
                .OrderBy(u => u.TripName).To<TripAdminViewModel>();
            return View(trips);
        }

        [HttpGet]
        public ActionResult ById(string urlId)
        {
            if (urlId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = this.trips.GetTripById(urlId);

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<FullTripViewModel>(model);
            return View(viewModel);
        }
    }
}