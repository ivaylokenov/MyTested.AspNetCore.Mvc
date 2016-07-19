namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    using System;
    using Attributes;
    using Base;

    /// <summary>
    /// Used for testing controllers.
    /// </summary>
    public interface IControllerTestBuilder
    {
        /// <summary>
        /// Tests whether the controller has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithComponent"/> type.</returns>
        IBaseTestBuilderWithComponent NoAttributes();

        /// <summary>
        /// Tests whether the controller has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested controller.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithComponent"/> type.</returns>
        IBaseTestBuilderWithComponent Attributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Tests whether the controller has specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the controller.</param>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithComponent"/> type.</returns>
        IBaseTestBuilderWithComponent Attributes(Action<IControllerAttributesTestBuilder> attributesTestBuilder);
    }
}
