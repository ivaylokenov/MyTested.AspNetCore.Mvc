namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentResultTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Setups;
    using Setups.Common;
    using Setups.Models;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewTestBuilderTests
    {
        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngine()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyViewComponent<ViewEngineComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(viewEngine))
                .ShouldReturn()
                .View()
                .WithViewEngine(viewEngine);
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("Test"))
                .ShouldReturn()
                .View("SomeView")
                .WithViewEngineOfType<CompositeViewEngine>();
        }

        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewEngineComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke(null))
                        .ShouldReturn()
                        .View()
                        .WithViewEngine(new CustomViewEngine());
                },
                "When invoking ViewEngineComponent expected view result ViewEngine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngine()
        {
            MyViewComponent<ViewEngineComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(new CustomViewEngine()))
                .ShouldReturn()
                .View()
                .WithViewEngineOfType<CustomViewEngine>();
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewEngineComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke(new CustomViewEngine()))
                        .ShouldReturn()
                        .View()
                        .WithViewEngineOfType<IViewEngine>();
                },
                "When invoking ViewEngineComponent expected view result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngine()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke(null))
                        .ShouldReturn()
                        .View()
                        .WithViewEngineOfType<CustomViewEngine>();
                },
                "When invoking ViewResultComponent expected view result ViewEngine to be of CustomViewEngine type, but instead received CompositeViewEngine.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyView()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithViewEngineOfType<CompositeViewEngine>()
                .AndAlso()
                .WithModelOfType<ResponseModel>();
        }
    }
}
