namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using Infrastructure.Mappings;
    using Services.Data;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using Utils;
    using ViewModels;

    public class CommentController : Controller
    {
        private ICommentsService comments;
        public CommentController(ICommentsService comments)
        {
            Guard.ThrowIfNull(comments, "Comments service");

            this.comments = comments;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var comments = this.comments.GetAllAdmin();

            var model = comments.To<CommentViewModel>();
            return View(model);
        }
    }
}