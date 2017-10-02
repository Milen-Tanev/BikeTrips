﻿using BikeTrips.Data.Common.Contracts;
using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeTrips.Data.Models
{
    public class Trip : IDeletable
    {
        private bool isDeleted;

        public Trip()
        {
            this.Participants = new List<User>();
            this.Comments = new List<Comment>();
            this.UtcTime = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string TripName { get; set; }

        [Required]
        public string StartingPoint { get; set; }

        [Required]
        public TripType Type { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartingTime { get; set; }

        [Required]
        public double Distance { get; set; }

        public double Denivelation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int LocalTimeOffsetMinutes { get; set; }

        [Required]
        public DateTime UtcTime { get; protected set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<User> Participants { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public bool IsPassed { get; set; }

        public bool IsDeleted { get; set; }
    }
}
