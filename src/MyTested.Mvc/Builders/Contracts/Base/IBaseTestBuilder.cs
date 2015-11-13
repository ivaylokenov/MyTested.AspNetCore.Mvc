namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc;
    using System.Collections.Generic;

    /// <summary>
    /// Base interface for all test builders.
    /// </summary>
    public interface IBaseTestBuilder
    {
        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET MVC 6 controller on which the action is tested.</returns>
        Controller AndProvideTheController();

        /// <summary>
        /// Gets the HTTP context with which the action will be tested.
        /// </summary>
        /// <returns>HttpContext from the tested controller.</returns>
        HttpContext AndProvideTheHttpContext();

        /// <summary>
        /// Gets the HTTP request with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequest from the tested controller.</returns>
        HttpRequest AndProvideTheHttpRequest();

        /// <summary>
        /// Gets the attributes on the tested controller..
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the controller.</returns>
        IEnumerable<object> AndProvideTheControllerAttributes();
    }
}
