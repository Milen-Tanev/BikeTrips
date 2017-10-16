namespace BikeTrips.Web.Controllers
{
    using AutoMapper;
    using System;
    using System.Web.Mvc;

    using Common.Constants;
    using Data.Models;
    using Services.Data.Contracts;
    using Services.Web.Contracts;
    using Utils;
    using ViewModels.TripModels;

    public class TripController : Controller
    {
        private IUserService users;
        private ITripsService trips;
        private ICacheService cacheService;
        private IMapper mapper;

        public TripController()
        {
        }

        public TripController
            (
                IUserService users,
                ITripsService trips,
                ICacheService cacheService,
                IMapper mapper
            )
        {
            Guard.ThrowIfNull(users, "Users service");
            Guard.ThrowIfNull(trips, "Trips service");
            Guard.ThrowIfNull(cacheService, "HTTP cache service");
            Guard.ThrowIfNull(mapper, "Mapper");

            this.users = users;
            this.trips = trips;
            this.cacheService = cacheService;
            this.mapper = mapper;
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

            if (model.StartingTime.AddMinutes(model.LocalTimeOffsetMinutes) < DateTime.UtcNow)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = this.mapper.Map<FullTripViewModel>(model);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = SeedConstants.UserRoleName)]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = SeedConstants.UserRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTripViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Description == null)
                {
                    model.Description = UiMessageConstants.NoTripDescription;
                }

                if (this.trips.GetTripByName(model.TripName) != null)
                {
                    ModelState.AddModelError("TripName", ErrorMessageConstants.TripNameAlreadyExists);
                    return View(model);
                }

                if (model.TripDate < DateTime.UtcNow.AddMinutes(model.LocalTimeOffsetMinutes))
                {
                    ModelState.AddModelError("TripDate", ErrorMessageConstants.TripDateInThePast);
                    return View(model);
                }

                var trip = this.mapper.Map<Trip>(model);
                this.trips.AddTrip(trip, model.TripDate, model.TripTime);

                this.cacheService.Remove("trips");

                var viewModel = this.mapper.Map<FullTripViewModel>(trip);
                return RedirectToAction("ById", new { urlId = viewModel.UrlId });
            }

            return View();
        }

        [HttpPost]
        public PartialViewResult JoinTrip(int id)
        {
            var trip = this.trips.GetTripById(id);
            this.trips.AddParticipantTo(trip);
            this.cacheService.Remove("trips");

            var viewModel = this.mapper.Map<FullTripViewModel>(trip);
            return PartialView("_ButtonsPartial", viewModel);
        }

        [HttpPost]
        public PartialViewResult LeaveTrip(int id)
        {
            var trip = this.trips.GetTripById(id);
            this.trips.LeaveTrip(trip);
            this.cacheService.Remove("trips");

            var viewModel = this.mapper.Map<FullTripViewModel>(trip);
            return PartialView("_ButtonsPartial", viewModel);
        }

        [HttpPost]
        public ActionResult DeleteTrip(int id)
        {
            var trip = this.trips.GetTripById(id);
            this.trips.DeleteTrip(trip);
            this.cacheService.Remove("trips");

            return JavaScript("window.location = '/Home/Index'");
        }
    }
}
