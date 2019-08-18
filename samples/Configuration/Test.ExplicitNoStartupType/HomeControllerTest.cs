namespace Test.ExplicitNoStartupType
{
    using MyTested.AspNetCore.Mvc;
    using System;
    using WebApplication.Core;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void ExplicitNoStartupShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => MyApplication.StartsFrom<Startup>());

            Assert.Equal("The test configuration ('testsettings.json' file by default) contained 'true' value for the 'General:NoStartup' option but Startup class was set through the 'StartsFrom<TStartup>()' method. Either do not set the class or change the option to 'false'.", exception.Message);
        }
    }
}
