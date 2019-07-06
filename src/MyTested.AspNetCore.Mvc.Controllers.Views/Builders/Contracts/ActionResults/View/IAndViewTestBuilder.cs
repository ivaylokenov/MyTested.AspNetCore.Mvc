namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> tests.
    /// </summary>
    public interface IAndViewTestBuilder : IViewTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IViewTestBuilder"/>.</returns>
        IViewTestBuilder AndAlso();
    }
}
