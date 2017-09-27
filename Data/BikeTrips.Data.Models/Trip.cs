using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeTrips.Data.Models
{
    public class Trip
    {
        public Trip(string tripName, string startingPoint, TripType type, string tripDate,
            string tripTime, double distance, double? denivelation, string landMark,
            string description, DateTimeOffset localTimeOffset, User creator)
        {
            this.TripName = tripName;
            this.StartingPoint = startingPoint;
            this.Type = type;
            this.TripDate = tripDate;
            this.TripTime = tripTime;
            this.Distance = distance;
            this.Denivelation = (denivelation != null) ? (double)denivelation : -1.0D;
            this.ServerTimeReservation = DateTime.UtcNow;

            this.Participants = new List<User>();
            this.Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string TripName { get; protected set; }

        [Required]
        public string StartingPoint { get; protected set; }

        [Required]
        public TripType Type { get; protected set; }

        [Required]
        public string TripDate { get; protected set; }

        [Required]
        public string TripTime { get; protected set; }

        [Required]
        public double Distance { get; protected set; }

        public double Denivelation { get; protected set; }

        [Required]
        public string LandMark { get; protected set; }

        [Required]
        public string Description { get; protected set; }

        [Required]
        public DateTimeOffset LocalTimeOffset { get; protected set; }

        [Required]
        public DateTime ServerTimeReservation { get; protected set; }

        [Required]
        public virtual User Creator { get; protected set; }

        public virtual ICollection<User> Participants { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public bool IsPassed { get; protected set; }

        public bool IsDeleted { get; protected set; }
    }
}
