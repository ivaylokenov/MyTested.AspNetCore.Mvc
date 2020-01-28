namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CustomTests
{
    using Exceptions;
    using Setups;
    using Setups.ActionResults;
    using Xunit;

    public class CustomActionResultTests
    {
        [Fact]
        public void CustomActionResultShouldNotThrowExceptionWithCorrectValues()
        {
            MyController<CustomActionResultController>
                .Instance()
                .Calling(c => c.CustomActionResult())
                .ShouldReturn()
                .Custom("Value", "CustomValue");
        }

        [Fact]
        public void CustomActionResultShouldNotThrowExceptionWithIncorrectValues()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<CustomActionResultController>
                        .Instance()
                        .Calling(c => c.CustomActionResult())
                        .ShouldReturn()
                        .Custom("InvalidValue", "CustomValue");
                },
                "When calling CustomActionResult action in CustomActionResultController expected response model CustomActionResult to pass the given predicate, but it failed.");
        }
    }
}
