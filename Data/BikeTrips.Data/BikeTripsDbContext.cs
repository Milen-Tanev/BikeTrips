using BikeTrips.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
