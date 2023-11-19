namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Routing;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Mock of <see cref="IApplicationBuilder"/>. Used for extracting registered routes.
    /// </summary>
    public class ApplicationBuilderMock : IApplicationBuilder
    {
        private const string ServerFeaturesPropertyName = "server.Features";
        private const string ApplicationServicesPropertyName = "application.Services";

        private readonly IList<Func<RequestDelegate, RequestDelegate>> components = new List<Func<RequestDelegate, RequestDelegate>>();

        private bool endpointsEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBuilderMock"/> class.
        /// </summary>
        /// <param name="serviceProvider">Application service provider.</param>
        public ApplicationBuilderMock(IServiceProvider serviceProvider)
        {
            this.Properties = new Dictionary<string, object>
            {
                [ServerFeaturesPropertyName] = new FeatureCollection()
            };

            this.Routes = new RouteCollection();
            this.ApplicationServices = serviceProvider;

            this.CheckForEndpointRouting(serviceProvider);
        }

        /// <summary>
        /// Gets or sets the current application services.
        /// </summary>
        /// <value>Result of <see cref="IServiceProvider"/> type.</value>
        public IServiceProvider ApplicationServices
        {
            get => this.GetProperty<IServiceProvider>(ApplicationServicesPropertyName);
            set => this.SetProperty(ApplicationServicesPropertyName, value);
        }

        /// <summary>
        /// Gets the current server feature collection. Not used in the actual testing.
        /// </summary>
        /// <value>Result of <see cref="IFeatureCollection"/> type.</value>
        public IFeatureCollection ServerFeatures => this.GetProperty<IFeatureCollection>(ServerFeaturesPropertyName);

        /// <summary>
        /// Gets the current application properties. Not used in the actual testing.
        /// </summary>
        /// <value>Result of <see cref="IFeatureCollection"/> type.</value>
        public IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Gets or sets the registered route collection.
        /// </summary>
        /// <value>Result of <see cref="RouteCollection"/> type.</value>
        public RouteCollection Routes { get; set; }

        /// <summary>
        /// Extracts registered routes from the provided middleware, if such are found.
        /// </summary>
        /// <param name="middleware">Middleware delegate.</param>
        /// <returns>The same <see cref="IApplicationBuilder"/>.</returns>
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            this.ExtractEndpointRoutes(middleware);
            this.ExtractLegacyRoutes(middleware);

            this.components.Add(middleware);

            return this;
        }

        /// <summary>
        /// Returns new instance of <see cref="IApplicationBuilder"/>. Not used in the actual testing.
        /// </summary>
        /// <returns>Result of <see cref="IApplicationBuilder"/> type.</returns>
        public IApplicationBuilder New() => new ApplicationBuilderMock(this.ApplicationServices);

        /// <summary>
        /// Builds the application delegate, which will process the incoming HTTP requests. Not used in the actual testing.
        /// </summary>
        /// <returns>Result of <see cref="RequestDelegate"/> type.</returns>
        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            };

            foreach (var component in this.components.Reverse())
            {
                app = component(app);
            }

            return app;
        }

        private T GetProperty<T>(string key)
            => this.Properties.TryGetValue(key, out var value) ? (T)value : default;

        private void SetProperty<T>(string key, T value) => this.Properties[key] = value;

        private void CheckForEndpointRouting(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetService<IOptions<MvcOptions>>()?.Value;

            this.endpointsEnabled = options?.EnableEndpointRouting ?? false;
        }

        private void ExtractEndpointRoutes(Func<RequestDelegate, RequestDelegate> middleware)
        {
            var stateTypeField = middleware.GetTargetField("state");
            var stateType = stateTypeField?.GetValue(middleware.Target);

            var middlewareTypeProperty = stateType?.GetType().GetProperty("Middleware");

            if (middlewareTypeProperty?.GetValue(stateType) is not Type { Name: "EndpointMiddleware" })
            {
                return;
            }

            var routeOptions = this.ApplicationServices.GetService<IOptions<RouteOptions>>()?.Value;

            if (routeOptions == null)
            {
                return;
            }

            var routeBuilder = new RouteBuilder(this)
            {
                DefaultHandler = RouteHandlerMock.Null
            };

            var endpointDataSources = routeOptions.Exposed().EndpointDataSources;

            foreach (EndpointDataSource endpointDataSource in endpointDataSources)
            {
                var routeEndpoints = new Dictionary<string, RouteEndpoint>();

                endpointDataSource
                    .Endpoints
                    .OfType<RouteEndpoint>()
                    .Where(route => route.Metadata.All(m =>
                        Reflection.AreNotAssignable(typeof(IRouteTemplateProvider), m.GetType())))
                    .OrderBy(route => route.Order)
                    .ForEach(route =>
                    {
                        var routeNameMetadata = route.Metadata.GetMetadata<IRouteNameMetadata>();
                        var routeName = routeNameMetadata?.RouteName;

                        if (routeName != null && !routeEndpoints.ContainsKey(routeName))
                        {
                            routeEndpoints[routeName] = route;
                        }
                    });

                foreach (var (routeName, routeEndpoint) in routeEndpoints)
                {
                    var routePattern = routeEndpoint.RoutePattern;
                    var rawRouteText = routePattern.RawText;

                    var defaultValues = new Dictionary<string, object>(routePattern.Defaults);

                    routePattern.Defaults.ForEach(defaultValue =>
                    {
                        if (rawRouteText.Contains($"{defaultValue.Key}="))
                        {
                            defaultValues.Remove(defaultValue.Key);
                        }
                    });

                    var constraints = new Dictionary<string, object>();

                    routePattern.Parameters.ForEach(parameter =>
                    {
                        parameter.ParameterPolicies.ForEach(policy =>
                        {
                            if (policy.ParameterPolicy is IRouteConstraint routeConstraintPolicy)
                            {
                                constraints[parameter.Name] = routeConstraintPolicy;
                            }
                        });
                    });

                    var dataTokens = routeEndpoint
                        .Metadata
                        .GetMetadata<IDataTokensMetadata>()
                        ?.DataTokens;

                    routeBuilder.MapRoute(
                        routeName,
                        rawRouteText,
                        defaultValues,
                        constraints,
                        dataTokens);
                }
            }

            routeBuilder.Routes.ForEach(route => this.Routes.Add(route));
        }

        private void ExtractLegacyRoutes(Func<RequestDelegate, RequestDelegate> middleware)
        {
            var middlewareArguments = middleware.GetTargetField("args");

            if (middlewareArguments?.GetValue(middleware.Target) is object[] argumentsValues)
            {
                foreach (var argument in argumentsValues.OfType<RouteCollection>())
                {
                    for (int i = 0; i < argument.Count; i++)
                    {
                        this.Routes.Add(argument[i]);
                    }
                }
            }
        }
    }
}
