namespace Test.FullFramework.NoCompilationContext
{
    using Microsoft.Extensions.Configuration;
    using WebApplication.FullFramework;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
