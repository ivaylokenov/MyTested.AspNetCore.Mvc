namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ViewTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class ViewTestBuilderTests
    {
        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngine()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewWithViewEngine(viewEngine))
                .ShouldReturn()
                .View(view => view
                    .WithViewEngine(viewEngine));
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View(view => view
                    .WithViewEngine(null));
        }
        
        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.ViewWithViewEngine(null))
                        .ShouldReturn()
                        .View(view => view
                            .WithViewEngine(new CustomViewEngine()));
                },
                "When calling ViewWithViewEngine action in MvcController expected view result engine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngine()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewWithViewEngine(new CustomViewEngine()))
                .ShouldReturn()
                .View(view => view
                    .WithViewEngineOfType<CustomViewEngine>());
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewWithViewEngine(new CustomViewEngine()))
                        .ShouldReturn()
                        .View(view => view
                            .WithViewEngineOfType<IViewEngine>());
                },
                "When calling ViewWithViewEngine action in MvcController expected view result engine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.DefaultView())
                        .ShouldReturn()
                        .View(view => view
                            .WithViewEngineOfType<CustomViewEngine>());
                },
                "When calling DefaultView action in MvcController expected view result engine to be of CustomViewEngine type, but instead received null.");
        }
        
        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngineForPartials()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.PartialViewWithViewEngine(viewEngine))
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithViewEngine(viewEngine));
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngineForPartials()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithViewEngine(null));
        }

        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngineForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.PartialViewWithViewEngine(null))
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithViewEngine(new CustomViewEngine()));
                },
                "When calling PartialViewWithViewEngine action in MvcController expected partial view result engine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngineForPartials()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.PartialViewWithViewEngine(new CustomViewEngine()))
                .ShouldReturn()
                .PartialView(partialView => partialView
                    .WithViewEngineOfType<CustomViewEngine>());
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngineForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.PartialViewWithViewEngine(new CustomViewEngine()))
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithViewEngineOfType<IViewEngine>());
                },
                "When calling PartialViewWithViewEngine action in MvcController expected partial view result engine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngineForPartials()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.DefaultPartialView())
                        .ShouldReturn()
                        .PartialView(partialView => partialView
                            .WithViewEngineOfType<CustomViewEngine>());
                },
                "When calling DefaultPartialView action in MvcController expected partial view result engine to be of CustomViewEngine type, but instead received null.");
        }
    }
}
