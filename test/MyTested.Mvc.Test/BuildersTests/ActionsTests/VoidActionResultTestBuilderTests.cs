namespace MyTested.Mvc.Test.BuildersTests.ActionsTests
{
    using System;
    using Exceptions;
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
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.EmptyActionWithException())
                    .ShouldReturnEmpty();
            });

            Assert.Equal("NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.", exception.Message);
        }

        [Fact]
        public void ShouldReturnEmptyWithAsyncShouldThrowExceptionIfActionThrowsException()
        {
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .CallingAsync(c => c.EmptyActionWithExceptionAsync())
                    .ShouldReturnEmpty();
            });

            Assert.Equal("AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.", exception.Message);
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
            var exception = Assert.Throws<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.EmptyAction())
                    .ShouldHave()
                    .InvalidModelState();
            });

            Assert.Equal("When calling EmptyAction action in MvcController expected to have invalid model state, but was in fact valid.", exception.Message);
        }
    }
}
