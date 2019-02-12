namespace MyTested.AspNetCore.Mvc.Abstractions.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.DependencyModel;

    public static class TestApplicationPartsProvider
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

        public static IEnumerable<AssemblyPart> GetTestAssemblyParts(
            IEnumerable<RuntimeLibrary> projectLibraries,
            string testAssemblyName,
            IEnumerable<string> currentParts)
            => projectLibraries
                .Where(l => l.Name != testAssemblyName)
                .Where(l => l.Dependencies.Select(d => d.Name).Any(d => AspNetCoreMvcLibraries.Contains(d)))
                .Where(l => !currentParts.Contains(l.Name))
                .Select(d => new AssemblyPart(Assembly.Load(new AssemblyName(d.Name))));
    }
}
