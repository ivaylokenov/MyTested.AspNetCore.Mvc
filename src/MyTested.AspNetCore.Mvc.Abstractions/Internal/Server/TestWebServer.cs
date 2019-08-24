namespace MyTested.AspNetCore.Mvc.Internal.Server
{
    using System;
    using Configuration;
    using Microsoft.AspNetCore.Hosting;

    public static partial class TestWebServer
    {
        private static IWebHostEnvironment environment;

        internal static IWebHostEnvironment Environment
        {
            get
            {
                if (environment == null)
                {
                    environment = PrepareEnvironment();
                }

                return environment;
            }
        }

        internal static string ApplicationName
            => ServerTestConfiguration.General.ApplicationName ?? WebAssemblyName ?? TestAssemblyName;
        
        private static IWebHostEnvironment PrepareEnvironment()
            => new TestHostEnvironment
            {
                ApplicationName = ApplicationName,
                EnvironmentName = ServerTestConfiguration.General.EnvironmentName,
                ContentRootPath = AppContext.BaseDirectory
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
