namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc.Test.Setups.Models;
    using Setups;
    using Setups.Controllers;
    using System.Collections.Generic;
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
                .View(view => view
                    .Passing((v) =>
                    {
                        Assert.False(string.IsNullOrEmpty(v.ViewName));
                        Assert.Equal("Index", v.ViewName);
                        Assert.True(typeof(ViewResult) == v.GetType());
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
                .View(view => view
                    .WithName("Index")
                    .AndAlso()
                    .Passing(v => v.GetType() == typeof(ViewResult)));
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
                .PartialView(view => view
                    .Passing(v =>
                    {
                        Assert.False(string.IsNullOrEmpty(v.ViewName));
                        Assert.Equal("_IndexPartial", v.ViewName);
                        Assert.True(typeof(PartialViewResult) == v.GetType());
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
                .View(view => view
                    .Passing(v => 
                    {
                        Assert.True(string.IsNullOrEmpty(v.ViewName));
                        Assert.True(v.GetType() == typeof(ViewResult));
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
                .View(view => view
                    .WithDefaultName()
                    .Passing(v => v.GetType() == typeof(ViewResult)));
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
                .PartialView(view => view
                    .Passing(v =>
                    {
                        Assert.True(string.IsNullOrEmpty(v.ViewName));
                        Assert.True(v.GetType() == typeof(PartialViewResult));
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
