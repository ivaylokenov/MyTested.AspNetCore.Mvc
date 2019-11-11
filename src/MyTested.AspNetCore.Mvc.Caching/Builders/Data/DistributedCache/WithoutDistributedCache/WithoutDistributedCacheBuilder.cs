namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache.WithoutDistributedCache
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;

    public class WithoutDistributedCacheBuilder : BaseDistributedCacheBuilder, IAndWithoutDistributedCache
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutDistributedCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IDistributedCache"/>.</param>
        public WithoutDistributedCacheBuilder(IServiceProvider services)
            : base(services)
        {
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCache WithoutAllEntries()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCache WithoutEntries(IEnumerable<object> keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCache WithoutEntries(params object[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IAndWithoutDistributedCache WithoutEntry(object key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IWithoutDistributedCache AndAlso() => this;
    }
}
