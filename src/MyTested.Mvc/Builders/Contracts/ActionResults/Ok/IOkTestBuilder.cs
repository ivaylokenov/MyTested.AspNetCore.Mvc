namespace MyTested.Mvc.Builders.Contracts.ActionResults.Ok
{
    using Models;

    /// <summary>
    /// Used for testing ok result.
    /// </summary>
    public interface IOkTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithNoResponseModel();
        
        // TODO: formatters?
        ///// <summary>
        ///// Tests whether ok result has the default content negotiator.
        ///// </summary>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder WithDefaultContentNegotiator();

        ///// <summary>
        ///// Tests whether ok result has specific type of content negotiator.
        ///// </summary>
        ///// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator);

        ///// <summary>
        ///// Tests whether ok result has specific type of content negotiator by using generic definition.
        ///// </summary>
        ///// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
        //    where TContentNegotiator : IContentNegotiator, new();

        ///// <summary>
        ///// Tests whether ok result contains the provided media type formatter.
        ///// </summary>
        ///// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        ///// <summary>
        ///// Tests whether ok result contains the provided type of media type formatter.
        ///// </summary>
        ///// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
        //    where TMediaTypeFormatter : MediaTypeFormatter, new();

        ///// <summary>
        ///// Tests whether ok result contains the default media type formatters provided by the framework.
        ///// </summary>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder ContainingDefaultFormatters();

        ///// <summary>
        ///// Tests whether ok result contains exactly the same types of media type formatters as the provided collection.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters);

        ///// <summary>
        ///// Tests whether ok result contains exactly the same types of media type formatters as the provided parameters.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters);

        ///// <summary>
        ///// Tests whether ok result contains the media type formatters provided by builder.
        ///// </summary>
        ///// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        ///// <returns>The same ok test builder.</returns>
        //IAndOkTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder);
    }
}
