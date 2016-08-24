namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Uri
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="System.Uri"/> builder.
    /// </summary>
    public interface IAndUriBuilder : IUriBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="System.Uri"/>.
        /// </summary>
        /// <returns>The same <see cref="IUriBuilder"/>.</returns>
        IUriBuilder AndAlso();
    }
}
