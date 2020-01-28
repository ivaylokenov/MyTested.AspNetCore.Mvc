namespace MyTested.AspNetCore.Mvc.Internal
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.FileProviders;

    public class TestHostEnvironment : IWebHostEnvironment
    {
        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
