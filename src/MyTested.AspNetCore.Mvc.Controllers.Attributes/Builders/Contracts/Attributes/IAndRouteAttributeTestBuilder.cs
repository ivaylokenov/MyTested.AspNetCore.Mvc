namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/> tests.
    /// </summary>
    public interface IAndRouteAttributeTestBuilder : IRouteAttributeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IRouteAttributeTestBuilder"/>.</returns>
        IRouteAttributeTestBuilder AndAlso();
    }
}
