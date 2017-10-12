namespace BikeTrips.Web.ViewModels.UserModels
{
    using System.Collections.Generic;

    using Areas.Admin.ViewModels;
    using Data.Models;
    using Infrastructure.Mappings;
    using TripModels;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<FullTripViewModel> AdministeredEvents { get; set; }

        public virtual ICollection<FullTripViewModel> VisitedEvents { get; set; }

        public virtual ICollection<CommentViewModel> Comments { get; set; }

        public bool IsDeleted { get; set; }
    }
}