namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class WebFramework
    {
        // Copied from the ASP.NET Core source code.
        internal static readonly HashSet<string> AspNetCoreMvcLibraries = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Microsoft.AspNetCore.App",
            "Microsoft.AspNetCore.Mvc",
            "Microsoft.AspNetCore.Mvc.Abstractions",
            "Microsoft.AspNetCore.Mvc.Core",
            "Microsoft.AspNetCore.Mvc.ViewFeatures"
        };

        internal static string AspNetCoreMetaPackageName => AspNetCoreMvcLibraries.First();
    }
}
