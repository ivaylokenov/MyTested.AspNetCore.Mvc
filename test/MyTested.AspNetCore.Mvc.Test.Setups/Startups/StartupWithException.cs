namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class StartupWithException
    {
        public virtual void ConfigureServices(IServiceCollection services)
            => throw new Exception("Exception during service registration.");

        public virtual void Configure(IApplicationBuilder app)
            => app
                .UseRouting()
                .UseEndpoints(endpoints => endpoints
                    .MapDefaultControllerRoute());
    }
}
