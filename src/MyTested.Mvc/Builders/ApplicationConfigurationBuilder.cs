namespace MyTested.Mvc.Builders
{
    using System;
    using Contracts.Application;
    using Internal.Application;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Routing;
    using Microsoft.Extensions.DependencyInjection;

    public class ApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        public ApplicationConfigurationBuilder(Type startupType)
        {
            TestApplication.StartupType = startupType?.GetType();
        }

        public IApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services)
        {
            TestApplication.AdditionalServices = services;
            return this;
        }

        public IApplicationConfigurationBuilder WithApplication(Action<IApplicationBuilder> app)
        {
            TestApplication.AdditionalConfiguration = app;
            return this;
        }

        public IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes)
        {
            TestApplication.AdditionalRoutes = routes;
            return this;
        }
    }
}
