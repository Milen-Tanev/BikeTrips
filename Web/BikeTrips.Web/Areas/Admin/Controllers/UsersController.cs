using BikeTrips.Services.Data.Contracts;
using BikeTrips.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeTrips.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private IUserService users;
        public UsersController(IUserService users)
        {
            Guard.ThrowIfNull(users, "Users");

            this.users = users;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = this.users.GetAllAdmin()
                .OrderBy(u => u.UserName);
            return View(users);
        }
    }
}