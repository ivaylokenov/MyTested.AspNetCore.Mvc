namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

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
