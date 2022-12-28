namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    // using Licensing;
    using Microsoft.DotNet.PlatformAbstractions;
    using Microsoft.Extensions.DependencyModel;
    using Plugins;
    using Utilities.Extensions;

    internal static class PluginsContainer
    {
        static PluginsContainer()
        {
            DefaultRegistrationPlugins = new HashSet<IDefaultRegistrationPlugin>();
            ServiceRegistrationPlugins = new HashSet<IServiceRegistrationPlugin>();
            RoutingServiceRegistrationPlugins = new HashSet<IRoutingServiceRegistrationPlugin>();
            InitializationPlugins = new HashSet<IInitializationPlugin>();
        }

        public static ISet<IDefaultRegistrationPlugin> DefaultRegistrationPlugins { get; }

        public static ISet<IServiceRegistrationPlugin> ServiceRegistrationPlugins { get; }

        public static ISet<IRoutingServiceRegistrationPlugin> RoutingServiceRegistrationPlugins { get; }

        public static ISet<IInitializationPlugin> InitializationPlugins { get; }

        public static void LoadPlugins(DependencyContext dependencyContext)
        {
            var testFrameworkName = TestFramework.TestFrameworkName;

            var testFrameworkAssemblies = dependencyContext
                .GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier())
                .Where(l => l.Name.StartsWith(testFrameworkName))
                .ToArray();

            //if (testFrameworkAssemblies.Length == 7 && testFrameworkAssemblies.Any(t => t.Name == $"{testFrameworkName}.Lite"))
            //{
            //    TestCounter.SkipValidation = true;
            //}

            var plugins = testFrameworkAssemblies
                .Select(l => Assembly
                    .Load(new AssemblyName(l.Name))
                    .GetType($"{testFrameworkName}.Plugins.{l.Name.Replace(testFrameworkName, string.Empty).Replace(".", string.Empty)}TestPlugin"))
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

        internal static void Reset()
        {
            DefaultRegistrationPlugins.Clear();
            ServiceRegistrationPlugins.Clear();
            RoutingServiceRegistrationPlugins.Clear();
            InitializationPlugins.Clear();
        }
    }
}
