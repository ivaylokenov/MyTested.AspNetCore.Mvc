namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Abstractions.Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    public static partial class TestApplication
    {
        // Copied from the ASP.NET Core source code.
        private static readonly HashSet<string> AspNetCoreMvcLibraries = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // The deps file for the Microsoft.AspNetCore.App shared runtime is authored in a way where it does not say
            // it depends on Microsoft.AspNetCore.Mvc even though it does. Explicitly list it so that referencing this runtime causes
            // assembly discovery to work correctly.
            "Microsoft.AspNetCore.App",
            "Microsoft.AspNetCore.Mvc",
            "Microsoft.AspNetCore.Mvc.Abstractions",
            "Microsoft.AspNetCore.Mvc.ApiExplorer",
            "Microsoft.AspNetCore.Mvc.Core",
            "Microsoft.AspNetCore.Mvc.Cors",
            "Microsoft.AspNetCore.Mvc.DataAnnotations",
            "Microsoft.AspNetCore.Mvc.Formatters.Json",
            "Microsoft.AspNetCore.Mvc.Formatters.Xml",
            "Microsoft.AspNetCore.Mvc.Localization",
            "Microsoft.AspNetCore.Mvc.NewtonsoftJson",
            "Microsoft.AspNetCore.Mvc.Razor",
            "Microsoft.AspNetCore.Mvc.RazorPages",
            "Microsoft.AspNetCore.Mvc.TagHelpers",
            "Microsoft.AspNetCore.Mvc.ViewFeatures"
        };

        private static string AspNetCoreMetaPackageName => AspNetCoreMvcLibraries.First();

        public static IEnumerable<AssemblyPart> GetTestAssemblyParts(IEnumerable<string> currentParts)
            => ProjectLibraries
                .Where(l => l.Name != TestAssemblyName)
                .Where(l => l.Dependencies.Select(d => d.Name).Any(d => AspNetCoreMvcLibraries.Contains(d)))
                .Where(l => !currentParts.Contains(l.Name))
                .Select(d => new AssemblyPart(Assembly.Load(new AssemblyName(d.Name))));

        private static void EnsureApplicationParts(IServiceCollection serviceCollection)
        {
            var baseStartupTypeAssembly = WebAssembly;
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

            var applicationPartManager = (ApplicationPartManager)serviceCollection
                .FirstOrDefault(t => t.ServiceType == typeof(ApplicationPartManager))
                ?.ImplementationInstance;

            if (applicationPartManager != null && baseStartupTypeAssembly != null)
            {
                var baseStartupTypeAssemblyName = baseStartupTypeAssembly.GetShortName();

                if (applicationPartManager.ApplicationParts.All(a => a.Name != baseStartupTypeAssemblyName))
                {
                    throw new InvalidOperationException($"Web application {baseStartupTypeAssemblyName} could not be loaded correctly. Make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file. Additionally, if your web project references the '{AspNetCoreMetaPackageName}' package, you need to reference it in your test project too.");
                }

                if (GeneralConfiguration.AutomaticApplicationParts)
                {
                    var currentApplicationParts = applicationPartManager
                        .ApplicationParts
                        .Select(a => a.Name);

                    var testAssemblyParts = GetTestAssemblyParts(currentApplicationParts);

                    testAssemblyParts.ForEach(ap => applicationPartManager.ApplicationParts.Add(ap));
                }
            }
        }
    }
}
