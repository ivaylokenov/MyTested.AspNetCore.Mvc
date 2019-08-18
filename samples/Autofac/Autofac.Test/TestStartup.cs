namespace Autofac.Test
{
    using Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using MyTested.AspNetCore.Mvc;
    using Web;
    using Web.Services;
    
    public class TestStartup : Startup
    {
        // Prepare additional server configuration - add AutoFac here. 
        // Call 'IsRunningOn' either in the static constructor of the TestStartup class
        // or in an assembly initialization method if your test runner supports it.
        // Note that 'IsRunningOn' should be called only once per test project.
        static TestStartup()
            => MyApplication
                .IsRunningOn(server => server
                    .WithServices(services => services.AddAutofac()));

        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            // Call the base ConfigureServices method to register all your web application services.
            base.ConfigureServices(services);

            // Add the 'MyTested.AspNetCore.Mvc' testing infrastructure afterward.
            // Depending on your needs, you may need to call 'AddMvcUniverseTesting' instead
            // because it includes every feature the testing framework supports - for example,
            // testable DbContext, ISession and IMemoryCache.
            services.AddMvcTesting();

            // If you prefer, you may be more specific about the testing
            // infrastructure by explicitly call each supported feature you want.

            // services
            //     .AddCoreTesting()
            //     .AddRoutingTesting()
            //     .AddControllersTesting()
            //     .AddViewFeaturesTesting()
            //     .AddViewComponentsTesting()
            //     .AddStringInputFormatter()
            //     .ReplaceOptions()
            //     .ReplaceTempDataProvider()
            //     .ReplaceDbContext()
            //     .ReplaceMemoryCache()
            //     .ReplaceSession();

            // Replace all services which need to be mocked in the service collection. 
            services.ReplaceSingleton<IDateTimeService, DateTimeServiceMock>();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            // Call the base ConfigureContainer method to register all your application container services.
            base.ConfigureContainer(builder);

            // Replace all services which need to be mocked in the container.
            builder.RegisterType<DataServiceMock>().As<IDataService>();
        }
    }
}
