using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeTrips.Data.Models
{
    public class User : IdentityUser
    {
        public User(string userName, UserType type)
        {
            this.UserName = userName;
            this.Role = type;

            this.AdministeredEvents = new List<Trip>();
            this.VisitedEvents = new List<Trip>();
            this.Comments = new List<Comment>();
        }
        

        public UserType Role { get; protected set; }

        public virtual ICollection<Trip> AdministeredEvents { get; set; }

        public virtual ICollection<Trip> VisitedEvents { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public bool IsDeleted { get; protected set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
