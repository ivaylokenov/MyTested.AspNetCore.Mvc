namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Contracts.And;
    using Contracts.Data;
    using Data;

    /// <summary>
    /// Class containing methods for testing <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>.
    /// </summary>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> NoMemoryCache()
        {
            if (this.TestContext.MockedMemoryCache.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(MemoryCacheTestBuilder.MemoryCacheName);
            }

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> MemoryCache(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                MemoryCacheTestBuilder.MemoryCacheName,
                withNumberOfEntries,
                this.TestContext.MockedMemoryCache.Count);

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> MemoryCache(Action<IMemoryCacheTestBuilder> memoryCacheTestBuilder)
        {
            memoryCacheTestBuilder(new MemoryCacheTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
