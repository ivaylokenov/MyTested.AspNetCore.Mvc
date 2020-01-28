namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Configuration;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.DependencyInjection;
    using Server;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        private static void EnsureApplicationParts(IServiceProvider applicationServiceProvider)
        {
            var baseStartupTypeAssembly = TestWebServer.WebAssembly;
            if (baseStartupTypeAssembly == null)
            {
                var baseStartupType = StartupType;
                while (baseStartupType != null && baseStartupType.BaseType != typeof(object))
                {
                    baseStartupType = baseStartupType.BaseType;
                }

                if (baseStartupType != null)
                {
                    baseStartupTypeAssembly = baseStartupType.GetTypeInfo().Assembly;
                }
            }
            
            var applicationPartManager = applicationServiceProvider.GetService<ApplicationPartManager>();
            
            if (applicationPartManager != null && baseStartupTypeAssembly != null)
            {
                var baseStartupTypeAssemblyName = baseStartupTypeAssembly.GetShortName();

                if (applicationPartManager.ApplicationParts.All(a => a.Name != baseStartupTypeAssemblyName))
                {
                    throw new InvalidOperationException($"Web application {baseStartupTypeAssemblyName} could not be loaded in the application part manager by convention. You need to set it manually by providing your web project's name in the test configuration's '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.WebAssemblyNameKey}' section ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default).");
                }

                if (ServerTestConfiguration.General.AutomaticApplicationParts)
                {
                    var currentApplicationParts = applicationPartManager
                        .ApplicationParts
                        .Select(a => a.Name);

                    var testAssemblyParts = GetTestAssemblyParts(currentApplicationParts);

                    testAssemblyParts.ForEach(ap => applicationPartManager.ApplicationParts.Add(ap));
                }
            }
        }
        
        private static IEnumerable<AssemblyPart> GetTestAssemblyParts(IEnumerable<string> currentParts)
            => TestWebServer.ProjectLibraries
                .Where(l => l.Name != TestWebServer.TestAssemblyName)
                .Where(l => l.Dependencies.Select(d => d.Name).Any(d => WebFramework.AspNetCoreMvcLibraries.Contains(d)))
                .Where(l => !currentParts.Contains(l.Name))
                .Select(d => new AssemblyPart(Assembly.Load(new AssemblyName(d.Name))));
    }
}
