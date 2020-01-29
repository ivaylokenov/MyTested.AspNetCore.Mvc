namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ServiceFilterAttribute"/>.
    /// </summary>
    public interface IServiceFilterAttributeTestBuilder : IBaseAttributeTestBuilderWithOrder<IAndServiceFilterAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ServiceFilterAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.ServiceFilterAttribute.ServiceType"/> value as the provided one.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of filter to find.</param>
        /// <returns>The same <see cref="IAndServiceFilterAttributeTestBuilder"/>.</returns>
        IAndServiceFilterAttributeTestBuilder OfType(Type type);
    }
}
