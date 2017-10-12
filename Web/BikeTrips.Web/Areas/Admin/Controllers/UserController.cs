namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNet.Identity.Owin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web;

    using Common.Constants;
    using Data.Models;
    using Services.Data.Contracts;
    using Utils;
    using Infrastructure.Mappings;
    using Web.ViewModels.UserModels;

    public class UserController : Controller
    {
        private IUserService users;
        private ApplicationUserManager userManager;

        public UserController(IUserService users, ApplicationUserManager userManager)
        {
            Guard.ThrowIfNull(users, "Users");
            Guard.ThrowIfNull(userManager, "User manager");

            this.users = users;
            this.UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }

        // GET: Admin/Users
        public async Task<ActionResult> Index()
        {
            var users = this.users.GetAllUsersAdmin();

            var usersInUserRole = new List<User>();
            foreach (var user in users)
            {
                if (await this.UserManager.IsInRoleAsync(user.Id, SeedConstants.UserRoleName))
                {
                    usersInUserRole.Add(user);
                }
            }

            var model = usersInUserRole.AsQueryable().To<UserViewModel>();
                        
            return View(model);
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