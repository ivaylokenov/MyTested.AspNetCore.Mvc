using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ApplicationParts.Web
{
#pragma warning disable ASPDEPR008 // Obsolete WebHost, kept to show the Startup class based hosting which MyTested supports.
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
#pragma warning restore ASPDEPR008 // Obsolete WebHost, kept to show the Startup class based hosting which MyTested supports.
}
