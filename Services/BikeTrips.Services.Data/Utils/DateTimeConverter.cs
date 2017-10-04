using BikeTrips.Services.Web.Contracts;
using System;

namespace BikeTrips.Services.Web
{
    public class DateTimeConverter : IDateTimeConverter
    {
        public DateTime Convert(DateTime date, DateTime time)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            int hour = time.Hour;
            int minute = time.Minute;
            int second = time.Second;

            return new DateTime(year, month, day, hour, minute, second, 0,DateTimeKind.Unspecified);
        }
    }
}
