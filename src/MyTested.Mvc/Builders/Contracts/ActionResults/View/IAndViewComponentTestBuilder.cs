namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    /// <summary>
    /// Used for adding AndAlso() method to the view component response tests.
    /// </summary>
    public interface IAndViewComponentTestBuilder : IViewComponentTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining view component result tests.
        /// </summary>
        /// <returns>View component result test builder.</returns>
        IViewComponentTestBuilder AndAlso();
    }
}
