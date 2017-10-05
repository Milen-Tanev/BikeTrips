namespace BikeTrips.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Common.Contracts;
    
    public class User : IdentityUser, IDeletable
    {
        public User()
        {
            this.AdministeredEvents = new HashSet<Trip>();
            this.VisitedEvents = new HashSet<Trip>();
            this.Comments = new HashSet<Comment>();
        }
          
        public virtual ICollection<Trip> AdministeredEvents { get; set; }

        public virtual ICollection<Trip> VisitedEvents { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
