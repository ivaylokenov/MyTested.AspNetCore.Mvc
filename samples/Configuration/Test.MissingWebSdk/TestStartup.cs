namespace Test.MissingWebSdk
{
    using Microsoft.Extensions.Configuration;
    using WebApplication;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}