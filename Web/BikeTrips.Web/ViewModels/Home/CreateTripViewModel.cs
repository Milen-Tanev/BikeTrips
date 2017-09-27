using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeTrips.Web.ViewModels.Home
{
    public class CreateTripViewModel : IMapTo<Trip>
    {
        [Required]
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

        public bool IsPassed { get; protected set; }

        public bool IsDeleted { get; set; }
    }
}