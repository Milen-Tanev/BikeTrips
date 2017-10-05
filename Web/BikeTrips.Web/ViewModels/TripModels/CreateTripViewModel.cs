using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.ComponentModel;
using Common.Constants;

namespace BikeTrips.Web.ViewModels.TripModels
{
    public class CreateTripViewModel : IMapTo<Trip>
    {
        [Required]
        [DisplayName("Trip name")]
        [StringLength(CommonStringLengthConstants.StandardMaxLength,
            ErrorMessage = ErrorMessageConstants.InvalidTripNameLengthErrorMessage,
            MinimumLength = CommonStringLengthConstants.StandardMinLength)]
        public string TripName { get; set; }

        [Required]
        [StringLength(CommonStringLengthConstants.StandardMaxLength,
            ErrorMessage = ErrorMessageConstants.InvalidTripStartingPointLengthErrorMessage)]
        [DisplayName("Starting point")]
        public string StartingPoint { get; set; }

        [DisplayName("Trip type")]
        [Required]
        public TripType Type { get; set; }

        [DisplayName("Starting date")]
        [Required]
        public DateTime TripDate { get; set; }

        [DisplayName("Starting time")]
        [Required]
        public DateTime TripTime { get; set; }

        [Required]
        public double Distance { get; set; }

        public double? Denivelation { get; set; }

        [StringLength(CommonStringLengthConstants.LongMaxLength,
            ErrorMessage = ErrorMessageConstants.InvalidTripNameLengthErrorMessage,
            MinimumLength = CommonStringLengthConstants.MinMinLength)]
        public string Description { get; set; }

        [Required]
        public short LocalTimeOffsetMinutes { get; set; }
    }
}
