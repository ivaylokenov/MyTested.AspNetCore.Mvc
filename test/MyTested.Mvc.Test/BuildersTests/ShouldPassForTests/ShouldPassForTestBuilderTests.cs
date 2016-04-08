namespace MyTested.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderTests
    {
        [Fact]
        public void HttpContextAssertionsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassFor()
                .TheHttpContext(context =>
                {
                    Assert.Equal("Test", context.Request.Scheme);
                });
        }

        [Fact]
        public void HttpContextPredicateShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassFor()
                .TheHttpContext(context => context.Request.Scheme == "Test");
        }

        [Fact]
        public void HttpContextPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithHttpContext(context => context.Request.Scheme = "Test")
                        .ShouldPassFor()
                        .TheHttpContext(context => context.Request.Scheme == "Invalid");
                },
                "Expected the HttpContext to pass the given predicate but it failed.");
        }

        [Fact]
        public void HttpRequestAssertionsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassFor()
                .TheHttpRequest(request =>
                {
                    Assert.Equal("Test", request.Scheme);
                });
        }

        [Fact]
        public void HttpRequestPredicateShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(context => context.Request.Scheme = "Test")
                .ShouldPassFor()
                .TheHttpRequest(request => request.Scheme == "Test");
        }

        [Fact]
        public void HttpRequestPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithHttpContext(context => context.Request.Scheme = "Test")
                        .ShouldPassFor()
                        .TheHttpRequest(request => request.Scheme == "Invalid");
                },
                "Expected the HttpRequest to pass the given predicate but it failed.");
        }
    }
}
