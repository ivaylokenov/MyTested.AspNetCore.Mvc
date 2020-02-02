namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.NotFoundTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class NotFoundTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionWithNoResponseModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .WithNoModel());
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionWithAnyResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NotFoundWithObjectAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .WithNoModel());
                },
                "When calling NotFoundWithObjectAction action in MvcController expected to not have a response model but in fact such was found.");
        }
    }
}
