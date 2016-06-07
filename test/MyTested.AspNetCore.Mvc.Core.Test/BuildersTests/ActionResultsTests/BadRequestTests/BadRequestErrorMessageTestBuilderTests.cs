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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .ThatEquals("Bad request");
        }

        [Fact]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage()
                        .ThatEquals("Bad");
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to be 'Bad', but instead found 'Bad request'.");
        }

        [Fact]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .BeginningWith("Bad ");
        }

        [Fact]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage()
                        .BeginningWith("request");
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to begin with 'request', but instead found 'Bad request'.");
        }

        [Fact]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .EndingWith("request");
        }

        [Fact]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage()
                        .EndingWith("Bad");
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to end with 'Bad', but instead found 'Bad request'.");
        }

        [Fact]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .Containing("d r");
        }

        [Fact]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage()
                        .Containing("Another");
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request error message to contain 'Another', but instead found 'Bad request'.");
        }
    }
}
