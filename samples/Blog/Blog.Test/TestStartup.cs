namespace Blog.Test
{
    using Microsoft.Extensions.Configuration;
    using Web;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
