namespace Test.WrongStartupType
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void WrongStartupShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                MyController<HomeController>
                    .Instance()
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("WrongStartup class could not be found. The provided 'WrongStartup' name in the 'General:StartupType' configuration is not valid.", exception.Message);
        }
    }
}
