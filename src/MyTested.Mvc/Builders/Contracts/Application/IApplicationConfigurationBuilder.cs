namespace MyTested.Mvc.Builders.Contracts.Application
{
    using System;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Routing;
    using Microsoft.Extensions.DependencyInjection;

    public interface IApplicationConfigurationBuilder
    {
        IApplicationConfigurationBuilder WithServices(Action<IServiceCollection> services);

        IApplicationConfigurationBuilder WithApplication(Action<IApplicationBuilder> app);

        IApplicationConfigurationBuilder WithRoutes(Action<IRouteBuilder> routes);
    }
}
