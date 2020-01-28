namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Mock of <see cref="IApplicationBuilder"/>. Used for extracting registered routes.
    /// </summary>
    public class ApplicationBuilderMock : IApplicationBuilder
    {
        private const string ServerFeaturesPropertyName = "server.Features";
        private const string ApplicationServicesPropertyName = "application.Services";

        private readonly IList<Func<RequestDelegate, RequestDelegate>> components = new List<Func<RequestDelegate, RequestDelegate>>();

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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBuilderMock"/> class.
        /// </summary>
        /// <param name="builder">Application builder to copy properties from.</param>
        public ApplicationBuilderMock(IApplicationBuilder builder) 
            => this.Properties = builder.Properties;

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
            this.ExtractRoutes(middleware);

            this.components.Add(middleware);
            return this;
        }

        /// <summary>
        /// Returns new instance of <see cref="IApplicationBuilder"/>. Not used in the actual testing.
        /// </summary>
        /// <returns>Result of <see cref="IApplicationBuilder"/> type.</returns>
        public IApplicationBuilder New() => new ApplicationBuilderMock(this);

        /// <summary>
        /// Builds the application delegate, which will process the incoming HTTP requests. Not used in the actual testing.
        /// </summary>
        /// <returns>Result of <see cref="RequestDelegate"/> type.</returns>
        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                context.Response.StatusCode = 404;
                return Task.FromResult(0);
            };

            foreach (var component in this.components.Reverse())
            {
                app = component(app);
            }

            return app;
        }

        private T GetProperty<T>(string key) 
            => this.Properties.TryGetValue(key, out var value) ? (T)value : default(T);

        private void SetProperty<T>(string key, T value) => this.Properties[key] = value;

        private void ExtractRoutes(Func<RequestDelegate, RequestDelegate> middleware)
        {
            var middlewareArguments = middleware
               .Target
               .GetType()
               .GetTypeInfo()
               .DeclaredFields
               .FirstOrDefault(m => m.Name == "args");

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
