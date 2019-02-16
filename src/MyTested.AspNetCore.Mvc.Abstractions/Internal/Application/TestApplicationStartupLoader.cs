namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Reflection;
    using Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Server;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        private const string DefaultStartupTypeName = "Startup";

        private static Type startupType;
        private static StartupMethods startupMethods;

        internal static Type StartupType
        {
            get => startupType;

            set
            {
                var generalConfiguration = ServerTestConfiguration.General;

                if (value != null && generalConfiguration.NoStartup)
                {
                    throw new InvalidOperationException($"The test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) contained 'true' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but {value.GetName()} class was set through the 'StartsFrom<TStartup>()' method. Either do not set the class or change the option to 'false'.");
                }

                if (startupType != null && generalConfiguration.AsynchronousTests)
                {
                    throw new InvalidOperationException($"Multiple Startup types per test project while running asynchronous tests is not supported. Either set '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.AsynchronousTestsKey}' in the test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) to 'false' or separate your tests into different test projects. The latter is recommended. If you choose the first option, you may need to disable asynchronous testing in your preferred test runner too.");
                }
                
                Reset();

                startupType = value;
            }
        }

        internal static Type TryFindTestStartupType()
        {
            TestWebServer.EnsureTestAssembly();

            var defaultTestStartupType = ServerTestConfiguration.General.StartupType 
                ?? $"{TestWebServer.Environment.EnvironmentName}{DefaultStartupTypeName}";

            // Check root of the test project.
            return
                TestWebServer.TestAssembly.GetType(defaultTestStartupType) ??
                TestWebServer.TestAssembly.GetType($"{TestWebServer.TestAssemblyName}.{defaultTestStartupType}");
        }

        internal static Type TryFindWebStartupType()
        {
            if (TestWebServer.WebAssembly == null)
            {
                return null;
            }

            // Check root of the test project.
            return
                TestWebServer.WebAssembly.GetType(DefaultStartupTypeName) ??
                TestWebServer.WebAssembly.GetType($"{TestWebServer.WebAssemblyName}.{DefaultStartupTypeName}");
        }

        private static void PrepareStartup(IServiceCollection serviceCollection)
        {
            if (StartupType != null)
            {
                startupMethods = StartupLoader.LoadMethods(
                    serviceCollection.BuildServiceProviderFromFactory(),
                    StartupType,
                    TestWebServer.Environment.EnvironmentName);

                if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(StartupType.GetTypeInfo()))
                {
                    serviceCollection.AddSingleton(typeof(IStartup), StartupType);
                }
                else
                {
                    serviceCollection.AddSingleton(typeof(IStartup), sp => new ConventionBasedStartup(startupMethods));
                }
            }
        }

        private static bool HasConfigureServicesIServiceProviderDelegate()
        {
            if (StartupType == null)
            {
                return false;
            }

            // Calling the internal StartupLoader.HasConfigureServicesIServiceProviderDelegate
            // method to prevent copy-pasted code from the ASP.NET Core source code.
            var findMethod = typeof(StartupLoader).GetMethod(
                "FindMethod",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (findMethod != null)
            {
                return null != findMethod.Invoke(null, new object[]
                {
                    StartupType,
                    "Configure{0}Services",
                    TestWebServer.Environment.EnvironmentName,
                    typeof(IServiceProvider),
                    false
                });
            }

            return true;
        }
    }
}
