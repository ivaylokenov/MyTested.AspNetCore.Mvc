namespace MyTested.Mvc.Builders.Contracts.ActionResults.Content
{
    using System.Net;
    using Models;

    /// <summary>
    /// Used for testing content result.
    /// </summary>
    public interface IContentTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether content result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode); // TODO: status code is int?

        /// <summary>
        /// Tests whether content result has the same content type as the provided string.
        /// </summary>
        /// <param name="mediaType">Media type as string.</param>
        /// <returns>The same content test builder.</returns>
        IAndContentTestBuilder WithMediaType(string mediaType);

        // TODO: is these needed?

        ///// <summary>
        ///// Tests whether content result has the same content type as the provided MediaTypeHeaderValue.
        ///// </summary>
        ///// <param name="mediaType">Media type as MediaTypeHeaderValue.</param>
        ///// <returns>The same content test builder.</returns>
        //IAndContentTestBuilder WithMediaType(MediaTypeHeaderValue mediaType);

        ///// <summary>
        ///// Tests whether content result has the default content negotiator.
        ///// </summary>
        ///// <returns>The same content test builder.</returns>
        //IAndContentTestBuilder WithDefaultContentNegotiator();

        ///// <summary>
        ///// Tests whether content result has specific type of content negotiator.
        ///// </summary>
        ///// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        ///// <returns>The same content test builder.</returns>
        //IAndContentTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator);

        ///// <summary>
        ///// Tests whether content result has specific type of content negotiator by using generic definition.
        ///// </summary>
        ///// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        ///// <returns>The same content test builder.</returns>
        //IAndContentTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
        //    where TContentNegotiator : IContentNegotiator, new();

        ///// <summary>
        ///// Tests whether created result contains the provided media type formatter.
        ///// </summary>
        ///// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        ///// <returns>The same created test builder.</returns>
        //IAndContentTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        ///// <summary>
        ///// Tests whether created result contains the provided type of media type formatter.
        ///// </summary>
        ///// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        ///// <returns>The same created test builder.</returns>
        //IAndContentTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
        //    where TMediaTypeFormatter : MediaTypeFormatter, new();

        ///// <summary>
        ///// Tests whether created result contains the default media type formatters provided by the framework.
        ///// </summary>
        ///// <returns>The same created test builder.</returns>
        //IAndContentTestBuilder ContainingDefaultFormatters();

        ///// <summary>
        ///// Tests whether created result contains exactly the same types of media type formatters as the provided collection.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        ///// <returns>The same created test builder.</returns>
        //IAndContentTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters);

        ///// <summary>
        ///// Tests whether created result contains exactly the same types of media type formatters as the provided parameters.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        ///// <returns>The same created test builder.</returns>
        //IAndContentTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters);

        ///// <summary>
        ///// Tests whether created result contains the media type formatters provided by builder.
        ///// </summary>
        ///// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        ///// <returns>The same created test builder.</returns>
        //IAndContentTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder);
    }
}
