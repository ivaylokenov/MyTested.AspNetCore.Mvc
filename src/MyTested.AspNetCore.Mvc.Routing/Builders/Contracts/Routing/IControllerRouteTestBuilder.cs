namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Routing
{
    /// <summary>
    /// Used for testing controller route details.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IControllerRouteTestBuilder<TController> : IAndResolvedRouteTestBuilder
    {
    }
}
