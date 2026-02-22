namespace MyTested.AspNetCore.Mvc.Test.PluginsTests
{
    using Microsoft.AspNetCore.Http;
    using Asp.Versioning;
    using Plugins;
    using Xunit;

    public class VersioningTestPluginTests
    {
        [Fact]
        public void ShouldInvokeMethodOfTypeVoidWithValidServiceCollectionForDefaultRegistration()
        {
            var testPlugin = new VersioningTestPlugin();

            var httpContext = new DefaultHttpContext();

            testPlugin.HttpFeatureRegistrationDelegate(httpContext);

            var versioningFeature = httpContext.Features[typeof(IApiVersioningFeature)];

            Assert.NotNull(versioningFeature);
            Assert.IsType<ApiVersioningFeature>(versioningFeature);
        }
    }
}
