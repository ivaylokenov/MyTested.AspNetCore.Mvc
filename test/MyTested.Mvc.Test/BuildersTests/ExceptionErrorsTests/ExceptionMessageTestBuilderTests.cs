namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    // TODO: no internal server error
    public class ExceptionMessageTestBuilderTests
    {
        //[Fact]
        //public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().ThatEquals("Test exception message");
        //}

        //[Fact]
        //public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessageAndFirstCallingWithMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .WithMessage().ThatEquals("Test exception message")
        //        .AndAlso()
        //        .OfType<NullReferenceException>();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in MvcController expected exception message to be 'Test', but instead found 'Test exception message'.")]
        //public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().ThatEquals("Test");
        //}

        //[Fact]
        //public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().BeginningWith("Test ");
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in MvcController expected exception message to begin with 'exception', but instead found 'Test exception message'.")]
        //public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().BeginningWith("exception");
        //}

        //[Fact]
        //public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().EndingWith("message");
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in MvcController expected exception message to end with 'Test', but instead found 'Test exception message'.")]
        //public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().EndingWith("Test");
        //}

        //[Fact]
        //public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().Containing("n m");
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in MvcController expected exception message to contain 'Another', but instead found 'Test exception message'.")]
        //public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage().Containing("Another");
        //}
    }
}
