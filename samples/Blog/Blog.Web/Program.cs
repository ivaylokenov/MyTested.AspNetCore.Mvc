namespace Blog.Web
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

#pragma warning disable ASPDEPR008 // Obsolete WebHost, kept to show the Startup class based hosting which MyTested supports.
    public class Program
    {
        public static void Main(string[] args) 
            => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
            => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
#pragma warning restore ASPDEPR008 // Obsolete WebHost, kept to show the Startup class based hosting which MyTested supports.
}
