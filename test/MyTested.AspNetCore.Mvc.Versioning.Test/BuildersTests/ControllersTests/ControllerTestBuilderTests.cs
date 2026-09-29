namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Asp.Versioning;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerTestBuilderTests
    {
        [Fact]
        public void ControllerAssertionShouldWorkCorrectlyWithVersioning()
        {
            MyController<VersioningController>
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ControllerAssertionShouldReturnRequestedApiVersionFromDefaultQueryStringParameter()
        {
            MyController<RequestedVersionController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery("api-version", "2.0"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => Assert.Equal(new ApiVersion(2, 0), result.Value)));
        }

        [Fact]
        public void ControllerAssertionShouldReturnRequestedApiVersionFromHeader()
        {
            MyController<RequestedVersionController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithHeader("X-Custom-Version", "1.0"))
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => Assert.Equal(new ApiVersion(1, 0), result.Value)));
        }

        [Fact]
        public void ControllerAssertionShouldReturnNullRequestedApiVersionWithoutVersion()
        {
            MyController<RequestedVersionController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => Assert.Null(result.Value)));
        }

        [Fact]
        public void ControllerAssertionShouldThrowExceptionWithIncorrectRequestedApiVersion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<RequestedVersionController>
                        .Instance()
                        .WithHttpRequest(request => request
                            .WithQuery("api-version", "2.0"))
                        .Calling(c => c.Index())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .Passing(result => new ApiVersion(1, 0).Equals(result.Value)));
                },
                "When calling Index action in RequestedVersionController expected the OkObjectResult to pass the given predicate, but it failed.");
        }
    }
}
