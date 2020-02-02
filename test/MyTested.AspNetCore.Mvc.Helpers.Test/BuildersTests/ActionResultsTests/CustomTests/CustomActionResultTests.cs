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
        public void CustomActionResultShouldThrowExceptionWithIncorrectActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<CustomActionResultController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldReturn()
                        .Custom("Value", "CustomValue");
                },
                "When calling Ok action in CustomActionResultController expected result to be CustomActionResult, but instead received OkResult.");
        }

        [Fact]
        public void CustomActionResultShouldThrowExceptionWithIncorrectValues()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<CustomActionResultController>
                        .Instance()
                        .Calling(c => c.CustomActionResult())
                        .ShouldReturn()
                        .Custom("InvalidValue", "CustomValue");
                },
                "When calling CustomActionResult action in CustomActionResultController expected the CustomActionResult to pass the given predicate, but it failed.");
        }
    }
}
