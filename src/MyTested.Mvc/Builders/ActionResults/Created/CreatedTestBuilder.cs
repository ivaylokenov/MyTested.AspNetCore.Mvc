namespace MyTested.Mvc.Builders.ActionResults.Created
{
    using System;
    using System.Linq.Expressions;
    using Common.Extensions;
    using Contracts.Uris;
    using Exceptions;
    using Models;
    using Utilities.Validators;
    using Contracts.ActionResults.Created;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    /// <typeparam name="TCreatedResult">Type of created result - CreatedAtActionResult or CreatedAtRouteResult.</typeparam>
    public class CreatedTestBuilder<TCreatedResult>
        : BaseResponseModelTestBuilder<TCreatedResult>, IAndCreatedTestBuilder
    {
        private const string RouteName = "route name";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedTestBuilder{TCreatedResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public CreatedTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TCreatedResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        // TODO: content negotiator?
        ///// <summary>
        ///// Tests whether created result has the default content negotiator.
        ///// </summary>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder WithDefaultContentNegotiator()
        //{
        //    return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
        //}

        ///// <summary>
        ///// Tests whether created result has specific type of content negotiator.
        ///// </summary>
        ///// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator)
        //{
        //    ContentNegotiatorValidator.ValidateContentNegotiator(
        //        this.ActionResult,
        //        contentNegotiator,
        //        this.ThrowNewCreatedResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether created result has specific type of content negotiator by using generic definition.
        ///// </summary>
        ///// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
        //    where TContentNegotiator : IContentNegotiator, new()
        //{
        //    return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        //}

        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewCreatedResultAssertionException);
            return this.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(Uri location)
        {
            LocationValidator.ValidateUri(
                this.ActionResult,
                location,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.ActionResult,
                uriTestBuilder,
                this.ThrowNewCreatedResultAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder At<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller
        {
            return this.AtRoute<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller
        {
            return this.AtRoute<TController>(actionCall);
        }

        // TODO: formatters?
        ///// <summary>
        ///// Tests whether created result contains the provided media type formatter.
        ///// </summary>
        ///// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
        //        this.ActionResult,
        //        mediaTypeFormatter,
        //        this.ThrowNewCreatedResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether created result contains the provided type of media type formatter.
        ///// </summary>
        ///// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
        //    where TMediaTypeFormatter : MediaTypeFormatter, new()
        //{
        //    return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        //}

        ///// <summary>
        ///// Tests whether created result contains the default media type formatters provided by the framework.
        ///// </summary>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder ContainingDefaultFormatters()
        //{
        //    return this.ContainingMediaTypeFormatters(MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters());
        //}

        ///// <summary>
        ///// Tests whether created result contains exactly the same types of media type formatters as the provided collection.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
        //        this.ActionResult,
        //        mediaTypeFormatters,
        //        this.ThrowNewCreatedResultAssertionException);

        //    return this;
        //}

        ///// <summary>
        ///// Tests whether created result contains exactly the same types of media type formatters as the provided parameters.
        ///// </summary>
        ///// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        //{
        //    return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        //}

        ///// <summary>
        ///// Tests whether created result contains the media type formatters provided by builder.
        ///// </summary>
        ///// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        ///// <returns>The same created test builder.</returns>
        //public IAndCreatedTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        //{
        //    MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
        //        this.ActionResult,
        //        formattersBuilder,
        //        this.ThrowNewCreatedResultAssertionException);

        //    return this;
        //}

        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        public ICreatedTestBuilder AndAlso()
        {
            return this;
        }

        private IAndCreatedTestBuilder AtRoute<TController>(LambdaExpression actionCall)
            where TController : Controller
        {
            // TODO: ?
            //RuntimeBinderValidator.ValidateBinding(() =>
            //{
            //    var createdAtRouteResult = this.GetActionResultAsDynamic();
            //    RouteValidator.Validate<TController>(
            //        createdAtRouteResult.Request,
            //        createdAtRouteResult.RouteName,
            //        createdAtRouteResult.RouteValues,
            //        actionCall,
            //        new Action<string, string, string>(this.ThrowNewCreatedResultAssertionException));
            //});

            return this;
        }

        private void ThrowNewCreatedResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new CreatedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected created result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
