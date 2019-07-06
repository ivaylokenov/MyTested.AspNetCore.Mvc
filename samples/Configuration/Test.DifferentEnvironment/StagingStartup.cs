namespace Test.DifferentEnvironment
{
    using Microsoft.Extensions.DependencyInjection;

    public class StagingStartup
    {
        public void ConfigureStagingServices(IServiceCollection services)
        {
            services.AddMvc();
        }
    }
}
