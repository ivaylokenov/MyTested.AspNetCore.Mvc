namespace MyTested.AspNetCore.Mvc.Internal.Application
{
    using System;
    using Configuration;
    using Microsoft.Extensions.Configuration;

    public static partial class TestApplication
    {
        private const string DefaultConfigurationFile = "testsettings.json";
        
        private static IConfigurationBuilder configurationBuilder;
        private static TestConfiguration configuration;
        private static GeneralTestConfiguration generalConfiguration;
        
        public static TestConfiguration TestConfiguration
        {
            get
            {
                if (configuration == null || AdditionalConfiguration != null)
                {
                    if (configurationBuilder == null)
                    {
                        configurationBuilder = new ConfigurationBuilder()
                            .AddJsonFile(DefaultConfigurationFile, optional: true)
                            .AddJsonFile("testconfig.json", optional: true); // For backwards compatibility.
                    }

                    AdditionalConfiguration?.Invoke(configurationBuilder);
                    AdditionalConfiguration = null;

                    configuration = TestConfiguration.With(configurationBuilder.Build());
                    generalConfiguration = null;

                    PrepareLicensing();
                }

                return configuration;
            }
        }

        internal static Action<IConfigurationBuilder> AdditionalConfiguration { get; set; }

        internal static GeneralTestConfiguration GeneralConfiguration
        {
            get
            {
                if (generalConfiguration == null)
                {
                    generalConfiguration = TestConfiguration.GetGeneralConfiguration();
                }

                return generalConfiguration;
            }
        }
    }
}
