namespace ApplicationParts.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Web;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
            : base(env)
        {
        }
    }
}
