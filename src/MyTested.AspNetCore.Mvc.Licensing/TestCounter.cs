namespace MyTested.AspNetCore.Mvc.Licensing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    internal static class TestCounter
    {
        private const int MaximumAllowedAssertions = 500;

        private static readonly object Sync;

        private static long totalAssertions;
        private static bool licensesValidated;
        private static string invalidLicenseMessage;

        private static IEnumerable<string> licenses;
        private static DateTime releaseDate;
        private static string projectNamespace;

        static TestCounter()
        {
            Sync = new object();
        }

        public static void IncrementAndValidate()
        {
            if (LicenseValidator.HasValidLicense)
            {
                return;
            }
            
            Interlocked.Increment(ref totalAssertions);

            if (!licensesValidated && totalAssertions > MaximumAllowedAssertions)
            {
                lock (Sync)
                {
                    if (!licensesValidated)
                    {
                        try
                        {
                            LicenseValidator.Validate(licenses, releaseDate, projectNamespace);
                            licensesValidated = true;
                        }
                        catch (InvalidLicenseException ex)
                        {
                            invalidLicenseMessage = $"{(licenses == null || !licenses.Any() ? string.Empty : $"You have invalid license: '{ex.Message}'. ")}The free-quota limit of {MaximumAllowedAssertions} assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.";
                        }

                        if (!LicenseValidator.HasValidLicense)
                        {
                            throw new InvalidLicenseException(invalidLicenseMessage);
                        }
                    }
                }
            }
        }

        public static void SetLicenseData(IEnumerable<string> registeredLicenses, DateTime packageReleaseDate, string testProjectNamespace)
        {
            licenses = registeredLicenses;
            releaseDate = packageReleaseDate;
            projectNamespace = testProjectNamespace;
            licensesValidated = false;
            totalAssertions = 0;
        }
    }
}
