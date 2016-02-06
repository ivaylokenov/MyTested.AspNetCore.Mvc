namespace MyTested.Mvc.Builders.Contracts.Routes
{
    /// <summary>
    /// Used for adding AndAlso() method to the route test builder.
    /// </summary>
    public interface IAndResolvedRouteTestBuilder : IResolvedRouteTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building route tests.
        /// </summary>
        /// <returns>The same route builder.</returns>
        IResolvedRouteTestBuilder AndAlso();
    }
}
