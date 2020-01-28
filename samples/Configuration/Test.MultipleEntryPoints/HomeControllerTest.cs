namespace Test.MultipleEntryPoints
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void MultipleEntryPointsShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                MyController<HomeController>
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("Web application WebApplication could not be loaded in the application part manager by convention. You need to set it manually by providing your web project's name in the test configuration's 'General:WebAssemblyName' section ('testsettings.json' file by default).", exception.Message);

        }
    }
}
