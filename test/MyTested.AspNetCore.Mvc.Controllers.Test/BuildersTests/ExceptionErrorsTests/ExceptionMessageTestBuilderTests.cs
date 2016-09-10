namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.CaughtExceptionsTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ExceptionMessageTestBuilderTests
    {
        [Fact]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().ThatEquals("Test exception message");
        }

        [Fact]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessageAndFirstCallingWithMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .WithMessage().ThatEquals("Test exception message")
                .AndAlso()
                .OfType<NullReferenceException>();
        }

        [Fact]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<NullReferenceException>()
                        .AndAlso()
                        .WithMessage().ThatEquals("Test");
                },
                "When calling ActionWithException action in MvcController expected exception message to be 'Test', but instead found 'Test exception message'.");
        }

        [Fact]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().BeginningWith("Test ");
        }

        [Fact]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<NullReferenceException>()
                        .AndAlso()
                        .WithMessage().BeginningWith("exception");
                },
                "When calling ActionWithException action in MvcController expected exception message to begin with 'exception', but instead found 'Test exception message'.");
        }

        [Fact]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().EndingWith("message");
        }

        [Fact]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<NullReferenceException>()
                        .AndAlso()
                        .WithMessage().EndingWith("Test");
                },
                "When calling ActionWithException action in MvcController expected exception message to end with 'Test', but instead found 'Test exception message'.");
        }

        [Fact]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().Containing("n m");
        }

        [Fact]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<NullReferenceException>()
                        .AndAlso()
                        .WithMessage().Containing("Another");
                }, 
                "When calling ActionWithException action in MvcController expected exception message to contain 'Another', but instead found 'Test exception message'.");
        }
    }
}
