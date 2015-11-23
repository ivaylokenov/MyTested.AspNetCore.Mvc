namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class AggregateExceptionTestBuilderTests
    {
        [Test]
        public void ContainingInnerExceptionOfTypeShouldNotThrowIfInnerExceptionIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling ActionWithAggregateException action in WebApiController expected AggregateException to contain ArgumentException, but none was found.")]
        public void ContainingInnerExceptionOfTypeShouldThrowIfInnerExceptionIsNotCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ArgumentException>();
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>()
                .AndAlso()
                .ContainingInnerExceptionOfType<InvalidOperationException>();
        }
    }
}
