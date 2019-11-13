namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.CaughtExceptionsTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class AggregateExceptionTestBuilderTests
    {
        [Fact]
        public void ContainingInnerExceptionOfTypeGenericShouldNotThrowIfInnerExceptionIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>();
        }

        [Fact]
        public void ContainingInnerExceptionOfGenericTypeShouldThrowIfInnerExceptionIsNotCorrect()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithAggregateException())
                        .ShouldThrow()
                        .AggregateException()
                        .ContainingInnerExceptionOfType<ArgumentException>();
                }, 
                "When calling ActionWithAggregateException action in MvcController expected AggregateException to contain ArgumentException, but none was found.");
        }


        [Fact]
        public void ContainingInnerExceptionOfTypeShouldNotThrowIfInnerExceptionIsCorrect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType(typeof(NullReferenceException));
        }

        [Fact]
        public void ContainingInnerExceptionOfTypeShouldThrowIfInnerExceptionIsNotCorrect()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithAggregateException())
                        .ShouldThrow()
                        .AggregateException()
                        .ContainingInnerExceptionOfType(typeof(ArgumentException));
                },
                "When calling ActionWithAggregateException action in MvcController expected AggregateException to contain ArgumentException, but none was found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyForGeneric()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>()
                .AndAlso()
                .ContainingInnerExceptionOfType<InvalidOperationException>();
        }


        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType(typeof(NullReferenceException))
                .AndAlso()
                .ContainingInnerExceptionOfType(typeof(InvalidOperationException));
        }
    }
}
