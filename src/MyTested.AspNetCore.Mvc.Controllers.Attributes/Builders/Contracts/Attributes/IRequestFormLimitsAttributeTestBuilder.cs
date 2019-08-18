namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>.
    /// </summary>
    public interface IRequestFormLimitsAttributeTestBuilder 
        : IBaseAttributeTestBuilderWithOrder<IAndRequestFormLimitsAttributeTestBuilder>
    {
        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.BufferBody"/> value as the provided one.
        /// </summary>
        /// <param name="bufferBody">Expected buffer body.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithBufferBody(bool bufferBody);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.MemoryBufferThreshold"/> value as the provided one.
        /// </summary>
        /// <param name="memoryBufferThreshold">Expected memory buffer threshold.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithMemoryBufferThreshold(int memoryBufferThreshold);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.BufferBodyLengthLimit"/> value as the provided one.
        /// </summary>
        /// <param name="bufferBodyLengthLimit">Expected buffer body length limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithBufferBodyLengthLimit(long bufferBodyLengthLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.ValueCountLimit"/> value as the provided one.
        /// </summary>
        /// <param name="valueCountLimit">Expected value count limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithValueCountLimit(int valueCountLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.KeyLengthLimit"/> value as the provided one.
        /// </summary>
        /// <param name="keyLengthLimit">Expected key length limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithKeyLengthLimit(int keyLengthLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.ValueLengthLimit"/> value as the provided one.
        /// </summary>
        /// <param name="valueLengthLimit">Expected value length limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithValueLengthLimit(int valueLengthLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.MultipartBoundaryLengthLimit"/> value as the provided one.
        /// </summary>
        /// <param name="multipartBoundaryLengthLimit">Expected multipart boundary length limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithMultipartBoundaryLengthLimit(int multipartBoundaryLengthLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.MultipartHeadersCountLimit"/> value as the provided one.
        /// </summary>
        /// <param name="multipartHeadersCountLimit">Expected multipart headers count limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithMultipartHeadersCountLimit(int multipartHeadersCountLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.MultipartHeadersLengthLimit"/> value as the provided one.
        /// </summary>
        /// <param name="multipartHeadersLengthLimit">Expected multipart headers length limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithMultipartHeadersLengthLimit(int multipartHeadersLengthLimit);

        /// <summary>
        /// Tests whether a <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute"/>
        /// has the same <see cref="Microsoft.AspNetCore.Mvc.RequestFormLimitsAttribute.MultipartBodyLengthLimit"/> value as the provided one.
        /// </summary>
        /// <param name="multipartBodyLengthLimit">Expected multipart body length limit.</param>
        /// <returns>The same <see cref="IAndRequestFormLimitsAttributeTestBuilder"/>.</returns>
        IAndRequestFormLimitsAttributeTestBuilder WithMultipartBodyLengthLimit(int multipartBodyLengthLimit);
    }
}
