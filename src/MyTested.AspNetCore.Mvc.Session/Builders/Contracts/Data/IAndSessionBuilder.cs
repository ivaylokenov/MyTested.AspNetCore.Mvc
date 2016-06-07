namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Http.ISession"/> builder.
    /// </summary>
    public interface IAndSessionBuilder : ISessionBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <returns>The same <see cref="ISessionBuilder"/>.</returns>
        ISessionBuilder AndAlso();
    }
}
