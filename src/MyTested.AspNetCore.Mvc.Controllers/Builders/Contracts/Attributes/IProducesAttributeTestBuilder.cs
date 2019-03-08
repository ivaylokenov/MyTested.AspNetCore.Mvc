namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
    /// </summary>
    public interface IProducesAttributeTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>
        /// contains the provided value in its <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.ContentTypes"/> collection.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> contains the provided content types
        /// in its <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.ContentTypes"/> collection.
        /// </summary>
        /// <param name="contentTypes">Expected content types.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> contains the provided content types
        /// in its <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.ContentTypes"/> collection.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="otherContentTypes">Expected other content types.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithContentTypes(string contentType, params string[] otherContentTypes);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.Type"/> value as the provided one.
        /// </summary>
        /// <param name="type">Expected type value.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithType(Type type);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.Order"/> value as the provided one.
        /// </summary>
        /// <param name="order">Expected order value.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithOrder(int order);
    }
}
