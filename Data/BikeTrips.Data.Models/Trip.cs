using BikeTrips.Data.Common.Contracts;
using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeTrips.Data.Models
{
    public class Trip : IDeletable
    {
        public Trip()
        {
            this.Participants = new List<User>();
            this.Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index]
        [MaxLength(CommonStringLengthConstants.LongMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string TripName { get; protected set; }

        [Required]
        [MaxLength(CommonStringLengthConstants.LongMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string StartingPoint { get; protected set; }

        [Required]
        public TripType Type { get; protected set; }

        [Required]
        public string TripDate { get; protected set; }

        [Required]
        [MaxLength(CommonStringLengthConstants.StandardMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string TripTime { get; protected set; }

        [Required]
        public double Distance { get; protected set; }

        public double Denivelation { get; protected set; }

        [Required]
        [MaxLength(CommonStringLengthConstants.StandardMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string LandMark { get; protected set; }

        [Required]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string Description { get; protected set; }

        [Required]
        public int LocalTimeOffsetMinutes { get; protected set; }

        [Required]
        public DateTime ServerTimeReservation { get; protected set; }

        [Required]
        public virtual User Creator { get; protected set; }

        public virtual ICollection<User> Participants { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public bool IsPassed { get; protected set; }

        public bool IsDeleted { get; set; }
    }
}
