namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Setups;
    using Setups.ViewComponents;
    using System;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void UsingUrlHelperInsideViewComponentInvocationShouldNotThrowWithEmptyRouteData()
        {
            MyViewComponent<RouteDataComponent>
               .Instance()
               .WithRouteData()
               .InvokedWith(c => c.Invoke())
               .ShouldReturn()
               .Content("/");
        }
        
        [Fact]
        public void UsingUrlHelperInsideViewComponentInvocationShouldNotThrowWithControllerActionRouteData()
        {
            MyViewComponent<RouteDataComponent>
               .Instance()
               .WithRouteData(new
               {
                   Controller = "TestController",
                   Action = "TestAction"
               })
               .InvokedWith(c => c.Invoke())
               .ShouldReturn()
               .Content("/TestController/TestAction");
        }

        [Fact]
        public void UsingUrlHelperInsideViewComponentInvocationShouldNotThrowWithRouteData()
        {
            MyViewComponent<RouteDataComponent>
               .Instance()
               .WithRouteData(new { Explicit = "TestValue" })
               .InvokedWith(c => c.Invoke())
               .ShouldReturn()
               .Content("TestValue");
        }

        [Fact]
        public void UsingUrlHelperInsideViewComponentInvocationShouldThrowWithoutRouteData()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyViewComponent<RouteDataComponent>
                       .Instance()
                       .InvokedWith(c => c.Invoke())
                       .ShouldReturn()
                       .View()
                       .WithModel("/");
                },
                "Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
        }
    }
}
