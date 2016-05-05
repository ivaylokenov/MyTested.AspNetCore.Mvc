namespace MyTested.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Http.Features.ISession"/> builder.
    /// </summary>
    public interface IAndSessionBuilder : ISessionBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.AspNetCore.Http.Features.ISession"/>.
        /// </summary>
        /// <returns>The same <see cref="ISessionBuilder"/>.</returns>
        ISessionBuilder AndAlso();
    }
}
