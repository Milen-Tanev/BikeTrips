namespace BikeTrips.Web.ViewModels.UserModels
{
    using Data.Models;
    using Infrastructure.Mappings;

    public class ShortUserViewModel : IMapFrom<User>
    {
        public string id;

        public string UserName { get; set; }
    }
}