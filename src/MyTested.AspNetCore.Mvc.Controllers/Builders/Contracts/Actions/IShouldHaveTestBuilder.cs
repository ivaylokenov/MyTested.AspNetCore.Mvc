namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Actions
{
    using System;
    using Attributes;
    using Base;

    /// <summary>
    /// Used for testing the action's additional data - action attributes, HTTP response, view bag and more.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldHaveTestBuilder<TActionResult> : IBaseTestBuilderWithComponentShouldHaveTestBuilder<IAndActionResultTestBuilder<TActionResult>>
    {
        /// <summary>
        /// Tests whether the action has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        IAndActionResultTestBuilder<TActionResult> NoActionAttributes();

        /// <summary>
        /// Tests whether the action has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested action.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        IAndActionResultTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Tests whether the action has specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the action.</param>
        /// <returns>Test builder of <see cref="IAndActionResultTestBuilder{TActionResult}"/> type.</returns>
        IAndActionResultTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder);
    }
}
