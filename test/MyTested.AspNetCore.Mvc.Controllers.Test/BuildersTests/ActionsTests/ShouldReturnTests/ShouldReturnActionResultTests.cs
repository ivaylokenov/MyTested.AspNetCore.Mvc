namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
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

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldNotThrowExceptionWhenResultIsIActionResultInterface()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultInterface())
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldNotThrowExceptionWhenResultIsIActionResultBaseClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultBaseClass())
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldNotThrowExceptionWhenResultIsActionResultOfT()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionResultOfT(int.MaxValue))
                .ShouldReturn()
                .ActionResult(result => result
                    .Ok(okResult => okResult
                        .Passing(ok => ok.Value?.GetType() == typeof(ResponseModel))));
        }

        [Fact]
        public void ShouldReturnActionResultWithDetailsShouldThrowExceptionWhenWhenResultIsNotActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .ActionResult(result => result
                            .Ok());
                },
                "When calling BadRequestAction action in MvcController expected result to be OkResult, but instead received BadRequestResult.");
        }
    }
}
