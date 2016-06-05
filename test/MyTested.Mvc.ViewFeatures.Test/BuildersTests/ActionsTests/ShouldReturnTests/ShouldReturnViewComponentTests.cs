namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnViewComponentTests
    {
        [Fact]
        public void ShouldReturnViewComponentShouldNotThrowExceptionWithViewComponent()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent();
        }

        [Fact]
        public void ShouldReturnViewComponentShouldThrowExceptionWithIncorrectViewComponent()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .ViewComponent();
                },
                "When calling BadRequestAction action in MvcController expected action result to be ViewComponentResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnViewComponentShouldNotThrowExceptionWithCorrectViewComponentName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent("TestComponent");
        }

        [Fact]
        public void ShouldReturnViewComponentShouldThrowExceptionWithIncorrectViewComponentWithName()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .ViewComponent("TestComponent");
                },
                "When calling BadRequestAction action in MvcController expected action result to be ViewComponentResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnViewComponentShouldThrowExceptionWithIncorrectViewComponentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent("Incorrect");
                },
                "When calling ViewComponentResultByName action in MvcController expected view component result to be 'Incorrect', but instead received 'TestComponent'.");
        }

        [Fact]
        public void ShouldReturnViewComponentShouldNotThrowExceptionWithCorrectViewComponentType()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(typeof(CustomViewComponent));
        }

        [Fact]
        public void ShouldReturnViewComponentShouldThrowExceptionWithIncorrectViewComponentWithType()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .ViewComponent(typeof(CustomViewComponent));
                },
                "When calling BadRequestAction action in MvcController expected action result to be ViewComponentResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnViewComponentShouldThrowExceptionWithIncorrectViewComponentType()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent(typeof(ViewComponent));
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result to be 'ViewComponent', but instead received 'CustomViewComponent'.");
        }
    }
}
