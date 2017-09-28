using System;
using BikeTrips.Data.Common.Contracts;

namespace BikeTrips.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BikeTripsDbContext context;

        public UnitOfWork(BikeTripsDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
