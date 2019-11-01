namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/>.
    /// </summary>
    public interface ITypeFilterAttributeTestBuilder : IBaseAttributeTestBuilderWithOrder<IAndTypeFilterAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute.ImplementationType"/> value as the provided one.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of filter to find.</param>
        /// <returns>The same <see cref="IAndTypeFilterAttributeTestBuilder"/>.</returns>
        IAndTypeFilterAttributeTestBuilder OfType(Type type);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/>
        /// contains the provided value in its <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute.Arguments"/> collection.
        /// </summary>
        /// <param name="args">Expected arguments.</param>
        /// <returns>The same attribute test builder.</returns>
        IAndTypeFilterAttributeTestBuilder WithArguments(object[] args);
    }
}
