namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class ExceptionMessageTestBuilderTests
    {
        [Test]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().ThatEquals("Test exception message");
        }

        [Test]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessageAndFirstCallingWithMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .WithMessage().ThatEquals("Test exception message")
                .AndAlso()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to be 'Test', but instead found 'Test exception message'.")]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().ThatEquals("Test");
        }

        [Test]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().BeginningWith("Test ");
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to begin with 'exception', but instead found 'Test exception message'.")]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().BeginningWith("exception");
        }

        [Test]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().EndingWith("message");
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to end with 'Test', but instead found 'Test exception message'.")]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().EndingWith("Test");
        }

        [Test]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().Containing("n m");
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to contain 'Another', but instead found 'Test exception message'.")]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().Containing("Another");
        }
    }
}
