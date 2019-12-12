namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface IWithoutSessionBuilder
    {
        /// <summary>
        /// Removes session key from <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="key">The key to remove as string.</param>
        /// <returns>The same <see cref="IAndWithoutSessionTestBuilder"/>.</returns>
        IAndWithoutSessionTestBuilder WithoutEntry(string key);

        /// <summary>
        /// Removes provided keys from <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="keys">The given collection of keys to remove as string.</param>
        /// <returns>The same <see cref="IAndWithoutSessionTestBuilder"/>.</returns>
        IAndWithoutSessionTestBuilder WithoutEntries(IEnumerable<string> keys);

        /// <summary>
        /// Removes provided keys from <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="keys">The given param colletion of keys to remove as string.</param>
        /// <returns>The same <see cref="IAndWithoutSessionTestBuilder"/>.</returns>
        IAndWithoutSessionTestBuilder WithoutEntries(params string[] keys);

        /// <summary>
        /// Clears all entries from <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndWithoutSessionTestBuilder"/>.</returns>
        IAndWithoutSessionTestBuilder WithoutAllEntries();
    }
}
