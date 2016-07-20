namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderViewDataExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> NoViewData<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            if (actualShouldHaveTestBuilder.TestContext.GetViewData().Count > 0)
            {
                DataProviderValidator.ThrowNewDataProviderAssertionExceptionWithNoEntries(
                    actualShouldHaveTestBuilder.TestContext,
                    ViewDataTestBuilder.ViewDataName);
            }

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> ViewData<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            int? withNumberOfEntries = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualShouldHaveTestBuilder.TestContext,
                ViewDataTestBuilder.ViewDataName,
                withNumberOfEntries,
                actualShouldHaveTestBuilder.TestContext.GetViewData().Count);

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewDataTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> ViewData<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<IViewDataTestBuilder> viewDataTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            viewDataTestBuilder(new ViewDataTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
    }
}
