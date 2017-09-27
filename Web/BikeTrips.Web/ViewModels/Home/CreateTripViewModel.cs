using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.ComponentModel;

namespace BikeTrips.Web.ViewModels.Home
{
    public class CreateTripViewModel : IMapTo<Trip>//, ICustomMappings
    {
        [DisplayName("Trip name")]
        [Required]
        [MaxLength(CommonStringLengthConstants.LongMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string TripName { get; protected set; }

        [DisplayName("Starting point")]
        [Required]
        [MaxLength(CommonStringLengthConstants.LongMaxLength)]
        [MinLength(CommonStringLengthConstants.StandardMinLength)]
        public string StartingPoint { get; protected set; }

        [DisplayName("Trip type")]
        [Required]
        public TripType Type { get; protected set; }

        [DisplayName("Starting date")]
        [Required]
        public string TripDate { get; protected set; }

        [DisplayName("Starting time")]
        [Required]
        public string TripTime { get; protected set; }

        [Required]
        public double Distance { get; protected set; }

        public double Denivelation { get; protected set; }

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

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    //dateString = "15/06/2008 08:30";
        //    configuration.CreateMap<Trip, CreateTripViewModel>()
        //        .ForMember(x => DateTime.Parse($"{x.TripDate} {x.TripTime}"), opt => opt.MapFrom(x => x.StartingTime));
        //}
    }
}