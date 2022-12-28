namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute"/>.
    /// </summary>
    public interface IProducesResponseTypeAttributeTestBuilder
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute.Type"/> value as the provided one.
        /// </summary>
        /// <param name="type">Expected type.</param>
        /// <returns>The same <see cref="IAndProducesResponseTypeAttributeTestBuilder"/>.</returns>
        IAndProducesResponseTypeAttributeTestBuilder OfType(Type type);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute.StatusCode"/> value as the provided one.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same <see cref="IAndProducesResponseTypeAttributeTestBuilder"/>.</returns>
        IAndProducesResponseTypeAttributeTestBuilder WithStatusCode(int statusCode);
    }
}
