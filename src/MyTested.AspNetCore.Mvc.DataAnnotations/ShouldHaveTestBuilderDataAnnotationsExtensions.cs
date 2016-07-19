namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Models;
    using Builders.Models;
    using Exceptions;
    using Utilities.Extensions;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderDataAnnotationsExtensions
    {
        /// <summary>
        /// Tests whether the action has specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="modelStateTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> ModelState<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<IModelStateTestBuilder> modelStateTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            modelStateTestBuilder(new ModelStateTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action has valid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> with no errors.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> ValidModelState<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            actualShouldHaveTestBuilder.CheckValidModelState();

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action has invalid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfErrors">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> InvalidModelState<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            int? withNumberOfErrors = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            var actualModelStateErrors = actualShouldHaveTestBuilder.TestContext.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have invalid model state{2}, {3}.",
                    actualShouldHaveTestBuilder.ActionName,
                    actualShouldHaveTestBuilder.Component.GetName(),
                    withNumberOfErrors == null ? string.Empty : $" with {withNumberOfErrors} errors",
                    withNumberOfErrors == null ? "but was in fact valid" : $"but in fact contained {actualModelStateErrors}"));
            }

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
    }
}
