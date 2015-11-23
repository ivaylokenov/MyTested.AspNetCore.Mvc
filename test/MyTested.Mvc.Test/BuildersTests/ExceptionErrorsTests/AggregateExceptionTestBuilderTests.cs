namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    using Setups;

    public class AggregateExceptionTestBuilderTests
    {
        [Fact]
        public void ContainingInnerExceptionOfTypeShouldNotThrowIfInnerExceptionIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>();
        }

        [Fact]
        public void ContainingInnerExceptionOfTypeShouldThrowIfInnerExceptionIsNotCorrect()
        {
            Test.AssertException<InvalidExceptionAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ActionWithAggregateException())
                    .ShouldThrow()
                    .AggregateException()
                    .ContainingInnerExceptionOfType<ArgumentException>();
            }, "When calling ActionWithAggregateException action in MvcController expected AggregateException to contain ArgumentException, but none was found.");

        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>()
                .AndAlso()
                .ContainingInnerExceptionOfType<InvalidOperationException>();
        }
    }
}
