namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Reflection;
    using Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        private const string DefaultStartupTypeName = "Startup";

        private static Type startupType;

        internal static Type StartupType
        {
            get => startupType;

            set
            {
                if (value != null && GeneralConfiguration.NoStartup)
                {
                    throw new InvalidOperationException($"The test configuration ('{DefaultConfigurationFile}' file by default) contained 'true' value for the '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.NoStartupKey}' option but {value.GetName()} class was set through the 'StartsFrom<TStartup>()' method. Either do not set the class or change the option to 'false'.");
                }

                if (startupType != null && GeneralConfiguration.AsynchronousTests)
                {
                    throw new InvalidOperationException($"Multiple Startup types per test project while running asynchronous tests is not supported. Either set '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.AsynchronousTestsKey}' in the test configuration ('{DefaultConfigurationFile}' file by default) to 'false' or separate your tests into different test projects. The latter is recommended. If you choose the first option, you may need to disable asynchronous testing in your preferred test runner too.");
                }

                if (initialiazed)
                {
                    Reset();
                }

                startupType = value;
            }
        }

        internal static Type TryFindTestStartupType()
        {
            EnsureTestAssembly();

            var defaultTestStartupType = GeneralConfiguration.StartupType ?? $"{Environment.EnvironmentName}{DefaultStartupTypeName}";

            // Check root of the test project.
            return
                TestAssembly.GetType(defaultTestStartupType) ??
                TestAssembly.GetType($"{TestAssemblyName}.{defaultTestStartupType}");
        }

        internal static Type TryFindWebStartupType()
        {
            if (WebAssembly == null)
            {
                return null;
            }

            // Check root of the test project.
            return
                WebAssembly.GetType(DefaultStartupTypeName) ??
                WebAssembly.GetType($"{WebAssemblyName}.{DefaultStartupTypeName}");
        }

        private static StartupMethods PrepareStartup(IServiceCollection serviceCollection)
        {
            StartupMethods startupMethods = null;
            if (StartupType != null)
            {
                startupMethods = StartupLoader.LoadMethods(
                    serviceCollection.BuildServiceProvider(),
                    StartupType,
                    Environment.EnvironmentName);

                if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(StartupType.GetTypeInfo()))
                {
                    serviceCollection.AddSingleton(typeof(IStartup), StartupType);
                }
                else
                {
                    serviceCollection.AddSingleton(typeof(IStartup), sp => new ConventionBasedStartup(startupMethods));
                }
            }

            return startupMethods;
        }
    }
}
