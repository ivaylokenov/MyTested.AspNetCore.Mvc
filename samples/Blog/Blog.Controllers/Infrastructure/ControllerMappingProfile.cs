namespace Blog.Controllers.Infrastructure
{
    using AutoMapper;
    using Models;
    using Services.Models;

    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this
                .CreateMap<ArticleDetailsServiceModel, ArticleFormModel>();
        }
    }
}
