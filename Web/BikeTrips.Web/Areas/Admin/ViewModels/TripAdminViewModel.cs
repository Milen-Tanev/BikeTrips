namespace BikeTrips.Web.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    using Data.Models;
    using Infrastructure.Mappings;
    using Services.Web;
    using Services.Web.Contracts;
    using Web.ViewModels.UserModels;

    public class TripAdminViewModel : IMapFrom<Trip>
    {
        public TripAdminViewModel()
        {
            this.Participants = new HashSet<UserViewModel>();
            this.Comments = new SortedSet<CommentViewModel>();
            this.UtcTime = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public TripType Type { get; set; }

        public DateTime StartingTime { get; set; }

        public double Distance { get; set; }

        public double Denivelation { get; set; }

        public string Description { get; set; }

        public short LocalTimeOffsetMinutes { get; set; }

        public DateTime UtcTime { get; protected set; }

        public UserViewModel Creator { get; set; }

        public ICollection<UserViewModel> Participants { get; protected set; }

        public ICollection<CommentViewModel> Comments { get; protected set; }

        public bool IsDeleted { get; set; }

        public string UrlId
        {
            get
            {
                IIdentifierProvider identifierProvider = new IdentifierProvider();
                return identifierProvider.GetUrlId(this.Id);
            }
        }
    }
}
