namespace Test.NoStartupType
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void NoStartupShouldThrowCorrectException()
        {
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                MyController<HomeController>
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("The test configuration ('testsettings.json' file by default) contained 'true' value for the 'General:NoStartup' option but TestStartup class was located at the root of the project. Either remove the class or change the option to 'false'.", exception.InnerException.Message);
        }
    }
}
