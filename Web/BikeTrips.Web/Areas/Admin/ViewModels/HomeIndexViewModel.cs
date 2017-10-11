namespace BikeTrips.Web.Areas.Admin.ViewModels
{
    public class HomeIndexViewModel
    {
        public int UsersCount { get; set; }

        public int UsersCountNotDeleted { get; set; }

        public int UsersCountDeleted { get; set; }

        public double AverageTripsPerUser { get; set; }

        public double AverageCommentsPerUser { get; set; }

        public int TripsCount { get; set; }

        public int TripsCountNotDeleted { get; set; }

        public int TripsCountDeleted { get; set; }

        public double AverageParticipantsInTrip { get; set; }

        public double AverageCommentsPerTrip { get; set; }

        public int CommentsCount { get; set; }

        public int CommentsCountNotDeleted { get; set; }

        public int CommentsCountDeleted { get; set; }
    }
}