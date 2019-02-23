namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Server;
    using Services;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        private const string DefaultStartupTypeName = "Startup";

        private static Type startupType;
        private static StartupMethods startupMethods;
        
        private static MethodInfo findMethod;
        
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

                if (startupType != null && startupType != value && generalConfiguration.AsynchronousTests)
                {
                    throw new InvalidOperationException($"Multiple Startup types per test project while running asynchronous tests is not supported. Either set '{GeneralTestConfiguration.PrefixKey}.{GeneralTestConfiguration.AsynchronousTestsKey}' in the test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) to 'false' or separate your tests into different test projects. The latter is recommended. If you choose the first option, you may need to disable asynchronous testing in your preferred test runner too.");
                }
                
                Reset();

                startupType = value;
            }
        }

        private static MethodInfo FindMethodDelegate
        {
            get
            {
                if (findMethod == null)
                {
                    // Calling the internal StartupLoader method to prevent
                    // copy-pasted code from the ASP.NET Core source code.
                    findMethod = typeof(StartupLoader).GetMethod(
                        "FindMethod",
                        BindingFlags.NonPublic | BindingFlags.Static);
                }

                return findMethod;
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
                // Startup static constructor may have server services configuration.
                if (StartupType.TypeInitializer != null)
                {
                    // Guarantees the static constructor of the Startup type is called only once.
                    RuntimeHelpers.RunClassConstructor(StartupType.TypeHandle);
                }
                
                TestWebServer.AdditionalServices?.Invoke(serviceCollection);

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

        private static MethodInfo GetConfigureContainerMethod()
        {
            if (startupMethods == null)
            {
                return null;
            }

            return FindMethodDelegate.Invoke(null, new object[]
            {
                StartupType,
                "Configure{0}Container",
                TestWebServer.Environment.EnvironmentName,
                typeof(void),
                false
            }) as MethodInfo;
        } 
    }
}
