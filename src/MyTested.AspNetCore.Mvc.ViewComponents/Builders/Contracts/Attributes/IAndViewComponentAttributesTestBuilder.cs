namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the view component attributes tests.
    /// </summary>
    public interface IAndViewComponentAttributesTestBuilder : IViewComponentAttributesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining view component attributes tests.
        /// </summary>
        /// <returns>The same <see cref="IViewComponentAttributesTestBuilder"/>.</returns>
        IViewComponentAttributesTestBuilder AndAlso();
    }
}
