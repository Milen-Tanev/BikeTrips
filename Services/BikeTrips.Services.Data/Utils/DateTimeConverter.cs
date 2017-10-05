namespace BikeTrips.Services.Web
{
    using System;

    using Contracts;
    using Utils;

    public class DateTimeConverter : IDateTimeConverter
    {
        public DateTime Convert(DateTime date, DateTime time)
        {
            Guard.ThrowIfNull(date, "Date");
            Guard.ThrowIfNull(time, "Time");

            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            int hour = time.Hour;
            int minute = time.Minute;
            int second = time.Second;

            return new DateTime(year, month, day, hour, minute, second, 0, DateTimeKind.Unspecified);
        }
    }
}
