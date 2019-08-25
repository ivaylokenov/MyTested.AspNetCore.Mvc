namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IControllerBuilder<TController> 
        : IBaseControllerBuilder<TController, IAndControllerBuilder<TController>>,
        IControllerActionCallBuilder<TController>
        where TController : class
    {
    }
}