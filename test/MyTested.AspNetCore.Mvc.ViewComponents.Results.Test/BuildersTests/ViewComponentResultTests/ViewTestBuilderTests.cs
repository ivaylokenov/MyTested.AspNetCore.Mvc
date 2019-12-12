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
        public void ShouldReturnViewWithDefaultNameShouldNotThrowExceptionWithCorrectName()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke(null))
                .ShouldReturn()
                .View(view => view
                    .WithDefaultName());
        }

        [Fact]
        public void ShouldReturnViewWithDefaultNameShouldThrowExceptionWithDifferentName()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke("custom"))
                        .ShouldReturn()
                        .View(view => view
                            .WithDefaultName());
                },
                "When invoking ViewResultComponent expected view result to be the default one, but instead received 'Custom'.");
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("custom"))
                .ShouldReturn()
                .View(view => view
                    .WithName("Custom"));
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke("custom"))
                        .ShouldReturn()
                        .View(view => view
                            .WithName("Incorrect"));
                },
                "When invoking ViewResultComponent expected view result to be 'Incorrect', but instead received 'Custom'.");
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngine()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyViewComponent<ViewEngineComponent>
                .InvokedWith(c => c.Invoke(viewEngine))
                .ShouldReturn()
                .View(view => view
                    .WithViewEngine(viewEngine));
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngineForGeneric()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("Test"))
                .ShouldReturn()
                .View(view => view
                    .WithName("SomeView")
                    .WithViewEngineOfType<CompositeViewEngine>());
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("Test"))
                .ShouldReturn()
                .View("SomeView")
                .WithViewEngineOfType(typeof(CompositeViewEngine));
        }

        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewEngineComponent>
                        .InvokedWith(c => c.Invoke(null))
                        .ShouldReturn()
                        .View(view => view
                            .WithViewEngine(new CustomViewEngine()));
                },
                "When invoking ViewEngineComponent expected view result ViewEngine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngineForGeneric()
        {
            MyViewComponent<ViewEngineComponent>
                .InvokedWith(c => c.Invoke(new CustomViewEngine()))
                .ShouldReturn()
                .View(view => view
                    .WithViewEngineOfType<CustomViewEngine>());
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngine()
        {
            MyViewComponent<ViewEngineComponent>
                .InvokedWith(c => c.Invoke(new CustomViewEngine()))
                .ShouldReturn()
                .View()
                .WithViewEngineOfType(typeof(CustomViewEngine));
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngineForGeneric()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewEngineComponent>
                        .InvokedWith(c => c.Invoke(new CustomViewEngine()))
                        .ShouldReturn()
                        .View(view => view
                            .WithViewEngineOfType<IViewEngine>());
                },
                "When invoking ViewEngineComponent expected view result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewEngineComponent>
                        .InvokedWith(c => c.Invoke(new CustomViewEngine()))
                        .ShouldReturn()
                        .View()
                        .WithViewEngineOfType(typeof(IViewEngine));
                },
                "When invoking ViewEngineComponent expected view result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngineForGeneric()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke(null))
                        .ShouldReturn()
                        .View(view => view
                            .WithViewEngineOfType<CustomViewEngine>());
                },
                "When invoking ViewResultComponent expected view result ViewEngine to be of CustomViewEngine type, but instead received CompositeViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngine()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke(null))
                        .ShouldReturn()
                        .View()
                        .WithViewEngineOfType(typeof(CustomViewEngine));
                },
                "When invoking ViewResultComponent expected view result ViewEngine to be of CustomViewEngine type, but instead received CompositeViewEngine.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyForGeneric()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View(view => view
                    .WithName("SomeView")
                    .WithViewEngineOfType<CompositeViewEngine>()
                    .AndAlso()
                    .WithModelOfType<ResponseModel>());
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithViewEngineOfType(typeof(CompositeViewEngine))
                .AndAlso()
                .WithModelOfType(typeof(ResponseModel));
        }
    }
}
