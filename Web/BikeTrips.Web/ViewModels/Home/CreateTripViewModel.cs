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
        public string TripName { get; set; }

        [DisplayName("Starting point")]
        [Required]
        public string StartingPoint { get; set; }

        [DisplayName("Trip type")]
        [Required]
        public TripType Type { get; set; }

        [DisplayName("Starting date")]
        [Required]
        public string TripDate { get; set; }

        [DisplayName("Starting time")]
        [Required]
        public string TripTime { get; set; }

        [Required]
        public double Distance { get; set; }

        public double Denivelation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int LocalTimeOffsetMinutes { get; set; }

        public virtual User Creator { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    //dateString = "15/06/2008 08:30";
        //    configuration.CreateMap<Trip, CreateTripViewModel>()
        //        .ForMember(x => DateTime.Parse($"{x.TripDate} {x.TripTime}"), opt => opt.MapFrom(x => x.StartingTime));
        //}
    }
}