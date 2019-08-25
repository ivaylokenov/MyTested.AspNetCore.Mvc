namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    /// <summary>
    /// Used for adding AndAlso() method to the controller instance builder.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IAndControllerInstanceBuilder<TController> : IControllerInstanceBuilder<TController>
        where TController : class
    {
        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same <see cref="IAndControllerBuilder{TController}"/>.</returns>
        IAndControllerInstanceBuilder<TController> AndAlso();
    }
}
