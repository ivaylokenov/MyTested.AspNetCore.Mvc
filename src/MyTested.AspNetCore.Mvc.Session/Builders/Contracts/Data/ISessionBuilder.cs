namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface ISessionBuilder
    {
        /// <summary>
        /// Sets session ID to the build <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="sessionId">Session ID to set as string.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithId(string sessionId);

        /// <summary>
        /// Adds byte array session entry to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="key">Key to set as string.</param>
        /// <param name="value">Value to set as byte array.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntry(string key, byte[] value);

        /// <summary>
        /// Adds string session entry to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="key">Key to set as string.</param>
        /// <param name="value">Value to set as string.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntry(string key, string value);

        /// <summary>
        /// Adds integer session entry to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="key">Key to set as string.</param>
        /// <param name="value">Value to set as integer.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntry(string key, int value);

        /// <summary>
        /// Adds session entries to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="entries">Session entries as anonymous object.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntries(object entries);

        /// <summary>
        /// Adds byte array session entries to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="entries">Byte array session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntries(IDictionary<string, byte[]> entries);

        /// <summary>
        /// Adds string session entries to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="entries">String session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntries(IDictionary<string, string> entries);

        /// <summary>
        /// Adds integer session entries to the built <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="entries">Integer session entries as dictionary.</param>
        /// <returns>The same <see cref="IAndSessionBuilder"/>.</returns>
        IAndSessionBuilder WithEntries(IDictionary<string, int> entries);
    }
}
