namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.NotFound
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/> tests.
    /// </summary>
    public interface IAndNotFoundTestBuilder : INotFoundTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        IAndNotFoundTestBuilder AndAlso();
    }
}
