namespace MyTested.Mvc.Builders.Contracts.Base
{
    using System.Collections.Generic;

    /// <summary>
    /// Base class for all test builders with controller.
    /// </summary>
    public interface IBaseTestBuilderWithController : IBaseTestBuilder
    {
        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET MVC controller on which the action is tested.</returns>
        object AndProvideTheController();

        /// <summary>
        /// Gets the attributes on the tested controller..
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the controller.</returns>
        IEnumerable<object> AndProvideTheControllerAttributes();
    }
}
