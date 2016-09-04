namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for testing view component attributes.
    /// </summary>
    public interface IViewComponentAttributesTestBuilder : IBaseAttributesTestBuilder<IAndViewComponentAttributesTestBuilder>
    {
        /// <summary>
        /// Tests whether the view component attributes contain <see cref="Microsoft.AspNetCore.Mvc.ViewComponentAttribute"/>.
        /// </summary>
        /// <param name="viewComponentName">Expected overridden name of the view component.</param>
        /// <returns>The same <see cref="IAndViewComponentAttributesTestBuilder"/>.</returns>
        IAndViewComponentAttributesTestBuilder ChangingViewComponentNameTo(string viewComponentName);
    }
}
