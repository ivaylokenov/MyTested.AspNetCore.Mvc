namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class ControllersTestConfiguration : BaseConfiguration
    {
        internal const string PrefixKey = "Controllers";

        internal const string ModelStateValidationKey = "ModelStateValidation";

        public ControllersTestConfiguration(IConfiguration configuration)
            : base(configuration)
        {
        }
        
        protected override string Prefix => PrefixKey;

        public bool ModelStateValidation => this.GetValue(ModelStateValidationKey, true);
    }
}
