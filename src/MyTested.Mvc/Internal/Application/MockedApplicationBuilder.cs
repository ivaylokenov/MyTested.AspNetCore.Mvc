namespace MyTested.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Features;
    using Microsoft.AspNet.Routing;
    using System.Reflection;
    public class MockedApplicationBuilder : IApplicationBuilder
    {
        private const string ServerFeaturesPropertyName = "server.Features";
        private const string ApplicationServicesPropertyName = "application.Services";

        private readonly IList<Func<RequestDelegate, RequestDelegate>> components = new List<Func<RequestDelegate, RequestDelegate>>();

        public MockedApplicationBuilder(IServiceProvider serviceProvider)
        {
            this.Properties = new Dictionary<string, object>();
            this.Routes = new RouteCollection();
            this.ApplicationServices = serviceProvider;
        }

        public MockedApplicationBuilder(IApplicationBuilder builder)
        {
            this.Properties = builder.Properties;
        }
        
        public IServiceProvider ApplicationServices
        {
            get
            {
                return this.GetProperty<IServiceProvider>(ApplicationServicesPropertyName);
            }
            set
            {
                this.SetProperty(ApplicationServicesPropertyName, value);
            }
        }

        public IFeatureCollection ServerFeatures
        {
            get
            {
                return this.GetProperty<IFeatureCollection>(ServerFeaturesPropertyName);
            }
        }

        public IDictionary<string, object> Properties { get; }

        public RouteCollection Routes { get; set; }

        private T GetProperty<T>(string key)
        {
            object value;
            return this.Properties.TryGetValue(key, out value) ? (T)value : default(T);
        }

        private void SetProperty<T>(string key, T value)
        {
            this.Properties[key] = value;
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            this.ExtractRoutes(middleware);

            this.components.Add(middleware);
            return this;
        }

        public IApplicationBuilder New()
        {
            return new MockedApplicationBuilder(this);
        }

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
        
        private void ExtractRoutes(Func<RequestDelegate, RequestDelegate> middleware)
        {
            var middlewareArguments = middleware
               .Target
               .GetType()
               .GetTypeInfo()
               .DeclaredFields
               .FirstOrDefault(m => m.Name == "args");

            if (middlewareArguments != null)
            {
                var argumentsValues = middlewareArguments.GetValue(middleware.Target) as object[];
                if (argumentsValues != null)
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
}
