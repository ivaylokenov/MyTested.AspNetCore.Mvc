﻿namespace MyTested.AspNetCore.Mvc.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups;
    using Setups.Controllers;
    using Setups.Services;
    using System;
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
                .ResultOfType<string>(result => result
                    .Passing(r => r == "Scoped"));

            MyController<ServicesController>
                .Instance()
                .WithDependencies(dependencies => dependencies
                    .WithNo<IScopedService>())
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>(result => result
                    .Passing(r => r == "Default"));

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>(result => result
                    .Passing(r => r == "Constructor"));

            MyController<ServicesController>
                .Instance()
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>(result => result
                    .Passing(r => r == "Constructor"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ShouldPassForShouldWorkCorrectly()
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
                .ShouldPassForThe<IServiceProvider>(services =>
                {
                    var scopedService = services.GetRequiredService<IScopedService>();
                    Assert.True(scopedService.Value == "Scoped");
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
