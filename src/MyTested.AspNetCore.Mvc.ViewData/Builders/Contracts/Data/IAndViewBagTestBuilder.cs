namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the dynamic view bag tests.
    /// </summary>
    public interface IAndViewBagTestBuilder : IViewBagTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing dynamic view bag.
        /// </summary>
        /// <returns>The same <see cref="IViewBagTestBuilder"/>.</returns>
        IViewBagTestBuilder AndAlso();
    }
}
