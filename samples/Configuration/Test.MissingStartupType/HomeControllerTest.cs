namespace Test.MissingStartupType
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void MissingStartupShouldThrowCorrectException()
        {
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                MyController<HomeController>
                    .Instance()
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("TestStartup class could not be found at the root of the test project. Either add it or set 'General:AutomaticStartup' in the test configuration ('testsettings.json' file by default) to 'false'.", exception.InnerException.Message);
        }
    }
}
