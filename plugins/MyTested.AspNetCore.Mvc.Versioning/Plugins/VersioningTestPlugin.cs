namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using System.Linq;
    using Asp.Versioning;
    using Internal.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    public class VersioningTestPlugin : IServiceRegistrationPlugin, IRoutingServiceRegistrationPlugin, IHttpFeatureRegistrationPlugin
    {
        private readonly Type apiVersionParserServiceType = typeof(IApiVersionParser);
        private readonly Type actionSelectorServiceType = typeof(IActionSelector);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor => serviceDescriptor.ServiceType == this.apiVersionParserServiceType;

        // The attribute routes are resolved with the application services, so their action selector is decorated too.
        public Action<IServiceCollection> ServiceRegistrationDelegate
            => serviceCollection => this.TryDecorateActionSelector(serviceCollection);

        public Action<IServiceCollection> RoutingServiceRegistrationDelegate
            => serviceCollection => this.TryDecorateActionSelector(serviceCollection);

        // The existing feature keeps the API version resolved during the route matching.
        public Action<HttpContext> HttpFeatureRegistrationDelegate
            => httpContext =>
            {
                if (httpContext.Features.Get<IApiVersioningFeature>() == null)
                {
                    httpContext.Features.Set<IApiVersioningFeature>(new ApiVersioningFeature(httpContext));
                }
            };

        private void TryDecorateActionSelector(IServiceCollection serviceCollection)
        {
            // API versioning is not configured by the tested application.
            if (serviceCollection.All(s => s.ServiceType != this.apiVersionParserServiceType))
            {
                return;
            }

            for (var index = serviceCollection.Count - 1; index >= 0; index--)
            {
                var serviceDescriptor = serviceCollection[index];

                if (serviceDescriptor.ServiceType != this.actionSelectorServiceType
                    || serviceDescriptor.IsKeyedService)
                {
                    continue;
                }

                if (!(serviceDescriptor is ApiVersionAwareActionSelectorServiceDescriptor))
                {
                    serviceCollection[index] = new ApiVersionAwareActionSelectorServiceDescriptor(serviceDescriptor);
                }

                return;
            }
        }
    }
}
