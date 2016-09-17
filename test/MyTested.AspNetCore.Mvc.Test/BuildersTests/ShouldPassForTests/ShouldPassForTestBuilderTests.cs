namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderTests
    {
        [Fact]
        public void HttpContextAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassForThe<HttpContext>(context =>
                {
                    Assert.Equal("Test", context.Request.Scheme);
                });
        }

        [Fact]
        public void HttpContextPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassForThe<HttpContext>(context => context.Request.Scheme == "Test");
        }

        [Fact]
        public void HttpContextPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithHttpContext(context => context.Request.Scheme = "Test")
                        .ShouldPassForThe<HttpContext>(context => context.Request.Scheme == "Invalid");
                },
                "Expected HttpContext to pass the given predicate but it failed.");
        }

        [Fact]
        public void HttpRequestAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassForThe<HttpRequest>(request =>
                {
                    Assert.Equal("Test", request.Scheme);
                });
        }

        [Fact]
        public void HttpRequestPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassForThe<HttpRequest>(request => request.Scheme == "Test");
        }

        [Fact]
        public void HttpRequestPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithHttpContext(context => context.Request.Scheme = "Test")
                        .ShouldPassForThe<HttpRequest>(request => request.Scheme == "Invalid");
                },
                "Expected HttpRequest to pass the given predicate but it failed.");
        }
    }
}
