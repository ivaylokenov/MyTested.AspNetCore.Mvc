namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface IWithoutSessionBuilder
    {
        /// <summary>
        /// Removes session key from <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <param name="key">The key to remove as string.</param>
        /// <returns>The same <see cref="IAndWithoutSessionBuilder"/>.</returns>
        IAndWithoutSessionBuilder WithoutEntry(string key);

        /// <summary>
        /// Clears the whole <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndWithoutSessionBuilder"/>.</returns>
        IAndWithoutSessionBuilder ClearSession();
    }
}
