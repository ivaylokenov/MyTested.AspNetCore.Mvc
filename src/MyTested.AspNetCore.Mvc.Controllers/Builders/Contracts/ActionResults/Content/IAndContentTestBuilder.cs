namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Content
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> tests.
    /// </summary>
    public interface IAndContentTestBuilder : IContentTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IContentTestBuilder"/>.</returns>
        IContentTestBuilder AndAlso();
    }
}
