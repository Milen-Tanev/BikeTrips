using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.ViewModels.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeTrips.Web.Controllers
{
    public class CommentController : Controller
    {
        private IUserService users;
        private ITripsService trips;
        private ICommentsService comments;
        private IIdentifierProvider identifierProvider;

        public CommentController()
        {
        }

        public CommentController(IUserService users, ITripsService trips, ICommentsService comments, IIdentifierProvider identifierProvider)
        {
            this.users = users;
            this.trips = trips;
            this.comments = comments;
            this.identifierProvider = identifierProvider;
        }

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(CreateCommentViewModel model, string tripUrlId)
        {
            model.Author = this.users.GetCurrentUser();

            var tripId = this.identifierProvider.GetId(tripUrlId);
            var trip = trips.GetTripById(tripId);
            var comment = new Comment { Content = model.Content, LocalTimeOffsetMinutes = model.LocalTimeOffsetMinutes };
            trip.Comments.Add(comment);

            return this.View();
        }
    }
}