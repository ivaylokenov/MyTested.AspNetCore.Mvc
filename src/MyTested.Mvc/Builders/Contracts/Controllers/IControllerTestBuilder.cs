namespace MyTested.Mvc.Builders.Contracts.Controllers
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
        /// Checks whether the tested controller has no attributes of any type. 
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder NoAttributes();

        /// <summary>
        /// Checks whether the tested controller has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested controller.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder Attributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Checks whether the tested controller has at specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the controller.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder Attributes(Action<IControllerAttributesTestBuilder> attributesTestBuilder);
    }
}
