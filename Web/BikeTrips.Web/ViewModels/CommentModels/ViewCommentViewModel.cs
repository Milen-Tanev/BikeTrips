using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;

namespace BikeTrips.Web.ViewModels.CommentModels
{
    public class ViewCommentViewModel : IMapTo<Comment>
    {
        public string Content { get; set; }

        public virtual User Author { get; set; }
    }
}