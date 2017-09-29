﻿using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Utils.Contracts;
using BikeTrips.Web.Infrastructure.Mappings;
using BikeTrips.Web.ViewModels.Home;
using System;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class TripController : Controller
    {
        private IUserService users;
        private ITripsService trips;
        private IDateTimeConverter converter;

        public TripController()
        {

        }

        public TripController(IUserService users, ITripsService trips, IDateTimeConverter converter)
        {
            this.users = users;
            this.trips = trips;
            this.converter = converter;
        }

        //[HttpGet]
        public ActionResult Index(int id)
        {
            var model = this.trips.GetTripById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<FullTripViewModel>(model);
            return View(viewModel);
        }

        //[ValidateAntiForgeryToken]
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
                model.Creator = this.users.GetCurrentUser();

                var trip = new Trip
                {
                    TripName = model.TripName,
                    StartingPoint = model.StartingPoint,
                    Type = model.Type,
                    Creator = this.users.GetCurrentUser(),
                    StartingTime = converter.Convert(model.TripDate, model.TripTime),
                    Distance = model.Distance,
                    Denivelation = model.Denivelation,
                    Description = model.Description,
                    LocalTimeOffsetMinutes = model.LocalTimeOffsetMinutes,
                    IsPassed = false,
                    IsDeleted = false
                };

                this.trips.AddTrip(trip);
                return RedirectToAction("Index" ,"Trip", new { id = trip.Id });
            }
            
            return View();
            
        }
    }
}