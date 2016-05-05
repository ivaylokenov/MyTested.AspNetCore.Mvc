namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Http.Features.ISession"/>.
    /// </summary>
    public interface ISessionTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry with the provided key.
        /// </summary>
        /// <param name="key">Key of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntryWithKey(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry with the provided byte array value.
        /// </summary>
        /// <param name="value">Byte array value of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntryWithValue(byte[] value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry with the provided string value.
        /// </summary>
        /// <param name="value">String value of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntryWithValue(string value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry with the provided integer value.
        /// </summary>
        /// <param name="value">Integer value of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntryWithValue(int value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry
        /// with the provided key and corresponding byte array value.
        /// </summary>
        /// <param name="key">Key of the session entry.</param>
        /// <param name="value">Byte array value of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntry(string key, byte[] value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry
        /// with the provided key and corresponding string value.
        /// </summary>
        /// <param name="key">Key of the session entry.</param>
        /// <param name="value">String value of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntry(string key, string value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains entry
        /// with the provided key and corresponding integer value.
        /// </summary>
        /// <param name="key">Key of the session entry.</param>
        /// <param name="value">Integer value of the session entry.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntry(string key, int value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains the provided entries.
        /// </summary>
        /// <param name="entries">Session entries as anonymous object.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntries(object entries);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains the provided entries.
        /// </summary>
        /// <param name="entries">Session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, object> entries);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains the provided byte array entries.
        /// </summary>
        /// <param name="entries">Byte array session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, byte[]> entries);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains the provided string entries.
        /// </summary>
        /// <param name="entries">String session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, string> entries);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> contains the provided integer entries.
        /// </summary>
        /// <param name="entries">Integer session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionTestBuilder>"/>.</returns>
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, int> entries);
    }
}
