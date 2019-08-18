namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyModel;

    public static class TestFramework
    {
        public const string TestFrameworkName = "MyTested.AspNetCore.Mvc";
        public const string ReleaseDate = "2019-07-01";
        public const string VersionPrefix = "2.1";

        internal static void EnsureCorrectVersion(DependencyContext dependencyContext)
        {
            var aspNetCoreMvcLibraries = dependencyContext
                .RuntimeLibraries
                .Where(l => WebFramework.AspNetCoreMvcLibraries.Contains(l.Name));

            var libraryWithMismatchedVersion = aspNetCoreMvcLibraries.FirstOrDefault(d => !d.Version.StartsWith(VersionPrefix));

            if (libraryWithMismatchedVersion != null)
            {
                throw new InvalidOperationException($"This version of {TestFrameworkName} only supports ASP.NET Core {VersionPrefix} applications but a {libraryWithMismatchedVersion.Version} assembly was referenced - {libraryWithMismatchedVersion.Name}.");
            }
        }
    }
}
