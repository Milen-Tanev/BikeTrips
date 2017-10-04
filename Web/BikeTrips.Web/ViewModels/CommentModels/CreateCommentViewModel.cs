using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BikeTrips.Web.ViewModels.CommentModels
{
    public class CreateCommentViewModel: IMapTo<Comment>
    {
        [Required]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string Content { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public short LocalTimeOffsetMinutes { get; set; }

        public Trip Subject { get; set; }
    }
}