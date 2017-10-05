using System;
using BikeTrips.Data.Common.Contracts;
using BikeTrips.Utils;

namespace BikeTrips.Data.Common
{
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
