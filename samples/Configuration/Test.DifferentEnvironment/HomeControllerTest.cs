namespace Test.DifferentEnvironment
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void DifferentEnvironmentShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                MyController<HomeController>
                    .Instance()
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("A public method named 'ConfigureStaging' or 'Configure' could not be found in the 'Test.DifferentEnvironment.StagingStartup' type.", exception.Message);
        }
    }
}
