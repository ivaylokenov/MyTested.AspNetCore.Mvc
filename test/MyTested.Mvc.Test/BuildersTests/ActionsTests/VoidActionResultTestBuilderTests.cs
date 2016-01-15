namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyAction())
                .ShouldReturnEmpty();
        }

        [Fact]
        public void ShouldReturnEmptyShouldThrowExceptionIfActionThrowsException()
        {
            Test.AssertException<ActionCallAssertionException>(
                () => 
                {
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .CallingAsync(c => c.EmptyActionWithExceptionAsync())
                        .ShouldReturnEmpty();
                },
                "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.");
        }

        [Fact]
        public void ShouldThrowExceptionShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Fact]
        public void ShouldHaveModelStateShouldWorkCorrectly()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.EmptyAction())
                        .ShouldHave()
                        .InvalidModelState();
                },
                "When calling EmptyAction action in MvcController expected to have invalid model state, but was in fact valid.");
        }
    }
}
