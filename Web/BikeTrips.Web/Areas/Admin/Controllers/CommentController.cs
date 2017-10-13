namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Mappings;
    using Services.Data.Contracts;
    using Utils;
    using ViewModels;

    [Authorize(Roles = SeedConstants.AdminRoleName)]
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