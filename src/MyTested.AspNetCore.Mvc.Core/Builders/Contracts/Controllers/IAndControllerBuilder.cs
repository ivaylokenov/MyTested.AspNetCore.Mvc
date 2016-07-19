namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    using Base;

    /// <summary>
    /// Used for adding AndAlso() method to the controller builder.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IAndControllerBuilder<TController> : IControllerBuilder<TController>, IBaseTestBuilderWithComponent<IAndControllerBuilder<TController>>
        where TController : class
    {
        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same <see cref="IAndControllerBuilder{TController}"/>.</returns>
        IAndControllerBuilder<TController> AndAlso();
    }
}
