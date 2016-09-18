namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Base interface for all attribute test builders.
    /// </summary>
    /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type for common methods.</typeparam>
    public interface IBaseAttributesTestBuilder<TAttributesTestBuilder>
    {
        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute;
    }
}
