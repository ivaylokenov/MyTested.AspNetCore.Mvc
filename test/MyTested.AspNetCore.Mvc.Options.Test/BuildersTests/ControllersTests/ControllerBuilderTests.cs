namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithOptionsShouldSetCorrectOptions()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("config.json")
                        .Build();
                       
                    services.Configure<CustomSettings>(configuration.GetSection("Settings"));
                });

            MyController<OptionsController>
                .Instance()
                .WithOptions(options => options
                    .For<CustomSettings>(settings => settings.Name = "Test"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok();

            MyController<OptionsController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .BadRequest();

            MyController<OptionsController>
                .Instance()
                .WithOptions(options => options
                    .For<CustomSettings>(settings => settings.Name = "Invalid"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .BadRequest();

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
