namespace BikeTrips.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Contracts;
    using global::Common.Constants;

    public class Trip : IDeletable
    {
        public Trip()
        {
            this.Participants = new HashSet<User>();
            this.Comments = new SortedSet<Comment>();
            this.UtcTime = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CommonStringLengthConstants.StandardMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string TripName { get; set; }

        [MaxLength(CommonStringLengthConstants.StandardMaxLength)]
        [Required]
        public string StartingPoint { get; set; }

        [Required]
        public TripType Type { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartingTime { get; set; }

        [Required]
        [Range(CommonNumericConstants.MinDistance, CommonNumericConstants.MaxDistance)]
        public double Distance { get; set; }

        [Range(CommonNumericConstants.MinDistance, CommonNumericConstants.MaxDistance)]
        public double Denivelation { get; set; }

        [Required]
        [MaxLength(CommonStringLengthConstants.LongMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string Description { get; set; }

        [Required]
        [Range(CommonNumericConstants.MinOffset, CommonNumericConstants.MaxOffset)]
        public short LocalTimeOffsetMinutes { get; set; }

        [Required]
        public DateTime UtcTime { get; protected set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<User> Participants { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
