namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class ServerTestConfiguration
    {
        internal const string DefaultConfigurationFile = "testsettings.json";

        private static IConfigurationBuilder configurationBuilder;
        private static TestConfiguration globalConfiguration;
        private static GeneralTestConfiguration generalConfiguration;

        public static TestConfiguration Global
        {
            get
            {
                if (globalConfiguration == null || AdditionalConfiguration != null)
                {
                    if (configurationBuilder == null)
                    {
                        configurationBuilder = new ConfigurationBuilder()
                            .AddJsonFile(DefaultConfigurationFile, optional: true)
                            .AddJsonFile("testconfig.json", optional: true); // For backwards compatibility.
                    }

                    AdditionalConfiguration?.Invoke(configurationBuilder);
                    AdditionalConfiguration = null;

                    globalConfiguration = TestConfiguration.With(configurationBuilder.Build());
                    generalConfiguration = null;
                }

                return globalConfiguration;
            }
        }

        internal static Action<IConfigurationBuilder> AdditionalConfiguration { get;
            set; }

        internal static GeneralTestConfiguration General
        {
            get
            {
                if (generalConfiguration == null)
                {
                    generalConfiguration = Global.GetGeneralConfiguration();
                }

                return generalConfiguration;
            }
        }

        internal static void Reset()
        {
            configurationBuilder = null;
            globalConfiguration = null;
            generalConfiguration = null;
        }
    }
}
