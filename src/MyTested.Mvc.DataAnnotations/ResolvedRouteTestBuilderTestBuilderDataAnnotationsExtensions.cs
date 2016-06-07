namespace MyTested.Mvc
{
    using System.Linq;
    using Builders.Contracts.Routes;
    using Builders.Routes;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IResolvedRouteTestBuilder"/>.
    /// </summary>
    public static class ResolvedRouteTestBuilderDataAnnotationsExtensions
    {
        /// <summary>
        /// Tests whether the resolved route has valid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <param name="resolvedRouteTestBuilder">Instance of <see cref="IResolvedRouteTestBuilder"/> type.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        public static IAndResolvedRouteTestBuilder ToValidModelState(this IResolvedRouteTestBuilder resolvedRouteTestBuilder)
        {
            var actualShouldMapTestBuilder = (ShouldMapTestBuilder)resolvedRouteTestBuilder;

            var actualInfo = actualShouldMapTestBuilder.GetActualRouteInfo();
            if (!actualInfo.IsResolved)
            {
                actualShouldMapTestBuilder.ThrowNewRouteAssertionException(
                    ShouldMapTestBuilder.ExpectedModelStateErrorMessage,
                    actualInfo.UnresolvedError);
            }

            if (!actualInfo.ModelState.IsValid)
            {
                actualShouldMapTestBuilder.ThrowNewRouteAssertionException(
                    ShouldMapTestBuilder.ExpectedModelStateErrorMessage,
                    "it had some");
            }

            return actualShouldMapTestBuilder;
        }

        /// <summary>
        /// Tests whether the resolved route has invalid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <param name="resolvedRouteTestBuilder">Instance of <see cref="IResolvedRouteTestBuilder"/> type.</param>
        /// <param name="withNumberOfErrors">Expected number of errors. If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>The same <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        public static IAndResolvedRouteTestBuilder ToInvalidModelState(
            this IResolvedRouteTestBuilder resolvedRouteTestBuilder,
            int? withNumberOfErrors = null)
        {
            var actualShouldMapTestBuilder = (ShouldMapTestBuilder)resolvedRouteTestBuilder;

            var actualInfo = actualShouldMapTestBuilder.GetActualRouteInfo();
            if (!actualInfo.IsResolved)
            {
                actualShouldMapTestBuilder.ThrowNewRouteAssertionException(
                    "have invalid model state",
                    actualInfo.UnresolvedError);
            }

            var actualModelStateErrors = actualInfo.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                actualShouldMapTestBuilder.ThrowNewRouteAssertionException(
                    $"have invalid model state{(withNumberOfErrors == null ? string.Empty : $" with {withNumberOfErrors} {(withNumberOfErrors != 1 ? "errors" : "error")}")}",
                    withNumberOfErrors == null ? "was in fact valid" : $"in fact contained {actualModelStateErrors}");
            }

            return actualShouldMapTestBuilder;
        }
    }
}
