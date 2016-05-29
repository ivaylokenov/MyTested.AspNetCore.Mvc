namespace MyTested.Mvc.Builders.Contracts.ActionResults.Forbid
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/> tests.
    /// </summary>
    public interface IAndForbidTestBuilder : IForbidTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IForbidTestBuilder"/>.</returns>
        IForbidTestBuilder AndAlso();
    }
}
