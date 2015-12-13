namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class ExceptionTestBuilderTests
    {
        // TODO: no internal server error
        //[Fact]
        //public void OfTypeShouldNotThrowExceptionWhenExceptionIsOfTheProvidedType()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in MvcController expected InvalidOperationException, but instead received NullReferenceException.")]
        //public void OfTypeShouldThrowExceptionWhenExceptionIsNotOfTheProvidedType()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<InvalidOperationException>();
        //}

        //[Fact]
        //public void WithMessageShouldNotThrowExceptionWhenExceptionIsWithCorrectMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .WithMessage("Test exception message")
        //        .AndAlso()
        //        .OfType<NullReferenceException>();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in MvcController expected exception with message 'Exception message', but instead received 'Test exception message'.")]
        //public void WithMessageShouldNotThrowExceptionWhenExceptionIsWithIncorrectMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.InternalServerErrorWithExceptionAction())
        //        .ShouldReturn()
        //        .InternalServerError()
        //        .WithException()
        //        .OfType<NullReferenceException>()
        //        .AndAlso()
        //        .WithMessage("Exception message");
        //}
    }
}
