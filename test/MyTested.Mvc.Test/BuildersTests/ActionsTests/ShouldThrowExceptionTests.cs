namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
        
    public class ShouldThrowExceptionTests
    {
        [Fact]
        public void ShouldThrowExceptionShouldCatchAndValidateThereIsException()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception();
        }

        [Fact]
        public void ShouldThrowExceptionShouldThrowIfNoExceptionIsCaught()
        {
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldThrow()
                    .Exception();
            });

            Assert.Equal("When calling OkResultAction action in MvcController thrown exception was expected, but in fact none was caught.", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionShouldCatchAndValidateTypeOfException()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Fact]
        public void ShouldThrowExceptionShouldThrowWithInvalidTypeOfException()
        {
            var exception = Assert.Throws<InvalidExceptionAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ActionWithException())
                    .ShouldThrow()
                    .Exception()
                    .OfType<InvalidOperationException>();
            });

            Assert.Equal("When calling ActionWithException action in MvcController expected InvalidOperationException, but instead received NullReferenceException.", exception.Message);
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateException()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException();
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldThrowIfTheExceptionIsNotValidType()
        {
            var exception = Assert.Throws<InvalidExceptionAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ActionWithException())
                    .ShouldThrow()
                    .AggregateException();
            });

            Assert.Equal("When calling ActionWithException action in MvcController expected AggregateException, but instead received NullReferenceException.", exception.Message);
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateExceptionWithSpecificNumberOfInnerExceptions()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException(2);
        }

        [Fact]
        public void ShouldThrowAggregateExceptionShouldCatchAndValidateAggregateExceptionWithWrongNumberOfInnerExceptions()
        {
            var exception = Assert.Throws<InvalidExceptionAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ActionWithAggregateException())
                    .ShouldThrow()
                    .AggregateException(3);
            });

            Assert.Equal("When calling ActionWithAggregateException action in MvcController expected AggregateException to contain 3 inner exceptions, but in fact contained 2.", exception.Message);
        }

        // TODO: ?
        //[Fact]
        //public void ShouldThrowHttpResponseExceptionShouldCatchAndValidateHttpResponseException()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ActionWithHttpResponseException())
        //        .ShouldThrow()
        //        .HttpResponseException();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidExceptionAssertionException),
        //    ExpectedMessage = "When calling ActionWithException action in MvcController expected HttpResponseException, but instead received NullReferenceException.")]
        //public void ShouldThrowHttpResponseExceptionShouldThrowIfTheExceptionIsNotValidType()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ActionWithException())
        //        .ShouldThrow()
        //        .HttpResponseException();
        //}
    }
}
