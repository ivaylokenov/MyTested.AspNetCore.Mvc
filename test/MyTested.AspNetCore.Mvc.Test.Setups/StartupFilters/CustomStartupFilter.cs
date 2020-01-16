namespace MyTested.AspNetCore.Mvc.Test.Setups.StartupFilters
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class CustomStartupFilter : IStartupFilter
    {
        public bool Registered { get; private set; }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            => app =>
            {
                this.Registered = true;

                app.Use(async (context, nextMiddleware) =>
                {
                    await context.Response.WriteAsync("Test Response");

                    await nextMiddleware();
                });

                next(app);
            };
    }
}
