namespace BikeTrips.Web.ViewModels.UserModels
{
    using Data.Models;
    using Infrastructure.Mappings;

    public class ShortUserViewModel : IMapFrom<User>
    {
        public string Id;

        public string UserName { get; set; }

        public bool IsDeleted { get; set; }
    }
}