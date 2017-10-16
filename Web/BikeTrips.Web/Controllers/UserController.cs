namespace BikeTrips.Web.Controllers
{
    using AutoMapper;
    using System.Web.Mvc;
    
    using Services.Data.Contracts;
    using Utils;
    using ViewModels.UserModels;

    public class UserController : Controller
    {
        private IUserService users;
        private IMapper mapper;
        
        public UserController(IUserService users, IMapper mapper)
        {
            Guard.ThrowIfNull(users, "Users");
            Guard.ThrowIfNull(mapper, "Mapper");

            this.users = users;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult ById(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = this.users.GetUserById(id);

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = this.mapper
                .Map<UserViewModel>(model);
            return View(viewModel);
        }

        private string notification;
        protected void AddNotification(string message)
        {
            this.notification = message;
        }
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(this.notification)) filterContext.Controller.ViewData.Add("PageLoadNotification", this.notification);
        }
    }
}
