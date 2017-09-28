using System;

namespace BikeTrips.Services.Utils.Contracts
{
    public interface IDateTimeConverter
    {
        DateTime Convert(DateTime date, DateTime hours);
    }
}
