namespace MyTested.AspNetCore.Mvc.Configuration.Test
{
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class ConfigurationBuilderExtensionsTests
    {
        [Fact]
        public void CallingAddMultipleTimesShouldAddCorrectConfigurationValues()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .Add("FirstKey", "FirstValue")
                .Add("SecondKey", "SecondValue")
                .Add("ThirdKey", "ThirdValue");

            var configuration = configurationBuilder.Build();

            Assert.Equal("FirstValue", configuration.GetValue<string>("FirstKey"));
            Assert.Equal("SecondValue", configuration.GetValue<string>("SecondKey"));
            Assert.Equal("ThirdValue", configuration.GetValue<string>("ThirdKey"));
        }
    }
}
