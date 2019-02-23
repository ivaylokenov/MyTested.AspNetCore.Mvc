namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using Common;
    using Microsoft.Extensions.DependencyInjection;

    public class StaticStartup : CustomStartupWithConfigureContainer
    {
        static StaticStartup()
            => MyApplication.IsRunningOn(server => server
                .WithServices(services => services
                    .AddTransient<IServiceProviderFactory<CustomContainer>, CustomContainerFactory>()));
    }
}
