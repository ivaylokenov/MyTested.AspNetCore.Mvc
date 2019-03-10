namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Base interface for all attribute test builders.
    /// </summary>
    /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type for common methods.</typeparam>
    public interface IBaseAttributesTestBuilder<TAttributesTestBuilder>
        where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
    {
        /// <summary>
        /// Tests whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute;

        /// <summary>
        /// Tests whether the collected attributes contain the provided attribute type passing the given assertions.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="assertions">Action containing assertions on the provided attribute.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder PassingFor<TAttribute>(Action<TAttribute> assertions)
            where TAttribute : Attribute;

        /// <summary>
        /// Tests whether the collected attributes contain the provided attribute type passing the given predicate.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <param name="predicate">Predicate testing the provided attribute.</param>
        /// <returns>The same attributes test builder.</returns>
        TAttributesTestBuilder PassingFor<TAttribute>(Func<TAttribute, bool> predicate)
            where TAttribute : Attribute;
    }
}
