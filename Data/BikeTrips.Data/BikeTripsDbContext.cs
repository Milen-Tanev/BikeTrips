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

        public static BikeTripsDbContext Create()
        {
            return new BikeTripsDbContext();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
