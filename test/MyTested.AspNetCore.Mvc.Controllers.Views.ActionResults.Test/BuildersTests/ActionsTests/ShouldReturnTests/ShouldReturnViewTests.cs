namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ShouldReturnViewTests
    {
        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View(view => view
                    .WithName("Index"));
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionAndPassAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View(result => result
                    .Passing(view =>
                    {
                        Assert.False(string.IsNullOrEmpty(view.ViewName));
                        Assert.Equal("Index", view.ViewName);
                        Assert.True(typeof(ViewResult) == view.GetType());
                    }));
        }

        [Fact]
        public void ShouldReturnViewShouldNotThrowExceptionWithCorrectViewName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewResultByName())
                .ShouldReturn()
                .View(view => view
                    .WithName("TestView"));
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectViewResultType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View(result => result
                    .WithName("Index")
                    .AndAlso()
                    .Passing(view => view.GetType() == typeof(ViewResult)));
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexView())
                       .ShouldReturn()
                       .View(view => view
                           .WithName("Incorrect"));
                },
                "When calling IndexView action in MvcController expected view result to be 'Incorrect', but instead received 'Index'.");
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionWithEmptyString()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexView())
                       .ShouldReturn()
                       .View(view => view
                           .WithName(string.Empty));
                },
                "When calling IndexView action in MvcController expected view result to be '', but instead received 'Index'.");
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionWithNullViewName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexView())
                       .ShouldReturn()
                       .View(view => view
                           .WithName(null));
                },
                "When calling IndexView action in MvcController expected view result to be the default one, but instead received 'Index'.");
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionWithIncorrectViewResultAndName()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .View(view => view
                           .WithName("TestView"));
                },
                "When calling BadRequestAction action in MvcController expected result to be ViewResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnPartialViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView(view => view
                    .WithName("_IndexPartial"));
        }

        [Fact]
        public void ShouldReturnPartialViewWithNameShouldNotThrowExceptionAndPassAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView(result => result
                    .Passing(partialView =>
                    {
                        Assert.False(string.IsNullOrEmpty(partialView.ViewName));
                        Assert.Equal("_IndexPartial", partialView.ViewName);
                        Assert.True(typeof(PartialViewResult) == partialView.GetType());
                    }));
        }

        [Fact]
        public void ShouldReturnPartialViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexPartialView())
                       .ShouldReturn()
                       .PartialView(view => view
                           .WithName("Incorrect"));
                },
                "When calling IndexPartialView action in MvcController expected partial view result to be 'Incorrect', but instead received '_IndexPartial'.");
        }

        [Fact]
        public void ShouldReturnPartialViewShouldThrowExceptionWithNullViewName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexPartialView())
                       .ShouldReturn()
                       .PartialView(view => view
                           .WithName(null));
                },
                "When calling IndexPartialView action in MvcController expected partial view result to be the default one, but instead received '_IndexPartial'.");
        }

        [Fact]
        public void ShouldReturnParrtialViewShouldThrowExceptionWithIncorrectViewResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .PartialView(view => view
                           .WithName("_IndexPartial"));
                },
                "When calling BadRequestAction action in MvcController expected result to be PartialViewResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnViewWithDefaultNameShouldNotThrowException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View(view => view
                    .WithDefaultName());
        }

        [Fact]
        public void ShouldReturnViewWithDefaultNameShouldNotThrowExceptionAndPassAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View(result => result
                    .Passing(view =>
                    {
                        Assert.True(string.IsNullOrEmpty(view.ViewName));
                        Assert.True(view.GetType() == typeof(ViewResult));
                    }));
        }

        [Fact]
        public void ShouldReturnViewWithDefaultNameAndViewModelShouldNotThrowException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultViewWithModel())
                .ShouldReturn()
                .View(view => view
                    .WithDefaultName()
                    .AndAlso()
                    .WithModelOfType<ICollection<ResponseModel>>());
        }

        [Fact]
        public void ShouldReturnViewWithDefaultNameAndCustomViewResultTypeShouldNotThrowExceptionWithCorrectType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewResult())
                .ShouldReturn()
                .View(result => result
                    .WithDefaultName()
                    .Passing(view => view.GetType() == typeof(ViewResult)));
        }

        [Fact]
        public void ShouldReturnPartialViewWithDefaultNameShouldNotThrowException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView(view => view
                    .WithDefaultName());
        }

        [Fact]
        public void ShouldReturnPartialViewWithDefaultNameShouldNotThrowExceptionAndPassAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView(result => result
                    .Passing(partialView =>
                    {
                        Assert.True(string.IsNullOrEmpty(partialView.ViewName));
                        Assert.True(partialView.GetType() == typeof(PartialViewResult));
                    }));
        }

        [Fact]
        public void ShouldReturnPartialViewWithDefaultNameAndViewModelShouldNotThrowException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialViewWithModel())
                .ShouldReturn()
                .PartialView(view => view
                    .WithDefaultName()
                    .AndAlso()
                    .WithModelOfType<ICollection<ResponseModel>>());
        }
    }
}
