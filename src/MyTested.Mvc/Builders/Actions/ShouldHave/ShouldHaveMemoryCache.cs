namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using Contracts.And;
    using Contracts.Data;
    using Data;
    using System;

    /// <summary>
    /// Class containing methods for testing memory cache.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        public IAndTestBuilder<TActionResult> NoMemoryCache()
        {
            if (this.TestContext.MockedMemoryCache.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(MemoryCacheTestBuilder.MemoryCacheName);
            }

            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> MemoryCache(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                MemoryCacheTestBuilder.MemoryCacheName,
                withNumberOfEntries,
                this.TestContext.MockedMemoryCache.Count);

            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> MemoryCache(Action<IMemoryCacheTestBuilder> memoryCacheTestBuilder)
        {
            memoryCacheTestBuilder(new MemoryCacheTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
