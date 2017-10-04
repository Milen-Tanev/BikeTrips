using System;

namespace BikeTrips.Services.Web.Contracts
{
    public interface IDateTimeConverter
    {
        DateTime Convert(DateTime date, DateTime hours);
    }
}
