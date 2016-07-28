namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Routing
{
    /// <summary>
    /// Used for adding AndAlso() method to the route test builder.
    /// </summary>
    public interface IAndResolvedRouteTestBuilder : IResolvedRouteTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building route tests.
        /// </summary>
        /// <returns>The same <see cref="IResolvedRouteTestBuilder"/>.</returns>
        IResolvedRouteTestBuilder AndAlso();
    }
}
