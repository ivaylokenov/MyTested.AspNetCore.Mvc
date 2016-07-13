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
            Test.AssertException<ActionCallAssertionException>(
                () => 
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyActionWithException())
                        .ShouldReturnEmpty();
                }, 
                "NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.");
        }

        [Fact]
        public void ShouldReturnEmptyWithAsyncShouldThrowExceptionIfActionThrowsException()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyActionWithExceptionAsync())
                        .ShouldReturnEmpty();
                },
                "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.");
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
