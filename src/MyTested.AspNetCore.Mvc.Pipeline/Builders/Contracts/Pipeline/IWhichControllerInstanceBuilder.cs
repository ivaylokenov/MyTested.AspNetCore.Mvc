namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Pipeline
{
    using Builders.Contracts.Actions;
    using Controllers;

    /// <summary>
    /// Used for building the controller which will be tested after a route assertion.
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    public interface IWhichControllerInstanceBuilder<TController> 
        : IBaseControllerBuilder<TController, IAndWhichControllerInstanceBuilder<TController>>,
        IActionResultTestBuilder<object>
        where TController : class
    {
    }
}
