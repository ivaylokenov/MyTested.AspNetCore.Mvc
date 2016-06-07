namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Internal.TestContexts;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderTempDataExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> NoTempData<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            if (actualShouldHaveTestBuilder.TestContext.GetTempData().Count > 0)
            {
                actualShouldHaveTestBuilder.ThrowNewDataProviderAssertionExceptionWithNoEntries(TempDataTestBuilder.TempDataName);
            }

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> TempData<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            int? withNumberOfEntries = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            actualShouldHaveTestBuilder.ValidateDataProviderNumberOfEntries(
                TempDataTestBuilder.TempDataName,
                withNumberOfEntries,
                actualShouldHaveTestBuilder.TestContext.GetTempData().Count);

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="tempDataTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> TempData<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<ITempDataTestBuilder> tempDataTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            tempDataTestBuilder(new TempDataTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
    }
}
