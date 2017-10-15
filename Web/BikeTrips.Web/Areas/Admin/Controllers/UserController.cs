namespace BikeTrips.Web.Areas.Admin.Controllers
{
    using AutoMapper;
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

    [Authorize(Roles = SeedConstants.AdminRoleName)]
    public class UserController : Controller
    {
        private IUserService users;
        private ApplicationUserManager userManager;
        private IMapper mapper;

        public UserController(IUserService users, ApplicationUserManager userManager, IMapper mapper)
        {
            Guard.ThrowIfNull(users, "Users");
            Guard.ThrowIfNull(userManager, "User manager");
            Guard.ThrowIfNull(mapper, "Mapper");

            this.users = users;
            this.UserManager = userManager;
            this.mapper = mapper;
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

        [HttpGet]
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

            var viewModel = this.mapper.Map<UserViewModel>(model);
            return View(viewModel);
        }
    }
}