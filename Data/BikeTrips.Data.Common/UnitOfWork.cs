namespace BikeTrips.Data.Common
{
    using Contracts;
    using Utils;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BikeTripsDbContext context;

        public UnitOfWork(BikeTripsDbContext context)
        {
            Guard.ThrowIfNull(context, "Context");

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
