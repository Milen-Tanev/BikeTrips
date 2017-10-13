namespace BikeTrips.Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Mappings;
    using Services.Data.Contracts;
    using Utils;
    using ViewModels.UserModels;

    public class UsersController : Controller
    {
        private IUserService users;

        public UsersController(IUserService users)
        {
            Guard.ThrowIfNull(users, "Users");

            this.users = users;
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

            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<UserViewModel>(model);
            return View(viewModel);
        }
    }
}