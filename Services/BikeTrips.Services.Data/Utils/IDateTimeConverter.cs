namespace BikeTrips.Services.Web.Contracts
{
    using System;

    public interface IDateTimeConverter
    {
        DateTime Convert(DateTime date, DateTime hours);
    }
}
