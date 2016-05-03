namespace MyTested.Mvc.Builders.Contracts.ActionResults.Forbid
{
    /// <summary>
    /// Used for adding AndAlso() method to the forbid result tests.
    /// </summary>
    public interface IAndForbidTestBuilder : IForbidTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining forbid tests.
        /// </summary>
        /// <returns>The same <see cref="IForbidTestBuilder"/>.</returns>
        IForbidTestBuilder AndAlso();
    }
}
