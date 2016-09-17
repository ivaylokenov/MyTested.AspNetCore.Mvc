namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class VoidActionResultTestBuilderTests
    {
        [Fact]
        public void ShouldReturnEmptyShouldNotThrowExceptionWithNormalVoidAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyAction())
                .ShouldReturnEmpty();
        }

        [Fact]
        public void ShouldReturnEmptyShouldThrowExceptionIfActionThrowsException()
        {
            Test.AssertException<InvocationAssertionException>(
                () => 
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyActionWithException())
                        .ShouldReturnEmpty();
                },
                "When calling EmptyActionWithException action in MvcController expected no exception but NullReferenceException with 'Test exception message' message was thrown without being caught.");
        }

        [Fact]
        public void ShouldReturnEmptyWithAsyncShouldThrowExceptionIfActionThrowsException()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyActionWithExceptionAsync())
                        .ShouldReturnEmpty();
                },
                "When calling EmptyActionWithExceptionAsync action in MvcController expected no exception but AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown without being caught.");
        }

        [Fact]
        public void ShouldThrowExceptionShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }
    }
}
