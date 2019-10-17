namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithOptionsShouldSetCorrectOptions()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("config.json")
                        .Build();

                    services.Configure<CustomSettings>(configuration.GetSection("Settings"));
                });

            MyViewComponent<OptionsComponent>
                .Instance()
                .WithOptions(options => options
                    .For<CustomSettings>(settings => settings.Name = "Test"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View();

            MyViewComponent<OptionsComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content();

            MyViewComponent<OptionsComponent>
                .Instance()
                .WithOptions(options => options
                    .For<CustomSettings>(settings => settings.Name = "Invalid"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content();

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
