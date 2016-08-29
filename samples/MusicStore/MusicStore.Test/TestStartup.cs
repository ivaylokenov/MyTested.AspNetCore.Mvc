namespace MusicStore.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Models;
    using MyTested.AspNetCore.Mvc;
#if NET451
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using MyTested.AspNetCore.Mvc.Internal;
    using MyTested.AspNetCore.Mvc.Plugins;
#endif

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
            : base(env)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            
            services.ReplaceSingleton<SignInManager<ApplicationUser>, SignInManagerMock>();

            // temporary workaround while DependencyContext issues are fixed for .NET 4.5.1
            // controller type validation is also removed for .NET 4.5.1
#if NET451
            var mvc = services.AddMvc();
            var applicationParts = mvc.PartManager.ApplicationParts;
            applicationParts.Clear();
            applicationParts.Add(new AssemblyPart(typeof(Startup).GetTypeInfo().Assembly));

            TestHelper.HttpFeatureRegistrationPlugins.Add(new SessionTestPlugin());

            TestHelper.ShouldPassForPlugins.Add(new AbstractionsTestPlugin());
            TestHelper.ShouldPassForPlugins.Add(new ControllersTestPlugin());

            services
                .AddMvcTesting()
                .AddRoutingTesting();
#endif
        }
    }
}
