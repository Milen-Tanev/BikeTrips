using BikeTrips.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BikeTrips.Data
{
    public class BikeTripsDbContext : IdentityDbContext<User>
    {
        public BikeTripsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<BikeTripsDbContext>(new CreateDatabaseIfNotExists<BikeTripsDbContext>());
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<Trip>()
                .HasMany<User>(t => t.Participants)
                .WithMany(u => u.VisitedEvents)
                .Map(cs =>
                {
                    cs.MapLeftKey("TripId");
                    cs.MapRightKey("UserId");
                    cs.ToTable("UsersTrips");
                });
        }
    }
}
