namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using System;
    using Xunit;

    public class ShouldThrowExceptionTests
    {
        [Fact]
        public void ShouldThrowExceptionShouldCatchAndValidateThereIsException()
        {
            MyViewComponent<ExceptionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldThrow()
                .Exception();
        }

        [Fact]
        public void ShouldThrowExceptionShouldThrowIfNoExceptionIsCaught()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldThrow()
                        .Exception();
                },
                "When invoking NormalComponent expected exception to be thrown, but in fact none was caught.");
        }

        [Fact]
        public void ShouldThrowExceptionShouldCatchAndValidateTypeOfException()
        {
            MyViewComponent<ExceptionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldThrow()
                .Exception()
                .OfType<IndexOutOfRangeException>();
        }

        [Fact]
        public void ShouldThrowExceptionShouldThrowWithInvalidTypeOfException()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyViewComponent<ExceptionComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldThrow()
                        .Exception()
                        .OfType<InvalidOperationException>();
                },
                "When invoking ExceptionComponent expected InvalidOperationException, but instead received IndexOutOfRangeException.");
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateException()
        {
            MyViewComponent<AggregateExceptionComponent>
                .Instance()
                .InvokedWith(c => c.InvokeAsync())
                .ShouldThrow()
                .AggregateException();
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldThrowIfTheExceptionIsNotValidType()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyViewComponent<ExceptionComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldThrow()
                        .AggregateException();
                },
                "When invoking ExceptionComponent expected AggregateException, but instead received IndexOutOfRangeException.");
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateExceptionWithSpecificNumberOfInnerExceptions()
        {
            MyViewComponent<AggregateExceptionComponent>
                .Instance()
                .InvokedWith(c => c.InvokeAsync())
                .ShouldThrow()
                .AggregateException(1);
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateExceptionWithWrongNumberOfInnerExceptions()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyViewComponent<AggregateExceptionComponent>
                        .Instance()
                        .InvokedWith(c => c.InvokeAsync())
                        .ShouldThrow()
                        .AggregateException(2);
                },
                "When invoking AggregateExceptionComponent expected AggregateException to contain 2 inner exceptions, but in fact contained 1.");
        }
    }
}
