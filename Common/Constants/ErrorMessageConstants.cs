namespace Common.Constants
{
    public class ErrorMessageConstants
    {
        public const string InvalidPassordLength = "The {0} must be at least {2} characters long.";
        public const string PasswordConfirmationDoesNotMatch = "The password and confirmation password do not match.";

        public const string InvalidTripNameLengthErrorMessage = "The {0} must be between {2} and {1} characters long.";
        public const string InvalidTripStartingPointLengthErrorMessage = "The {0} cannot be mode than {1} characters long.";

        public const string TripNameAlreadyExists = "Trip name already exists. Please, choose another trip name.";
        public const string TripDateInThePast = "The date of the trip cannot be in the past.";

        public const string NullError = "{0} cannot be null.";

        public const string NotCreator = "You cannot delete a trip if you are not it's creator.";

        public const string TripHasPassed = "Starting time cannot be less than the current time.";

        public const string  UserNotLoggedIn = "User not logged in.";
    }
}
