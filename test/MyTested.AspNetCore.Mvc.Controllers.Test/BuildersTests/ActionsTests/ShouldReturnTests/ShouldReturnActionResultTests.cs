namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnActionResultTests
    {
        [Fact]
        public void ShouldReturnActionResultShouldNotThrowExceptionWhenResultIsIActionResultInterface()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterface())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultShouldNotThrowExceptionWhenResultIsIActionResultBaseClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultBaseClass())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultShouldNotThrowExceptionWhenResultIsActionResultOfT()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfAnonymousType())
                .ShouldReturn()
                .ActionResult();
        }

        [Fact]
        public void ShouldReturnActionResultShouldThrowExceptionWhenWhenResultIsNotActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AnonymousResult())
                        .ShouldReturn()
                        .ActionResult();
                },
                "When calling AnonymousResult action in MvcController expected result to be IActionResult or ActionResult<TValue>, but instead received AnonymousType<Int32, String, AnonymousType<Boolean>>.");
        }
    }
}
