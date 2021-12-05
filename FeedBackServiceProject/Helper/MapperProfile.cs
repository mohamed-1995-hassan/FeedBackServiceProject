using AutoMapper;

namespace FeedBackServiceProject.Api.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Infrastructure.Entities.Feedback, Core.Models.Feedback>();
            CreateMap<Core.Models.Feedback, Infrastructure.Entities.Feedback>();
        }
    }
}
