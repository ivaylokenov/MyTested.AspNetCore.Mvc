namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Pipeline
{
    /// <summary>
    /// Used for adding AndAlso() method to the controller instance builder from a route test.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IAndWhichControllerInstanceBuilder<TController> : IWhichControllerInstanceBuilder<TController>
        where TController : class
    {
        /// <summary>
        /// AndAlso method for better readability when building controller instance from a route test.
        /// </summary>
        /// <returns>The same <see cref="IWhichControllerInstanceBuilder{TController}"/>.</returns>
        IWhichControllerInstanceBuilder<TController> AndAlso();
    }
}
