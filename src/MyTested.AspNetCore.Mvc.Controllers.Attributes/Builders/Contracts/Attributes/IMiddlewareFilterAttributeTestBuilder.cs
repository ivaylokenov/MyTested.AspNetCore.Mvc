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
        /// <param name="type">Expected type value.</param>
        /// <returns>The same <see cref="IMiddlewareFilterAttributeTestBuilder"/>.</returns>
        IAndMiddlewareFilterAttributeTestBuilder WithType(Type type);
    }
}
