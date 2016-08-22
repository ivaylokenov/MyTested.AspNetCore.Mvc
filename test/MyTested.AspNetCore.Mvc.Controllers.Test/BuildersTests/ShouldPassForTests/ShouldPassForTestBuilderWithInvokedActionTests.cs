namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderWithInvokedActionTests
    {
        [Fact]
        public void CaughtExceptionAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<Exception>(exception =>
                {
                    Assert.Null(exception);
                });
        }

        [Fact]
        public void CaughtExceptionPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassForThe<Exception>(exception => exception == null);
        }

        [Fact]
        public void CaughtExceptionPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassForThe<Exception>(exception => exception != null);
                },
                "Expected the caught exception to pass the given predicate but it failed.");
        }
        
        [Fact]
        public void CaughtExceptionPredicateShouldThrowExceptionWithInvalidTestAndActualException()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldPassForThe<Exception>(exception => exception == null);
                },
                "Expected the NullReferenceException to pass the given predicate but it failed.");
        }

        [Fact]
        public void HttpResponseAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<HttpResponse>(response =>
                {
                    Assert.NotNull(response);
                });
        }

        [Fact]
        public void HttpResponsePredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassForThe<HttpResponse>(response => response != null);
        }

        [Fact]
        public void HttpResponsePredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassForThe<HttpResponse>(response => response == null);
                },
                "Expected the HttpResponse to pass the given predicate but it failed.");
        }
    }
}
