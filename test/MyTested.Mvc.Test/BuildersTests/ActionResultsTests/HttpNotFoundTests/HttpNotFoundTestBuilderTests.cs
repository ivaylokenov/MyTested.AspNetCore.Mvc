namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.HttpNotFoundTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class HttpNotFoundTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionWithNoResponseModel()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.HttpNotFoundAction())
                .ShouldReturn()
                .HttpNotFound()
                .WithNoResponseModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionWithAnyResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.HttpNotFoundWithObjectAction())
                        .ShouldReturn()
                        .HttpNotFound()
                        .WithNoResponseModel();
                },
                "When calling HttpNotFoundWithObjectAction action in MvcController expected to not have response model, but in fact response model was found.");
        }
    }
}
