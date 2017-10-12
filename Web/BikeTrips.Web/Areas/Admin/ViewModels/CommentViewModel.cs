namespace BikeTrips.Web.Areas.Admin.ViewModels
{
    using System;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mappings;

    public class CommentViewModel : IMapFrom<Comment>, ICustomMapped
    {
        public int Id { get; set; }
        
        public string Content { get; set; }
        
        public string Author { get; set; }
                
        public string Subject { get; set; }
        
        public DateTime UtcTime { get; private set; }
        
        public bool IsDeleted { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.Subject, opt => opt.MapFrom(x => x.Subject.TripName));
        }
    }
}