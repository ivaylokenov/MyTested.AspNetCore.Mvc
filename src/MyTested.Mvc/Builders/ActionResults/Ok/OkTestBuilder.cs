namespace MyTested.Mvc.Builders.ActionResults.Ok
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.Extensions;
    using Exceptions;
    using Models;
    using Contracts.ActionResults.Ok;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing OK result.
    /// </summary>
    /// <typeparam name="TActionResult">Type of OK result - HttpOkResult or HttpOkObjectResult.</typeparam>
    public class OkTestBuilder<TActionResult>
        : BaseResponseModelTestBuilder<TActionResult>, IAndOkTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public OkTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        public IAndOkTestBuilder WithNoResponseModel()
        {
            var actualResult = this.ActionResult as HttpOkResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }

        // TODO: formatters?
        ///// <summary>
        ///// Tests whether ok result has the default content negotiator.
        ///// </summary>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder WithDefaultContentNegotiator()
        //{
        //    return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
        //}

        ///// <summary>
        ///// Tests whether ok result has specific type of content negotiator.
        ///// </summary>
        ///// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator)
        //{
        //    ContentNegotiatorValidator.ValidateContentNegotiator(
        //        this.ActionResult,
        //        contentNegotiator,
        //        this.ThrowNewOkResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether ok result has specific type of content negotiator by using generic definition.
        ///// </summary>
        ///// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
        //    where TContentNegotiator : IContentNegotiator, new()
        //{
        //    return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        //}

        ///// <summary>
        ///// Tests whether ok result contains the provided media type formatter.
        ///// </summary>
        ///// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
        //        this.ActionResult,
        //        mediaTypeFormatter,
        //        this.ThrowNewOkResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether ok result contains the provided type of media type formatter.
        ///// </summary>
        ///// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
        //    where TMediaTypeFormatter : MediaTypeFormatter, new()
        //{
        //    return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        //}

        ///// <summary>
        ///// Tests whether ok result contains the default media type formatters provided by the framework.
        ///// </summary>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder ContainingDefaultFormatters()
        //{
        //    return this.ContainingMediaTypeFormatters(MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters());
        //}

        ///// <summary>
        ///// Tests whether ok result contains exactly the same types of media type formatters as the provided collection.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
        //        this.ActionResult,
        //        mediaTypeFormatters,
        //        this.ThrowNewOkResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether ok result contains exactly the same types of media type formatters as the provided parameters.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        //{
        //    return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        //}

        ///// <summary>
        ///// Tests whether ok result contains the media type formatters provided by builder.
        ///// </summary>
        ///// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        ///// <returns>The same ok test builder.</returns>
        //public IAndOkTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
        //        this.ActionResult,
        //        formattersBuilder,
        //        this.ThrowNewOkResultAssertionException);

        //    return this;
        //}

        /// <summary>
        /// AndAlso method for better readability when chaining ok tests.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        public IOkTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewOkResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new OkResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected ok result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
