namespace Test.WrongTestAssembly
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void WrongTestAssemblyShouldThrowCorrectException()
        {
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                MyController<HomeController>
                    .Instance()
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("Test assembly could not be loaded. The provided 'WrongTestAssembly' name in the 'General:TestAssemblyName' configuration is not valid.", exception.InnerException?.InnerException?.Message);
        }
    }
}
