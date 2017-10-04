﻿using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mappings;
using BikeTrips.Web.ViewModels.TripModels;
using Common.Constants;
using System;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class TripController : Controller
    {
        private IUserService users;
        private ITripsService trips;
        private ICacheService cacheService;

        public TripController()
        {
        }

        public TripController
            (
                IUserService users,
                ITripsService trips,
                ICacheService cacheService
            )
        {
            this.users = users;
            this.trips = trips;
            this.cacheService = cacheService;
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

            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<FullTripViewModel>(model);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
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

                var trip = AutoMapperConfig
                    .Configuration.CreateMapper()
                    .Map<Trip>(model);
                this.trips.AddTrip(trip, model.TripDate, model.TripTime);

                this.cacheService.Remove("trips");

                var viewModel = AutoMapperConfig
                    .Configuration.CreateMapper()
                    .Map<FullTripViewModel>(trip);
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

            var viewModel = AutoMapperConfig
                    .Configuration.CreateMapper()
                    .Map<FullTripViewModel>(trip);
            return PartialView("_ButtonsPartial", viewModel);
        }

        [HttpPost]
        public PartialViewResult LeaveTrip(int id)
        {
            var trip = this.trips.GetTripById(id);
            this.trips.RemoveParticipantFrom(trip);
            this.cacheService.Remove("trips");

            var viewModel = AutoMapperConfig
                    .Configuration.CreateMapper()
                    .Map<FullTripViewModel>(trip);

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

