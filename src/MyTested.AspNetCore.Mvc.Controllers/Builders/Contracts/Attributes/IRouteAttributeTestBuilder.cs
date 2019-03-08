namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>.
    /// </summary>
    public interface IRouteAttributeTestBuilder
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute.Template"/> value as the provided one.
        /// </summary>
        /// <param name="template">Expected template.</param>
        /// <returns>The same <see cref="IAndRouteAttributeTestBuilder"/>.</returns>
        IAndRouteAttributeTestBuilder WithTemplate(string template);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute.Name"/> value as the provided one.
        /// </summary>
        /// <param name="name">Expected name.</param>
        /// <returns>The same <see cref="IAndRouteAttributeTestBuilder"/>.</returns>
        IAndRouteAttributeTestBuilder WithName(string name);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RouteAttribute.Order"/> value as the provided one.
        /// </summary>
        /// <param name="order">Expected order.</param>
        /// <returns>The same <see cref="IAndRouteAttributeTestBuilder"/>.</returns>
        IAndRouteAttributeTestBuilder WithOrder(int order);
    }
}
