namespace Test.WrongWebAssembly
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

            Assert.Equal("Web assembly could not be loaded. The provided 'WrongWebAssembly' name in the 'General:WebAssemblyName' configuration is not valid.", exception.InnerException.Message);
        }
    }
}
