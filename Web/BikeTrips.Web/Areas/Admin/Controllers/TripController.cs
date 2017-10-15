namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Mappings;
    using Services.Data.Contracts;
    using Utils;
    using ViewModels;
    using Web.ViewModels.TripModels;

    [Authorize(Roles = SeedConstants.AdminRoleName)]
    public class TripController : Controller
    {
        private ITripsService trips;
        private IMapper mapper;

        public TripController()
        {
        }

        public TripController(ITripsService trips, IMapper mapper)
        {
            Guard.ThrowIfNull(trips, "Trips");
            Guard.ThrowIfNull(mapper, "Mapper");

            this.trips = trips;
            this.mapper = mapper;
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

            var model = this.trips.GetTripByIdAdmin(urlId);

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var viewModel = this.mapper.Map<FullTripViewModel>(model);
            return View(viewModel);
        }
    }
}