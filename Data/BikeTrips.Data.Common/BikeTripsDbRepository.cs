namespace BikeTrips.Data.Common.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Utils;

    public class BikeTripsDbRepository<T> : IBikeTripsDbRepository<T>
        where T : class, IDeletable
    {
        public BikeTripsDbRepository(DbContext context)
        {
            Guard.ThrowIfNull(context, "Context");

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public DbContext Context { get; }

        public DbSet<T> DbSet { get; }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var e = this.DbSet.Find(entity);

            if(!e.IsDeleted)
            {
                var entry = Context.Entry(entity);
                entry.State = EntityState.Modified;
            }
        }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AdminAll()
        {
            return this.DbSet;
        }
        
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public T GetById(int id)
        {
            var entity = this.DbSet.Find(id);
            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        public T GetById(string id)
        {
            var entity = this.DbSet.Find(id);
            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate)
                .Where(x => !x.IsDeleted);
        }
    }
}
