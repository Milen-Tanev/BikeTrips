using BikeTrips.Data.Common.Contracts;
using Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace BikeTrips.Data.Models
{
    public class Comment : IDeletable, IComparable<Comment>
    {
        public Comment()
        {
            this.UtcTime = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(CommonStringLengthConstants.MinMinLength)]
        public string Content { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        [Range(CommonNumericConstants.MinOffset, CommonNumericConstants.MaxOffset)]
        public short LocalTimeOffsetMinutes { get; set; }
        
        [Required]
        public virtual Trip Subject { get; set; }

        [Required]
        public DateTime UtcTime { get; private set; }

        [Required]
        public bool IsDeleted {  get; set; }

        public int CompareTo(Comment other)
        {
            return this.UtcTime.CompareTo(other.UtcTime) * -1;
        }
    }
}
