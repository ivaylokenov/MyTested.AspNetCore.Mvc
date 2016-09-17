namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldThrowExceptionTests
    {
        [Fact]
        public void ShouldThrowExceptionShouldCatchAndValidateThereIsException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception();
        }

        [Fact]
        public void ShouldThrowExceptionShouldThrowIfNoExceptionIsCaught()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldThrow()
                        .Exception();
                }, 
                "When calling OkResultAction action in MvcController expected exception to be thrown, but in fact none was caught.");
        }

        [Fact]
        public void ShouldThrowExceptionShouldCatchAndValidateTypeOfException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Fact]
        public void ShouldThrowExceptionShouldThrowWithInvalidTypeOfException()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .Exception()
                        .OfType<InvalidOperationException>();
                },
                "When calling ActionWithException action in MvcController expected InvalidOperationException, but instead received NullReferenceException.");
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException();
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldThrowIfTheExceptionIsNotValidType()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldThrow()
                        .AggregateException();
                }, 
                "When calling ActionWithException action in MvcController expected AggregateException, but instead received NullReferenceException.");
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateExceptionWithSpecificNumberOfInnerExceptions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException(2);
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateExceptionWithWrongNumberOfInnerExceptions()
        {
            Test.AssertException<InvalidExceptionAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithAggregateException())
                        .ShouldThrow()
                        .AggregateException(3);
                }, 
                "When calling ActionWithAggregateException action in MvcController expected AggregateException to contain 3 inner exceptions, but in fact contained 2.");
        }
    }
}
