namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal.Application;
    using Internal.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Setups.Common;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void OptionsLifetimesShouldBeReplacedWithScoped()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("config.json")
                        .Build();

                    services.Configure<CustomOptions>(configuration.GetSection("Options"));
                    services.Configure<CustomSettings>(configuration.GetSection("Settings"));
                });

            var options = TestApplication.Services.GetService<IOptions<CustomOptions>>();
            var settings = TestApplication.Services.GetService<IOptions<CustomSettings>>();

            Assert.NotNull(options);
            Assert.NotNull(settings);

            Assert.Equal(ServiceLifetime.Scoped, TestServiceProvider.GetServiceLifetime(typeof(IOptions<>)));

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
