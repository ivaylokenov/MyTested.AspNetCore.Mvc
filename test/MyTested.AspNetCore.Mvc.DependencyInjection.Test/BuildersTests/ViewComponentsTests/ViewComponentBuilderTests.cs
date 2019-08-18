namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Services;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithAndWithNoShouldWorkCorrectly()
        {
            MyViewComponent<MultipleServicesComponent>
                .Instance()
                .WithDependencies(dependencies => dependencies
                    .With<IInjectedService>(new InjectedService())
                    .WithNo<IAnotherInjectedService>())
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<MultipleServicesComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent.InjectedService);
                    Assert.Null(viewComponent.AnotherService);
                });
        }
        
        [Fact]
        public void WithServicesShouldWorkCorrectly()
        {
            var service = new InjectedService();
            var anotherService = new AnotherInjectedService();

            MyViewComponent<MultipleServicesComponent>
                .Instance()
                .WithDependencies(dependencies => dependencies
                    .With(service)
                    .With(anotherService))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<MultipleServicesComponent>(viewComponent =>
                {
                    Assert.Same(service, viewComponent.InjectedService);
                    Assert.Same(anotherService, viewComponent.AnotherService);
                });
        }
        
        [Fact]
        public void WithServicesDirectlyShouldWorkCorrectly()
        {
            var service = new InjectedService();
            var anotherService = new AnotherInjectedService();

            MyViewComponent<MultipleServicesComponent>
                .Instance()
                .WithDependencies(service, anotherService)
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<MultipleServicesComponent>(viewComponent =>
                {
                    Assert.Same(service, viewComponent.InjectedService);
                    Assert.Same(anotherService, viewComponent.AnotherService);
                });
        }

        [Fact]
        public void WithServiceSetupForShouldSetupScopedServiceCorrectlyWithConstructorInjection()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddScoped<IScopedService, ScopedService>();
                });

            MyViewComponent<ScopedServiceComponent>
                .Instance()
                .WithDependencies(dependencies => dependencies
                    .WithSetupFor<IScopedService>(s => s.Value = "TestValue"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View("TestValue");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
