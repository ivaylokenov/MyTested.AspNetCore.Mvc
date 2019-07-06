namespace Blog.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;

    public static class MvcOptionsExtensions
    {
        public static MvcOptions AddAutoValidateAntiforgeryToken(this MvcOptions options)
        {
            options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            return options;
        }
    }
}
