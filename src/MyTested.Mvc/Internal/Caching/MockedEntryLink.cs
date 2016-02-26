namespace MyTested.Mvc.Internal.Caching
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;
    using System.Linq;

    public class MockedEntryLink : IEntryLink
    {
        public DateTimeOffset? AbsoluteExpiration => DateTimeOffset.UtcNow;

        public IEnumerable<IChangeToken> ExpirationTokens => Enumerable.Empty<IChangeToken>();

        public void AddExpirationTokens(IList<IChangeToken> expirationTokens)
        {
            // intentionally does nothing
        }

        public void Dispose()
        {
            // intentionally does nothing
        }

        public void SetAbsoluteExpiration(DateTimeOffset absoluteExpiration)
        {
            // intentionally does nothing
        }
    }
}
