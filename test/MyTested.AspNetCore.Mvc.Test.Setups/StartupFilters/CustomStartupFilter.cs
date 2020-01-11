namespace MyTested.AspNetCore.Mvc.Test.Setups.StartupFilters
{
    using System;
    using Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;

    public class CustomStartupFilter : IStartupFilter
    {
        public bool Registered { get; private set; }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            => app =>
            {
                this.Registered = true;

                app.Use(async (context, nextMiddleware) =>
                {
                    context.Features.Set(new CustomHttpFeature());

                    await nextMiddleware();
                });

                next(app);
            };
    }
}
