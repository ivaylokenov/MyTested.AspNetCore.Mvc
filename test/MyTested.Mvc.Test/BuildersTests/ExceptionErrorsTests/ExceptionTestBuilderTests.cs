namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ExceptionTestBuilderTests
    {
        [Fact]
        public void OfTypeShouldNotThrowExceptionWhenExceptionIsOfTheProvidedType()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Fact]
        public void OfTypeShouldThrowExceptionWhenExceptionIsNotOfTheProvidedType()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<InvalidOperationException>();
                },
                "When calling ActionWithException action in MvcController expected InvalidOperationException, but instead received NullReferenceException.");
        }

        [Fact]
        public void WithMessageShouldNotThrowExceptionWhenExceptionIsWithCorrectMessage()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .WithMessage("Test exception message")
                .AndAlso()
                .OfType<NullReferenceException>();
        }

        [Fact]
        public void WithMessageShouldNotThrowExceptionWhenExceptionIsWithIncorrectMessage()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                   () =>
                   {
                       MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<NullReferenceException>()
                        .AndAlso()
                        .WithMessage("Exception message");
                   },
                   "When calling ActionWithException action in MvcController expected exception with message 'Exception message', but instead received 'Test exception message'.");
        }
    }
}
