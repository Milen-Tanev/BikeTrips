using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.ComponentModel;

namespace BikeTrips.Web.ViewModels.Home
{
    public class CreateTripViewModel : IMapTo<Trip>
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
        public DateTime TripDate { get; set; }

        [DisplayName("Starting time")]
        [Required]
        public DateTime TripTime { get; set; }

        [Required]
        public double Distance { get; set; }

        public double Denivelation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int LocalTimeOffsetMinutes { get; set; }

        public virtual User Creator { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            throw new NotImplementedException();
        }
    }
}