﻿using BikeTrips.Services.Web.Contracts;
using Common.Constants;
using System;
using System.Text;

namespace BikeTrips.Services.Web
{
    public class IdentifierProvider : IIdentifierProvider
    {
        public int GetId(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var str = Encoding.UTF8.GetString(base64EncodedBytes);
            str = str.Replace(IdentityConstants.Salt, string.Empty);
            return int.Parse(str);
        }

        public string GetUrlId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{id}{IdentityConstants.Salt}");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
