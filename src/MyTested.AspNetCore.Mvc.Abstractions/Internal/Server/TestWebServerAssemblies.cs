namespace MyTested.AspNetCore.Mvc.Internal.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Configuration;
    using Microsoft.Extensions.DependencyModel;
    using Utilities.Extensions;

    public static partial class TestWebServer
    {
        private static DependencyContext dependencyContext;
        private static IEnumerable<RuntimeLibrary> projectLibraries;

        private static Assembly testAssembly;
        private static Assembly webAssembly;
        private static bool testAssemblyScanned;
        private static bool webAssemblyScanned;
        private static string testAssemblyName;
        private static string webAssemblyName;

        internal static Assembly TestAssembly
        {
            get
            {
                if (testAssembly == null)
                {
                    TryFindTestAssembly();
                }

                return testAssembly;
            }

            set => testAssembly = value;
        }

        internal static Assembly WebAssembly
        {
            get
            {
                if (webAssembly == null)
                {
                    TryFindWebAssembly();
                }

                return webAssembly;
            }

            set => webAssembly = value;
        }

        internal static string TestAssemblyName
        {
            get
            {
                if (testAssemblyName == null)
                {
                    if (testAssembly != null)
                    {
                        testAssemblyName = testAssembly.GetShortName();
                    }
                    else
                    {
                        TryFindTestAssembly();
                    }
                }

                return testAssemblyName;
            }
        }

        internal static string WebAssemblyName
        {
            get
            {
                if (webAssemblyName == null)
                {
                    if (webAssembly != null)
                    {
                        webAssemblyName = webAssembly.GetShortName();
                    }
                    else
                    {
                        TryFindWebAssembly();
                    }
                }

                return webAssemblyName;
            }
        }

        internal static IEnumerable<RuntimeLibrary> ProjectLibraries
        {
            get
            {
                if (projectLibraries == null)
                {
                    projectLibraries = GetDependencyContext()
                        .RuntimeLibraries
                        .Where(l => l.Type == "project");
                }

                return projectLibraries;
            }
        }

        internal static DependencyContext GetDependencyContext()
        {
            if (dependencyContext == null)
            {
                dependencyContext = DependencyContext.Load(TestAssembly);
                if (dependencyContext == null)
                {
                    throw new InvalidOperationException("Testing infrastructure could not be loaded. Depending on your project's configuration you may need to set '<PreserveCompilationContext>true</PreserveCompilationContext>' in the test assembly's '.csproj' file.");
                }
            }

            return dependencyContext;
        }

        internal static void TryFindTestAssembly()
        {
            if (testAssembly != null || testAssemblyScanned)
            {
                return;
            }

            var testAssemblyNameFromConfiguration = ServerTestConfiguration.General.TestAssemblyName;
            if (testAssemblyNameFromConfiguration != null)
            {
                try
                {
                    testAssemblyName = testAssemblyNameFromConfiguration;
                    testAssembly = Assembly.Load(new AssemblyName(testAssemblyName));
                }
                catch
                {
                    throw new InvalidOperationException($"Test assembly could not be loaded. The provided '{testAssemblyName}' name in the '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.TestAssemblyNameKey}' configuration is not valid.");
                }
            }
            else
            {
                try
                {
                    // Using default dependency context since test assembly is still not loaded.
                    var defaultDependencyContext = DependencyContext.Default;
                    if (defaultDependencyContext != null)
                    {
                        testAssemblyName = defaultDependencyContext
                            .GetDefaultAssemblyNames()
                            .First()
                            .Name;

                        testAssembly = Assembly.Load(testAssemblyName);
                    }
                    else
                    {
                        // Dependency context could not be loaded - fallback to analyze the current application domain.
                        testAssembly = AppDomain
                            .CurrentDomain
                            .GetAssemblies()
                            .First(a => a.GetReferencedAssemblies()
                                .Any(r => r.Name.Contains(TestFramework.TestFrameworkName)));

                        testAssemblyName = testAssembly.GetName().Name;
                    }
                }
                catch
                {
                    // Intentional silent fail.
                }
            }

            testAssemblyScanned = true;
        }

        internal static void TryFindWebAssembly()
        {
            if (webAssembly != null || webAssemblyScanned)
            {
                return;
            }

            var webAssemblyNameFromConfiguration = ServerTestConfiguration.General.WebAssemblyName;
            if (webAssemblyNameFromConfiguration != null)
            {
                try
                {
                    webAssemblyName = webAssemblyNameFromConfiguration;
                    webAssembly = Assembly.Load(new AssemblyName(webAssemblyName));
                }
                catch
                {
                    throw new InvalidOperationException($"Web assembly could not be loaded. The provided '{webAssemblyName}' name in the '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.WebAssemblyNameKey}' configuration is not valid.");
                }
            }
            else
            {
                try
                {
                    var testLibrary = ProjectLibraries.First();
                    var dependencies = testLibrary.Dependencies;

                    // Search for a single dependency of the test project which starts with the same namespace.
                    var dependenciesWithSameNamespace = dependencies
                        .Where(d => d.Name.StartsWith(testLibrary.Name.Split('.').First()))
                        .ToList();

                    if (dependenciesWithSameNamespace.Count == 1)
                    {
                        webAssemblyName = dependenciesWithSameNamespace.First().Name;
                        webAssembly = Assembly.Load(webAssemblyName);
                    }
                    else
                    {
                        // Fallback to search for a single dependency of the test project which has an entry Main method.
                        var dependenciesWithEntryPoint = ProjectLibraries
                            .Select(a => a.Name)
                            .Intersect(dependencies.Select(d => d.Name))
                            .Select(l => Assembly.Load(new AssemblyName(l)))
                            .Where(a => a.EntryPoint != null)
                            .ToList();

                        if (dependenciesWithEntryPoint.Count == 1)
                        {
                            webAssemblyName = dependenciesWithEntryPoint.First().GetShortName();
                            webAssembly = Assembly.Load(webAssemblyName);
                        }
                    }
                }
                catch
                {
                    // Intentional silent fail.
                }
            }

            webAssemblyScanned = true;
        }
        
        internal static void EnsureTestAssembly()
        {
            if (TestAssembly == null)
            {
                throw new InvalidOperationException($"Test assembly could not be loaded. You can specify it explicitly in the test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) by providing a value for the '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.TestAssemblyNameKey}' option or set it by calling '.StartsFrom<TStartup>().WithTestAssembly(this)'.");
            }
        }
    }
}
