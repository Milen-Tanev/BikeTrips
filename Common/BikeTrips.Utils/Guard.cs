namespace BikeTrips.Utils
{
    using System;

    using Common.Constants;

    public static class Guard
    {
        public static void ThrowIfNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                var message = string.Format(ErrorMessageConstants.NullError, argumentName);
                throw new ArgumentNullException(ErrorMessageConstants.NullError, argumentName);
            }
        }

        public static void ThrowIfDifferent(object firstElement, object secondElement, string errorText)
        {
            if (firstElement != secondElement)
            {
                throw new UnauthorizedAccessException(errorText);
            }
        }

        public static void ThrowIfDateEarlier(DateTime firstDate, DateTime secondDate, string errorText)
        {
            if (firstDate < secondDate)
            {
                throw new ArgumentException(errorText);
            }
        }
    }
}
