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
            MyMvc
                   .IsUsingDefaultConfiguration()
                   .WithServices(services =>
                   {
                       var configuration = new ConfigurationBuilder()
                           .AddJsonFile("config.json")
                           .Build();
                       
                       services.Configure<CustomSettings>(configuration.GetSection("Settings"));
                   });

            MyMvc
                .Controller<OptionsController>()
                .WithOptions(options => options
                    .For<CustomSettings>(settings => settings.Name = "Test"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok();

            MyMvc
                .Controller<OptionsController>()
                .Calling(c => c.Index())
                .ShouldReturn()
                .BadRequest();

            MyMvc
                .Controller<OptionsController>()
                .WithOptions(options => options
                    .For<CustomSettings>(settings => settings.Name = "Invalid"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .BadRequest();

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
