using BikeTrips.Data.Models;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Web.Infrastructure.Mappings;
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
        private ITripsService trips;
        private ICommentsService comments;
        private IIdentifierProvider identifierProvider;

        public CommentController()
        {
        }

        public CommentController(ITripsService trips, ICommentsService comments)
        {
            this.trips = trips;
            this.comments = comments;
        }

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(CreateCommentViewModel model, string tripUrlId)
        {
            var comment = AutoMapperConfig
                    .Configuration.CreateMapper()
                    .Map<Comment>(model);
            this.comments.AddComment(comment, tripUrlId);
            return this.View();
        }
    }
}