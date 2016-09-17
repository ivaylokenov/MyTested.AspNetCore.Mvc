namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponents
{
    using System;
    using Base;
    using Attributes;

    /// <summary>
    /// Used for testing view components.
    /// </summary>
    public interface IViewComponentTestBuilder
    {
        /// <summary>
        /// Tests whether the view component has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponent"/> type.</returns>
        IBaseTestBuilderWithViewComponent NoAttributes();

        /// <summary>
        /// Tests whether the view component has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested view component.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponent"/> type.</returns>
        IBaseTestBuilderWithViewComponent Attributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Tests whether the view component has specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the view component.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithViewComponent"/> type.</returns>
        IBaseTestBuilderWithViewComponent Attributes(Action<IViewComponentAttributesTestBuilder> attributesTestBuilder);
    }
}
