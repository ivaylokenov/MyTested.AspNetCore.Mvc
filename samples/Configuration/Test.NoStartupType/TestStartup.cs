namespace Test.NoStartupType
{
    using Microsoft.Extensions.Configuration;
    using WebApplication.Core;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
