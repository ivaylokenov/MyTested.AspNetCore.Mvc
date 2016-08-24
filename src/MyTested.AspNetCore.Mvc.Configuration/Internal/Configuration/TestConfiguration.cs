namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public class TestConfiguration : BaseConfiguration
    {
        private const string LicenseConfigKey = "License";
        private const string LicensesConfigKey = "Licenses";
        
        private TestConfiguration(IConfiguration configuration)
            : base(configuration)
        {
            this.General = new GeneralTestConfiguration(this.Configuration);
            this.Controllers = new ControllersTestConfiguration(this.Configuration);
        }
        
        public GeneralTestConfiguration General { get; private set; }

        public ControllersTestConfiguration Controllers { get; private set; }

        public IEnumerable<string> Licenses
        {
            get
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
        }

        public static TestConfiguration With(IConfiguration configuration)
        {
            return new TestConfiguration(configuration);
        }
    }
}
