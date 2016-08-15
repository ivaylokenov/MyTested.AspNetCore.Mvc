namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> extension methods for <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentShouldHaveTestBuilderViewDataExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder NoViewData<TBuilder>(this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            if (actualBuilder.TestContext.GetViewData().Count > 0)
            {
                DataProviderValidator.ThrowNewDataProviderAssertionExceptionWithNoEntries(
                    actualBuilder.TestContext,
                    ViewDataTestBuilder.ViewDataName);
            }

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder ViewData<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            int? withNumberOfEntries = null)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualBuilder.TestContext,
                ViewDataTestBuilder.ViewDataName,
                withNumberOfEntries,
                actualBuilder.TestContext.GetViewData().Count);

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="viewDataTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder ViewData<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            Action<IViewDataTestBuilder> viewDataTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            viewDataTestBuilder(new ViewDataTestBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
