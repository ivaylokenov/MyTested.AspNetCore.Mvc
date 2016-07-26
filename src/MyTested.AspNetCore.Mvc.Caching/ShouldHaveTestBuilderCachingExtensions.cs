namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderCachingExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IAndActionResultTestBuilder<TActionResult> NoMemoryCache<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            if (actualShouldHaveTestBuilder.TestContext.GetMockedMemoryCache().Count > 0)
            {
                DataProviderValidator.ThrowNewDataProviderAssertionExceptionWithNoEntries(
                    actualShouldHaveTestBuilder.TestContext,
                    MemoryCacheTestBuilder.MemoryCacheName);
            }

            return actualShouldHaveTestBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IAndActionResultTestBuilder<TActionResult> MemoryCache<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            int? withNumberOfEntries = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualShouldHaveTestBuilder.TestContext,
                MemoryCacheTestBuilder.MemoryCacheName,
                withNumberOfEntries,
                actualShouldHaveTestBuilder.TestContext.GetMockedMemoryCache().Count);

            return actualShouldHaveTestBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the action sets specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="memoryCacheTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IAndActionResultTestBuilder<TActionResult> MemoryCache<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<IMemoryCacheTestBuilder> memoryCacheTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            memoryCacheTestBuilder(new MemoryCacheTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.Builder;
        }
    }
}
