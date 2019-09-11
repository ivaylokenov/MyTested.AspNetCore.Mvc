namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>.
    /// </summary>
    public interface IProducesAttributeTestBuilder : IBaseAttributeTestBuilderWithOrder<IAndProducesAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>
        /// contains the provided value in its <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.ContentTypes"/> collection.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> contains the provided content types
        /// in its <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.ContentTypes"/> collection.
        /// </summary>
        /// <param name="contentTypes">Expected content types.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/> contains the provided content types
        /// in its <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.ContentTypes"/> collection.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="otherContentTypes">Expected other content types.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder WithContentTypes(string contentType, params string[] otherContentTypes);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.ProducesAttribute.Type"/> value as the provided one.
        /// </summary>
        /// <param name="type">Expected type value.</param>
        /// <returns>The same <see cref="IAndProducesAttributeTestBuilder"/>.</returns>
        IAndProducesAttributeTestBuilder OfType(Type type);
    }
}
