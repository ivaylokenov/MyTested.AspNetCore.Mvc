namespace MyTested.AspNetCore.Mvc.Internal.Extensions
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Contracts.ActionResults;
    using Utilities.Extensions;
    using Utilities.Validators;

    public static class BaseTestBuilderWithViewResultExtensions
    {
        internal static TViewResultTestBuilder WithDefaultName<TViewResultTestBuilder>(
            this IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult)
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithViewResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualViewName = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .ViewName;

                if (actualViewName != null)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "name",
                        $"to be {TestHelper.GetFriendlyName(null)}",
                        $"instead received {TestHelper.GetFriendlyName(actualViewName)}");
                }
            });

            return actualBuilder.ResultTestBuilder;
        }
        
        private static IBaseTestBuilderWithViewFeatureResultInternal<TViewResultTestBuilder>
            GetActualBuilder<TViewResultTestBuilder>(
                IBaseTestBuilderWithViewResult<TViewResultTestBuilder> baseTestBuilderWithViewResult)
            where TViewResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithViewFeatureResultInternal<TViewResultTestBuilder>)baseTestBuilderWithViewResult;
    }
}
