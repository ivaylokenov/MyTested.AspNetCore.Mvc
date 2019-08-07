namespace MyTested.AspNetCore.Mvc.Test.InternalTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups;
    using Setups.Controllers;
    using Setups.ViewComponents;
    using System;
    using Xunit;

    public class ComponentTestContextViewDataExtensionsTests
    {
        [Fact]
        public void GetViewDataShouldThrowInvalidOperationExceptionWithPocoViewComponent()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyViewComponent<PocoViewComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .NoViewData()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                }, 
                "ViewDataDictionary could not be found on the provided PocoViewComponent. The property should be specified manually by providing component instance or using the specified helper methods.");
        }

        [Fact]
        public void GetViewDataShouldHaveNoViewDataForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldHave()
                .NoViewData()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
