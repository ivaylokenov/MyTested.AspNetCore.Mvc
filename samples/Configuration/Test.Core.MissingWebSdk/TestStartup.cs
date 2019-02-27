namespace Test.Core.MissingWebSdk
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
