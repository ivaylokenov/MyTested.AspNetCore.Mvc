namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnHttpBadRequestTests
    {
        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsHttpBadRequest()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .HttpBadRequest();
        }

        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsInvalidModelStateResult()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .HttpBadRequest();
        }

        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequestErrorMessageResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .HttpBadRequest();
        }

        [Fact]
        public void ShouldReturnNotFoundShouldThrowExceptionWhenActionDoesNotReturnNotFound()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.HttpNotFoundAction())
                        .ShouldReturn()
                        .HttpBadRequest();
                },
                "When calling HttpNotFoundAction action in MvcController expected action result to be BadRequestResult, but instead received HttpNotFoundResult.");
        }
    }
}
