namespace Test.FullFramework.NoCompilationContext
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void MissingCompilationContextShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                MyController<HomeController>
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("Testing infrastructure could not be loaded. Depending on your project's configuration you may need to set '<PreserveCompilationContext>true</PreserveCompilationContext>' in the test assembly's '.csproj' file.", exception.Message);
        }
    }
}
