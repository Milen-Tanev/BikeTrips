using BikeTrips.Data.Models;
using BikeTrips.Web.Infrastructure.Mappings;
using System;
using AutoMapper;

namespace BikeTrips.Web.ViewModels.CommentModels
{
    public class CommentViewModel: IMapFrom<Comment>, ICustomMappings
    {
        public string Content { get; set; }
        
        public string Author { get; set; }
        
        public DateTime UtcTime { get; private set; }

        public Trip Subject { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}