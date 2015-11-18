namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnBadRequestTests
    {
        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequest()
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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.NotFoundAction())
                    .ShouldReturn()
                    .HttpBadRequest();
            });

            Assert.Equal("When calling NotFoundAction action in MvcController expected action result to be BadRequestResult, but instead received NotFoundResult.", exception.Message);
        }
    }
}
