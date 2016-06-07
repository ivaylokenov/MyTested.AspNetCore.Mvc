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
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderViewBagExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> NoViewBag<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            if (actualShouldHaveTestBuilder.TestContext.GetViewData().Count > 0)
            {
                actualShouldHaveTestBuilder.ThrowNewDataProviderAssertionExceptionWithNoEntries(ViewBagTestBuilder.ViewBagName);
            }

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> ViewBag<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            int? withNumberOfEntries = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            actualShouldHaveTestBuilder.ValidateDataProviderNumberOfEntries(
                ViewBagTestBuilder.ViewBagName,
                withNumberOfEntries,
                actualShouldHaveTestBuilder.TestContext.GetViewData().Count);

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="viewBagTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> ViewBag<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<IViewBagTestBuilder> viewBagTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            viewBagTestBuilder(new ViewBagTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
    }
}
