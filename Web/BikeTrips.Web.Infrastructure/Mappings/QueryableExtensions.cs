using AutoMapper.QueryableExtensions;
using BikeTrips.Web.Infrastructure.Mappings;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BikeTrips.Web.Infrastructure.Mapping
{


    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}