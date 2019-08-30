namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers
{
    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IControllerInstanceBuilder<TController> : IBaseControllerBuilder<TController, IAndControllerInstanceBuilder<TController>>
        where TController : class
    {
    }
}
