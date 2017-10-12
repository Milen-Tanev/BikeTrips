namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using Utils;
    using ViewModels;
    using Infrastructure.Mappings;
    using Web.ViewModels.TripModels;

    public class TripController : Controller
    {
        private ITripsService trips;

        public TripController()
        {

        }
        public TripController(ITripsService trips)
        {
            Guard.ThrowIfNull(trips, "Trips");

            this.trips = trips;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var trips = this.trips.GetAllAdmin()
                .OrderBy(u => u.TripName);

            var model = trips.To<TripAdminViewModel>();
            return View(model);
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