namespace BikeTrips.Web.Infrastructure.Mapping
{
    using AutoMapper.QueryableExtensions;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Mappings;

    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }   
}