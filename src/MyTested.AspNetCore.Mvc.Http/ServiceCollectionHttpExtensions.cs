namespace MyTested.AspNetCore.Mvc
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Validators;
    using Internal.Formatters;

    /// <summary>
    /// Contains HTTP extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionHttpExtensions
    {
        /// <summary>
        /// Adds <see cref="IHttpContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            return serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds an <see cref="IInputFormatter"/> which can process "text/plain" media type. Useful for testing with HTTP request body.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddStringInputFormatter(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            
            // custom MVC options
            serviceCollection.Configure<MvcOptions>(options =>
            {
                // string input formatter helps with HTTP request processing
                var inputFormatters = options.InputFormatters.OfType<TextInputFormatter>();
                if (!inputFormatters.Any(f => f.SupportedMediaTypes.Contains(ContentType.TextPlain)))
                {
                    options.InputFormatters.Add(new StringInputFormatter());
                }
            });

            return serviceCollection;
        }
    }
}
