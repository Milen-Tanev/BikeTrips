﻿using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;

namespace BikeTrips.Web.ViewModels.UserModels
{
    public class ShortUserViewModel : IMapFrom<User>
    {
        public string Name { get; set; }
    }
}