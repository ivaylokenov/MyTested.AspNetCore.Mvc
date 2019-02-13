namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Licensing;
    using Microsoft.DotNet.PlatformAbstractions;
    using Microsoft.Extensions.DependencyModel;
    using MyTested.AspNetCore.Mvc.Utilities.Extensions;
    using Plugins;

    public static partial class TestApplication
    {
        private static readonly ISet<IDefaultRegistrationPlugin> DefaultRegistrationPlugins;
        private static readonly ISet<IServiceRegistrationPlugin> ServiceRegistrationPlugins;
        private static readonly ISet<IRoutingServiceRegistrationPlugin> RoutingServiceRegistrationPlugins;
        private static readonly ISet<IInitializationPlugin> InitializationPlugins;
        
        internal static void LoadPlugins()
        {
            var testFrameworkAssemblies = GetDependencyContext()
                .GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier())
                .Where(l => l.Name.StartsWith(TestFrameworkName))
                .ToArray();

            if (testFrameworkAssemblies.Length == 7 && testFrameworkAssemblies.Any(t => t.Name == $"{TestFrameworkName}.Lite"))
            {
                TestCounter.SkipValidation = true;
            }

            var plugins = testFrameworkAssemblies
                .Select(l => Assembly
                    .Load(new AssemblyName(l.Name))
                    .GetType($"{TestFrameworkName}.Plugins.{l.Name.Replace(TestFrameworkName, string.Empty).Trim('.')}TestPlugin"))
                .Where(p => p != null)
                .ToArray();

            if (!plugins.Any())
            {
                throw new InvalidOperationException("Test plugins could not be loaded. Depending on your project's configuration you may need to set '<PreserveCompilationContext>true</PreserveCompilationContext>' in the test assembly's '.csproj' file and/or may need to call '.StartsFrom<TStartup>().WithTestAssembly(this)'.");
            }

            plugins.ForEach(t =>
            {
                var plugin = Activator.CreateInstance(t);

                if (plugin is IDefaultRegistrationPlugin defaultRegistrationPlugin)
                {
                    DefaultRegistrationPlugins.Add(defaultRegistrationPlugin);
                }

                if (plugin is IServiceRegistrationPlugin servicePlugin)
                {
                    ServiceRegistrationPlugins.Add(servicePlugin);
                }

                if (plugin is IRoutingServiceRegistrationPlugin routingServicePlugin)
                {
                    RoutingServiceRegistrationPlugins.Add(routingServicePlugin);
                }

                if (plugin is IInitializationPlugin initializationPlugin)
                {
                    InitializationPlugins.Add(initializationPlugin);
                }

                if (plugin is IHttpFeatureRegistrationPlugin httpFeatureRegistrationPlugin)
                {
                    TestHelper.HttpFeatureRegistrationPlugins.Add(httpFeatureRegistrationPlugin);
                }

                if (plugin is IShouldPassForPlugin shouldPassForPlugin)
                {
                    TestHelper.ShouldPassForPlugins.Add(shouldPassForPlugin);
                }
            });
        }
    }
}
