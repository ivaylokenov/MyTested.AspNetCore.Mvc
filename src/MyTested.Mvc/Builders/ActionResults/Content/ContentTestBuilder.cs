namespace MyTested.Mvc.Builders.ActionResults.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Common.Extensions;
    using Exceptions;
    using Models;
    using Utilities.Validators;
    using Contracts.ActionResults.Content;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing content result.
    /// </summary>
    public class ContentTestBuilder
        : BaseResponseModelTestBuilder<ContentResult>, IAndContentTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ContentTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            ContentResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether content result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            // TODO: no generics

            //RuntimeBinderValidator.ValidateBinding(() =>
            //{
            //    var actualStatusCode = (HttpStatusCode)this.GetActionResultAsDynamic().StatusCode;
            //    if (actualStatusCode != statusCode)
            //    {
            //        throw new ContentResultAssertionException(string.Format(
            //            "When calling {0} action in {1} expected to have {2} ({3}) status code, but received {4} ({5}).",
            //            this.ActionName,
            //            this.Controller.GetName(),
            //            (int)statusCode,
            //            statusCode,
            //            (int)actualStatusCode,
            //            actualStatusCode));
            //    }
            //});

            return this;
        }

        /// <summary>
        /// Tests whether content result has the same content type as the provided string.
        /// </summary>
        /// <param name="mediaType">Media type as string.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithMediaType(string mediaType)
        {
            return this.WithMediaType(new MediaTypeHeaderValue(mediaType));
        }

        /// <summary>
        /// Tests whether content result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="mediaType">Media type as MediaTypeHeaderValue.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithMediaType(MediaTypeHeaderValue mediaType)
        {
            // TODO: check, no generics
            //RuntimeBinderValidator.ValidateBinding(() =>
            //{
            //    var actualMediaType = this.GetActionResultAsDynamic().MediaType as MediaTypeHeaderValue;
            //    if ((mediaType == null && actualMediaType != null)
            //        || (mediaType != null && actualMediaType == null)
            //        || (mediaType != null && mediaType.MediaType != actualMediaType.MediaType))
            //    {
            //        this.ThrowNewContentResultAssertionException(
            //            "MediaType",
            //            string.Format("to be {0}", mediaType != null ? mediaType.MediaType : "null"),
            //            string.Format("instead received {0}", actualMediaType != null ? actualMediaType.MediaType : "null"));
            //    }
            //});

            return this;
        }

        /// <summary>
        /// Tests whether content result has the default content negotiator.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithDefaultContentNegotiator()
        {
            // TODO: check, no generics
            // return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
            return null;
        }

        // TODO: check, no generics
        /// <summary>
        /// Tests whether content result has specific type of content negotiator.
        /// </summary>
        /// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        /// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator)
        //{
        //    //ContentNegotiatorValidator.ValidateContentNegotiator(
        //    //    this.ActionResult,
        //    //    contentNegotiator,
        //    //    this.ThrowNewContentResultAssertionException);

        //    return this;
        //}

        /// <summary>
        /// Tests whether content result has specific type of content negotiator by using generic definition.
        /// </summary>
        /// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        /// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
        //    where TContentNegotiator : IContentNegotiator, new()
        //{
        //    return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        //}

            // TODO: formatters
        /// <summary>
        /// Tests whether content result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
        //        this.ActionResult,
        //        mediaTypeFormatter,
        //        this.ThrowNewContentResultAssertionException);

        //    return this;
        //}

            // TODO: no formatters

        /// <summary>
        /// Tests whether content result contains the provided type of media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
        //    where TMediaTypeFormatter : MediaTypeFormatter, new()
        //{
        //    return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        //}

        ///// <summary>
        ///// Tests whether content result contains the default media type formatters provided by the framework.
        ///// </summary>
        ///// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder ContainingDefaultFormatters()
        //{
        //    return this.ContainingMediaTypeFormatters(MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters());
        //}

        ///// <summary>
        ///// Tests whether content result contains exactly the same types of media type formatters as the provided collection.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        ///// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
        //        this.ActionResult,
        //        mediaTypeFormatters,
        //        this.ThrowNewContentResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether content result contains exactly the same types of media type formatters as the provided parameters.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        ///// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        //{
        //    return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        //}

        ///// <summary>
        ///// Tests whether content result contains the media type formatters provided by builder.
        ///// </summary>
        ///// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        ///// <returns>The same content test builder.</returns>
        //public IAndContentTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
        //        this.ActionResult,
        //        formattersBuilder,
        //        this.ThrowNewContentResultAssertionException);

        //    return this;
        //}

        /// <summary>
        /// AndAlso method for better readability when chaining content tests.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        public IContentTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewContentResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            // TODO: is this needed?

            //throw new ContentResultAssertionException(string.Format(
            //        "When calling {0} action in {1} expected content result {2} {3}, but {4}.",
            //        this.ActionName,
            //        this.Controller.GetName(),
            //        propertyName,
            //        expectedValue,
            //        actualValue));
        }
    }
}
