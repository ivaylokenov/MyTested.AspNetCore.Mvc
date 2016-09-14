namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public class TestConfiguration : BaseConfiguration
    {
        private const string LicenseConfigKey = "License";
        private const string LicensesConfigKey = "Licenses";

        private readonly IDictionary<Type, BaseConfiguration> configurationTypes;

        private TestConfiguration(IConfiguration configuration)
            : base(configuration)
        {
            this.configurationTypes = new Dictionary<Type, BaseConfiguration>();
        }

        public GeneralTestConfiguration General()
            => this.GetConfiguration(() => new GeneralTestConfiguration(this.Configuration));
        
        public IEnumerable<string> Licenses()
        {
            var license = this.Configuration[LicenseConfigKey];
            if (license != null)
            {
                return new[] { license };
            }

            var licenses = new List<string>();
            this.Configuration.GetSection(LicensesConfigKey).Bind(licenses);
            return licenses;
        }

        public TConfiguration GetConfiguration<TConfiguration>(Func<TConfiguration> configurationFactory)
            where TConfiguration : BaseConfiguration
        {
            var typeOfConfiguration = typeof(TConfiguration);
            if (!this.configurationTypes.ContainsKey(typeOfConfiguration))
            {
                this.configurationTypes[typeOfConfiguration] = configurationFactory();
            }

            return (TConfiguration)this.configurationTypes[typeOfConfiguration];
        }

        public static TestConfiguration With(IConfiguration configuration)
        {
            return new TestConfiguration(configuration);
        }
    }
}
