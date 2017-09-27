using BikeTrips.Data.Common.Contracts;
using Common.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeTrips.Data.Models
{
    public class User : IdentityUser, IDeletable
    {
        public User()
        {
            this.AdministeredEvents = new LinkedList<Trip>();
            this.VisitedEvents = new List<Trip>();
            this.Comments = new List<Comment>();
        }

        [Required]
        [MaxLength(CommonStringLengthConstants.StandardMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string Name { get; set; }
        
        public virtual ICollection<Trip> AdministeredEvents { get; set; }

        public virtual ICollection<Trip> VisitedEvents { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

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
