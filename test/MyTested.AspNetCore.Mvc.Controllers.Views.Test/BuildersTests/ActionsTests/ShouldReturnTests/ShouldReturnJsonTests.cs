namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnJsonTests
    {
        [Fact]
        public void ShouldReturnJsonShouldNotThrowExceptionIfResultIsJson()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json();
        }

        [Fact]
        public void ShouldReturnJsonShouldThrowExceptionIfResultIsNotJson()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Json();
                },
                "When calling BadRequestAction action in MvcController expected result to be JsonResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnJsonShouldNotThrowExceptionIfResultIsEmptyJson()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyJsonAction())
                .ShouldReturn()
                .Json();
        }

        [Fact]
        public void ShouldReturnJsonShouldNotThrowExceptionIfResultIsNullJson()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NullJsonAction())
                .ShouldReturn()
                .Json();
        }

        [Fact]
        public void ShouldReturnJsonShouldThrowExceptionIfResultIsNullAction()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                         .Instance()
                         .Calling(c => c.NullAction())
                         .ShouldReturn()
                         .Json();
                },
                "When calling NullAction action in MvcController expected result to be JsonResult, but instead received null.");
        }

        [Fact]
        public void ShouldReturnJsonShouldThrowExceptionIfResultIsContentAction()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                            .Instance()
                            .Calling(c => c.ContentAction())
                            .ShouldReturn()
                            .Json();
                },
                "When calling ContentAction action in MvcController expected result to be JsonResult, but instead received ContentResult.");
        }
    }
}
