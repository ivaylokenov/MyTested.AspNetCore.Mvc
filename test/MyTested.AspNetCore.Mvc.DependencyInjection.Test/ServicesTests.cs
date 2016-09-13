namespace MyTested.AspNetCore.Mvc.Test
{
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups;
    using Setups.Controllers;
    using Setups.Services;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void ScopedServicesShouldRemainThroughTheTestCase()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.TryAddScoped<IScopedService, ScopedService>();
                });

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.SetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Scoped");

            MyController<ServicesController>
                .Instance()
                .WithServices(services => services
                    .WithNo<IScopedService>())
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Default");

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Constructor");

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Constructor");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
