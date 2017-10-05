namespace BikeTrips.Data.Common.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    
    public interface IBikeTripsDbRepository<T>
        where T : class, IDeletable
    {
        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        IQueryable<T> All();

        IQueryable<T> AdminAll();

        T GetById(int id);


        IQueryable<T> Search(Expression<Func<T, bool>> predicate);
    }
}
