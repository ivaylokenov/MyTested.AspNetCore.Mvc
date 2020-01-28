namespace MyTested.AspNetCore.Mvc.Internal.Server
{
    using System;
    using System.Diagnostics;
    using Configuration;
    using Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Builder;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;
    using Services;

    public static partial class TestWebServer
    {
        internal static Action<IServiceCollection> AdditionalServices { get; set; }
        
        internal static IServiceCollection GetInitialServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            // Default server services.
            serviceCollection.AddSingleton(Environment);
            serviceCollection.AddSingleton<IApplicationLifetime, ApplicationLifetime>();

            serviceCollection.AddTransient<IApplicationBuilderFactory, ApplicationBuilderFactory>();
            serviceCollection.AddTransient<IHttpContextFactory, HttpContextFactory>();
            serviceCollection.AddScoped<IMiddlewareFactory, MiddlewareFactory>();
            serviceCollection.AddOptions();

            serviceCollection.AddSingleton<ILoggerFactory>(LoggerFactoryMock.Create());
            serviceCollection.AddLogging();

            serviceCollection.AddSingleton(ServerTestConfiguration.Global.Configuration);

            var diagnosticListener = new DiagnosticListener(TestFramework.TestFrameworkName);

            serviceCollection.AddSingleton(diagnosticListener);
            serviceCollection.AddSingleton<DiagnosticSource>(diagnosticListener);

            serviceCollection.AddTransient<IStartupFilter, AutoRequestServicesStartupFilter>();
            serviceCollection.AddTransient<IServiceProviderFactory<IServiceCollection>, DefaultServiceProviderFactory>();

            serviceCollection.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            // Test services.
            serviceCollection.AddRoutingServices();

            return serviceCollection;
        }
    }
}
