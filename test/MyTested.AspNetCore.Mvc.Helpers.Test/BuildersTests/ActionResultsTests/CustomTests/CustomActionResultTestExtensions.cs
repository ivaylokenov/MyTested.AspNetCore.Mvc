namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CustomTests
{
    using Builders.Actions.ShouldReturn;
    using Builders.And;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Setups.ActionResults;

    public static class CustomActionResultTestExtensions
    {
        public static IAndTestBuilder Custom<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string value, 
            string customProperty)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)shouldReturnTestBuilder;

            actualBuilder
                .ResultOfType<CustomActionResult>(result => result
                    .Passing(c => c.Value as string == value && c.CustomProperty == customProperty));

            return new AndTestBuilder(actualBuilder.TestContext);
        }
    }
}
