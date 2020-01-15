namespace MyTested.AspNetCore.Mvc.Internal.Server
{
    using System;
    using Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.FileProviders;

    public static partial class TestWebServer
    {
        private static IWebHostEnvironment environment;

        internal static IWebHostEnvironment Environment => environment ??= PrepareEnvironment();

        internal static string ApplicationName
            => ServerTestConfiguration.General.ApplicationName ?? WebAssemblyName ?? TestAssemblyName;
        
        private static IWebHostEnvironment PrepareEnvironment()
            => new TestHostEnvironment
            {
                ApplicationName = ApplicationName,
                EnvironmentName = ServerTestConfiguration.General.EnvironmentName,
                ContentRootPath = AppContext.BaseDirectory,
                WebRootFileProvider = new NullFileProvider()
            };

        internal static void ResetConfigurationAndAssemblies()
        {
            dependencyContext = null;
            projectLibraries = null;
            testAssembly = null;
            webAssembly = null;
            testAssemblyScanned = false;
            webAssemblyScanned = false;
            testAssemblyName = null;
            webAssemblyName = null;
            environment = null;
            ServerTestConfiguration.Reset();
        }

        internal static void ResetSetup() 
            => AdditionalServices = null;
    }
}
