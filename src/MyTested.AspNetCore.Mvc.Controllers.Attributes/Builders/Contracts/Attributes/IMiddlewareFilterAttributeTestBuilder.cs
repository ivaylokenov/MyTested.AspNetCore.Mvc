namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.MiddlewareFilterAttribute"/>.
    /// </summary>
    public interface IMiddlewareFilterAttributeTestBuilder : IBaseAttributeTestBuilderWithOrder<IAndMiddlewareFilterAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.MiddlewareFilterAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.MiddlewareFilterAttribute.ConfigurationType"/> value as the provided one.
        /// </summary>
        /// <param name="configurationType">A type which configures a middleware pipeline.</param>
        /// <returns>The same <see cref="IMiddlewareFilterAttributeTestBuilder"/>.</returns>
        IAndMiddlewareFilterAttributeTestBuilder OfType(Type configurationType);
    }
}
