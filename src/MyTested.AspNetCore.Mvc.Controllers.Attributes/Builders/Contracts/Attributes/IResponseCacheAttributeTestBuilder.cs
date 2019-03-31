namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ResponseCacheAttribute"/>.
    /// </summary>
    public interface IResponseCacheAttributeTestBuilder : IBaseAttributeTestBuilderWithOrder<IAndResponseCacheAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.Duration"/> value as the provided one.
        /// </summary>
        /// <param name="duration">Expected duration in seconds.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithDuration(int duration);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.Location"/> value as the provided one.
        /// </summary>
        /// <param name="location">Expected location as <see cref="ResponseCacheLocation"/>.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithLocation(ResponseCacheLocation location);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.NoStore"/> value as the provided one.
        /// </summary>
        /// <param name="noStore">Expected <see cref="ResponseCacheAttribute.NoStore"/> value.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithNoStore(bool noStore);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.VaryByHeader"/> value as the provided one.
        /// </summary>
        /// <param name="varyByHeader">Expected <see cref="ResponseCacheAttribute.VaryByHeader"/> value.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithVaryByHeader(string varyByHeader);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// contains the provided value in its <see cref="ResponseCacheAttribute.VaryByQueryKeys"/> collection.
        /// </summary>
        /// <param name="varyByQueryKey">Expected query key value.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithVaryByQueryKey(string varyByQueryKey);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.VaryByQueryKeys"/> value as the provided one.
        /// </summary>
        /// <param name="varyByQueryKeys">Expected <see cref="ResponseCacheAttribute.VaryByQueryKeys"/> values.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithVaryByQueryKeys(IEnumerable<string> varyByQueryKeys);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.VaryByQueryKeys"/> value as the provided one.
        /// </summary>
        /// <param name="varyByQueryKeys">Expected <see cref="ResponseCacheAttribute.VaryByQueryKeys"/> values.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithVaryByQueryKeys(params string[] varyByQueryKeys);

        /// <summary>
        /// Tests whether a <see cref="ResponseCacheAttribute"/>
        /// has the same <see cref="ResponseCacheAttribute.CacheProfileName"/> value as the provided one.
        /// </summary>
        /// <param name="cacheProfileName">Expected cache profile name.</param>
        /// <returns>The same <see cref="IAndResponseCacheAttributeTestBuilder"/>.</returns>
        IAndResponseCacheAttributeTestBuilder WithCacheProfileName(string cacheProfileName);
    }
}
