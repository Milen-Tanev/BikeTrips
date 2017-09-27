using BikeTrips.Data.Models;
using BikeTrips.Services.Data;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Web.ViewModels.Home;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class TripController : Controller
    {
        private IUserService users;

        public TripController()
        {

        }
        public TripController(IUserService users)
        {
            this.users = users;
        }

        // GET: Trip
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    TripName = model.TripName,
                    StartingPoint = model.StartingPoint,
                    Type = model.Type,
                    StartingTime = DateTime.Parse(model.TripDate), // model.TripTime
                    Distance = model.Distance,
                    Denivelation = model.Denivelation,
                    Description = model.Description,
                    LocalTimeOffsetMinutes = 0,
                    Creator = this.users.GetCurrentUser(),
                    IsPassed = false,
                    IsDeleted = false
                };
                return View();
            }
            
            return View();

        }

        // If we got this far, something failed, redisplay form
    }
}