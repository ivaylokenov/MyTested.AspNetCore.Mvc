namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    using System;
    using Attributes;
    using Base;

    /// <summary>
    /// Used for testing the view component's additional data - attributes, HTTP response, view bag and more.
    /// </summary>
    /// <typeparam name="TInvocationResult">Result from invoked view component in ASP.NET Core MVC.</typeparam>
    public interface IViewComponentShouldHaveTestBuilder<TInvocationResult>
        : IBaseTestBuilderWithComponentShouldHaveTestBuilder<IAndViewComponentResultTestBuilder<TInvocationResult>>
    {
        /// <summary>
        /// Tests whether the view component has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder of <see cref="IAndViewComponentResultTestBuilder{TInvocationResult}"/> type.</returns>
        IAndViewComponentResultTestBuilder<TInvocationResult> NoAttributes();

        /// <summary>
        /// Tests whether the view component has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested view component.</param>
        /// <returns>Test builder of <see cref="IAndViewComponentResultTestBuilder{TInvocationResult}"/> type.</returns>
        IAndViewComponentResultTestBuilder<TInvocationResult> Attributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Tests whether the view component has specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the view component.</param>
        /// <returns>Test builder of <see cref="IAndViewComponentResultTestBuilder{TInvocationResult}"/> type.</returns>
        IAndViewComponentResultTestBuilder<TInvocationResult> Attributes(
            Action<IViewComponentAttributesTestBuilder> attributesTestBuilder);
    }
}
