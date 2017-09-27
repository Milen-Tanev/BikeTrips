using BikeTrips.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BikeTrips.Data
{
    public class BikeTripsDbContext : IdentityDbContext<User>
    {
        public BikeTripsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Trip> Trips { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static BikeTripsDbContext Create()
        {
            return new BikeTripsDbContext();
        }
        
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
