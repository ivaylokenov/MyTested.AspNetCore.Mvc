namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Base interface for all attribute test builders with order.
    /// </summary>
    /// <typeparam name="TAttributeTestBuilder">Type of attribute test builder to use as a return type for common methods.</typeparam>
    public interface IBaseAttributeTestBuilderWithOrder<TAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether an attribute has the same order value as the provided one.
        /// </summary>
        /// <param name="order">Expected order.</param>
        /// <returns>The same attribute test builder.</returns>
        TAttributeTestBuilder WithOrder(int order);
    }
}
