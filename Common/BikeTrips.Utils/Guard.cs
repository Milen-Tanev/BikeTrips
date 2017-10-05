using Common.Constants;
using System;

namespace BikeTrips.Utils
{
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
    }
}
