namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    /// <summary>
    /// Used for adding AndAlso() method to the view response tests.
    /// </summary>
    public interface IAndViewTestBuilder : IViewTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining view result tests.
        /// </summary>
        /// <returns>View result test builder.</returns>
        IViewTestBuilder AndAlso();
    }
}
