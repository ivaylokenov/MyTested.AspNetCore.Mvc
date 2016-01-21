namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.OkTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class OkTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionIfNoResponseModelIsProvided()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
                .WithNoResponseModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionIfResponseModelIsProvided()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok()
                        .WithNoResponseModel();
                },
                "When calling OkResultWithResponse action in MvcController expected to not have response model but in fact response model was found.");
        }
    }
}
