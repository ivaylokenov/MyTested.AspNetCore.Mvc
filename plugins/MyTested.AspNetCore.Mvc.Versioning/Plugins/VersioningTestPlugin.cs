namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using System.Linq;
    using Asp.Versioning;
    using Internal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    public class VersioningTestPlugin
        : IHttpFeatureRegistrationPlugin,
          IServiceRegistrationPlugin,
          IRoutingServiceRegistrationPlugin
    {
        public Action<HttpContext> HttpFeatureRegistrationDelegate
            => httpContext => httpContext
                .Features
                .Set<IApiVersioningFeature>(new ApiVersioningFeature(httpContext));

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor
                => serviceDescriptor.ServiceType == typeof(IActionConstraintProvider);

        public Action<IServiceCollection> ServiceRegistrationDelegate
            => serviceCollection
                => serviceCollection.AddSingleton<IActionConstraintProvider, ApiVersionActionConstraintProvider>();

        public Action<IServiceCollection> RoutingServiceRegistrationDelegate
            => serviceCollection =>
            {
                var selectorDescriptor = serviceCollection
                    .FirstOrDefault(d => d.ServiceType == typeof(IActionSelector));

                if (selectorDescriptor != null)
                {
                    serviceCollection.Remove(selectorDescriptor);
                    serviceCollection.AddSingleton<IActionSelector>(sp =>
                    {
                        var innerSelector = (IActionSelector)ActivatorUtilities
                            .CreateInstance(sp, selectorDescriptor.ImplementationType);

                        return new ApiVersionAwareActionSelector(innerSelector);
                    });
                }
            };
    }
}
