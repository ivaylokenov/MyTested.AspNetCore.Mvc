namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyModel;

    public static class TestFramework
    {
        public const string TestFrameworkName = "MyTested.AspNetCore.Mvc";
        public const string ReleaseDate = "2023-01-01";
        public const string VersionPrefix = "7.0";

        internal static void EnsureCorrectVersion(DependencyContext dependencyContext)
        {
            var aspNetCoreMetaPackage = dependencyContext
                .RuntimeLibraries
                .FirstOrDefault(l => l.Name.StartsWith(WebFramework.AspNetCoreMetaPackageName));

            if (aspNetCoreMetaPackage == null || !aspNetCoreMetaPackage.Version.StartsWith(VersionPrefix))
            {
                throw new InvalidOperationException($"This version of {TestFrameworkName} only supports ASP.NET Core {VersionPrefix} applications but {(aspNetCoreMetaPackage == null ? "no" : $"the {aspNetCoreMetaPackage.Version}")} web framework was referenced.");
            }
        }
    }
}
