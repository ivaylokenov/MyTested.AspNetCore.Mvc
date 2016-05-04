namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> tests.
    /// </summary>
    public interface IAndViewComponentTestBuilder : IViewComponentTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IViewComponentTestBuilder"/>.</returns>
        IViewComponentTestBuilder AndAlso();
    }
}
