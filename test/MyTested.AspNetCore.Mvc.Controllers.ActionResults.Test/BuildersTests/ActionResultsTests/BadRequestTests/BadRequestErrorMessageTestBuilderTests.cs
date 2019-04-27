namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class BadRequestErrorMessageTestBuilderTests
    {
        [Fact]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorMessage()
                    .ThatEquals("Bad request"));
        }

        [Fact]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage()
                            .ThatEquals("Bad"));
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to be 'Bad', but instead found 'Bad request'.");
        }

        [Fact]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorMessage()
                    .BeginningWith("Bad "));
        }

        [Fact]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage()
                            .BeginningWith("request"));
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to begin with 'request', but instead found 'Bad request'.");
        }

        [Fact]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorMessage()
                    .EndingWith("request"));
        }

        [Fact]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage()
                            .EndingWith("Bad"));
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to end with 'Bad', but instead found 'Bad request'.");
        }

        [Fact]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorMessage()
                    .Containing("d r"));
        }

        [Fact]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage()
                            .Containing("Another"));
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to contain 'Another', but instead found 'Bad request'.");
        }
    }
}
