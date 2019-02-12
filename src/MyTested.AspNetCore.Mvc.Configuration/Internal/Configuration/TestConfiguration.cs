namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using Microsoft.Extensions.Configuration;

    public class TestConfiguration : BaseConfiguration
    {
        internal const string LicenseKey = "License";
        internal const string LicensesKey = "Licenses";

        private readonly ConcurrentDictionary<Type, BaseConfiguration> configurationTypes;

        private TestConfiguration(IConfiguration configuration)
            : base(configuration) 
            => this.configurationTypes = new ConcurrentDictionary<Type, BaseConfiguration>();

        public IEnumerable<string> Licenses
        {
            get
            {
                var license = this.Configuration[LicenseKey];
                if (license != null)
                {
                    return new[] { license };
                }

                var licenses = new List<string>();
                this.Configuration.GetSection(LicensesKey).Bind(licenses);
                return licenses;
            }
        }

        public TConfiguration GetConfiguration<TConfiguration>(Func<TConfiguration> configurationFactory)
            where TConfiguration : BaseConfiguration
            => (TConfiguration)this.configurationTypes
                .GetOrAdd(typeof(TConfiguration), _ => configurationFactory());

        public static TestConfiguration With(IConfiguration configuration) 
            => new TestConfiguration(configuration);
    }
}
