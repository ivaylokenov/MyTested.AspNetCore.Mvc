namespace Blog.Services.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using Models;

    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this
                .CreateMap<Article, ArticleListingServiceModel>()
                .ForMember(a => a.Author, opt => opt.MapFrom(a => a.User.UserName));
            
            this
                .CreateMap<Article, ArticleNonPublicListingServiceModel>()
                .ForMember(a => a.Author, opt => opt.MapFrom(a => a.User.UserName));

            this
                .CreateMap<Article, ArticleDetailsServiceModel>()
                .ForMember(a => a.Author, opt => opt.MapFrom(a => a.User.UserName));
            
            this
                .CreateMap<Article, ArticleForUserListingServiceModel>()
                .ForMember(a => a.Author, opt => opt.MapFrom(a => a.User.UserName));
        }
    }
}
