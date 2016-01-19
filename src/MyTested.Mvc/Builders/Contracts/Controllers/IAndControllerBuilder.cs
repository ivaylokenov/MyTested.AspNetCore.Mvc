namespace MyTested.Mvc.Builders.Contracts.Controllers
{
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for adding AndAlso() method to controller builder.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
    public interface IAndControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : Controller
    {
        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> AndAlso();
    }
}
