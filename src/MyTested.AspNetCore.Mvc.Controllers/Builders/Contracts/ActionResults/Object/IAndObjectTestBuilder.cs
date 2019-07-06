namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Object
{
    /// <summary>
    /// Used for adding AndAlso() method to
    /// the <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> tests.
    /// </summary>
    public interface IAndObjectTestBuilder : IObjectTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IObjectTestBuilder"/>.</returns>
        IObjectTestBuilder AndAlso();
    }
}
