namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class ControllersTestConfiguration : BaseConfiguration
    {
        private const string ModelStateValidationConfigKey = "ModelStateValidation";

        public ControllersTestConfiguration(IConfiguration configuration)
            : base(configuration)
        {
            this.Prefix = "Controllers";
        }

        public bool ModelStateValidation() => this.GetValue(ModelStateValidationConfigKey, true);
    }
}
