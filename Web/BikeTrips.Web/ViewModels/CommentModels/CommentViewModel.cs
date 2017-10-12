namespace BikeTrips.Web.ViewModels.CommentModels
{
    using System;

    using Data.Models;
    using Infrastructure.Mappings;
    using UserModels;

    public class CommentViewModel: IMapFrom<Comment>
    {
        public string Content { get; set; }
        
        public ShortUserViewModel Author { get; set; }
        
        public DateTime UtcTime { get; private set; }

        public Trip Subject { get; set; }
    }
}