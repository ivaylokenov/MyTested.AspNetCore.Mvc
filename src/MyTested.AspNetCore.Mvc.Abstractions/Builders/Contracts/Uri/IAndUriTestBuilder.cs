namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Uri
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="System.Uri"/> tests.
    /// </summary>
    public interface IAndUriTestBuilder : IUriTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="System.Uri"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IUriTestBuilder"/>.</returns>
        IUriTestBuilder AndAlso();
    }
}
