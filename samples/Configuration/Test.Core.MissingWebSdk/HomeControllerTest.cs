namespace Test.Core.MissingWebSdk
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void MissingSdkShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                MyController<HomeController>
                    .Instance()
                    .Calling(c => c.Index())
                    .ShouldReturn()
                    .View();
            });

            Assert.Equal("HomeController is not recognized as a valid controller type. Make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file. If your controller is still not recognized, you may manually add it in the application part manager by using the 'AddMvc().PartManager.ApplicationParts.Add(applicationPart))' method.", exception.Message);
        }
    }
}
