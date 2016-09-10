namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderWithInvokedActionTests
    {
        [Fact]
        public void CaughtExceptionAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexOutOfRangeException())
                .ShouldPassForThe<Exception>(exception =>
                {
                    Assert.NotNull(exception);
                });
        }

        [Fact]
        public void CaughtExceptionPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexOutOfRangeException())
                .ShouldPassForThe<Exception>(exception => exception != null);
        }

        [Fact]
        public void CaughtExceptionPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.IndexOutOfRangeException())
                        .ShouldPassForThe<Exception>(exception => exception == null);
                },
                "Expected Exception to pass the given predicate but it failed.");
        }
        
        [Fact]
        public void CaughtExceptionPredicateShouldThrowExceptionWithInvalidTestAndActualException()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithException())
                        .ShouldPassForThe<NullReferenceException>(exception => exception == null);
                },
                "Expected NullReferenceException to pass the given predicate but it failed.");
        }
    }
}
