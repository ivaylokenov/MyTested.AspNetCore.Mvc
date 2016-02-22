namespace MyTested.Mvc.Builders.ActionResults.View
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Contracts.ActionResults.View;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Microsoft.AspNetCore.Routing;
    using Internal.TestContexts;
    using Utilities.Validators;
    /// <summary>
    /// Used for testing view component results.
    /// </summary>
    public class ViewComponentTestBuilder
        : ViewTestBuilder<ViewComponentResult>, IAndViewComponentTestBuilder
    {
        private const string ArgumentsName = "arguments";

        private readonly IDictionary<string, object> viewComponentArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="viewComponentResult">Result from the tested action.</param>
        public ViewComponentTestBuilder(ControllerTestContext testContext)
            : base(testContext, "view component")
        {
            // uses internal reflection caching
            this.viewComponentArguments = new RouteValueDictionary(this.ActionResult.Arguments);
        }

        /// <summary>
        /// Tests whether view component result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <summary>
        /// Tests whether view component result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            base.WithStatusCode(statusCode);
            return this;
        }

        /// <summary>
        /// Tests whether view component result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithContentType(string contentType)
        {
            base.WithContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether view component result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType);

        /// <summary>
        /// Tests whether view component result has the same view engine as the provided one.
        /// </summary>
        /// <param name="viewEngine">View engine of type IViewEngine.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithViewEngine(IViewEngine viewEngine)
        {
            base.WithViewEngine(viewEngine);
            return this;
        }

        /// <summary>
        /// Tests whether view component result has the same view engine type as the provided one.
        /// </summary>
        /// <typeparam name="TViewEngine">View engine of type IViewEngine.</typeparam>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithViewEngineOfType<TViewEngine>()
            where TViewEngine : IViewEngine
        {
            base.WithViewEngineOfType<TViewEngine>();
            return this;
        }

        /// <summary>
        /// Tests whether view component result will be invoked with an argument with the same name as the provided one.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Expected argument value.</param>
        /// <returns>The same view component test builder.</returns>
        public IAndViewComponentTestBuilder ContainingArgument(string name)
        {
            DictionaryValidator.ValidateStringKey(
                ArgumentsName,
                this.viewComponentArguments,
                name,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether view component result will be invoked with an argument deeply equal to the provided one.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Expected argument value.</param>
        /// <returns>The same view component test builder.</returns>
        public IAndViewComponentTestBuilder ContainingArgument(string name, object value)
        {
            DictionaryValidator.ValidateStringKeyAndValue(
                ArgumentsName,
                this.viewComponentArguments,
                name,
                value,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether view component result will be invoked with an argument equal to the provided one.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <param name="argument">Argument object.</param>
        /// <returns>The same view component test builder.</returns>
        public IAndViewComponentTestBuilder ContainingArgument<TArgument>(TArgument argument)
        {
            DictionaryValidator.ValidateValue(
                ArgumentsName,
                this.viewComponentArguments,
                argument,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether view component result will be invoked with an argument of the provided type.
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument.</typeparam>
        /// <returns>The same view component test builder.</returns>
        public IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>()
        {
            DictionaryValidator.ValidateValueOfType<TArgument>(
                ArgumentsName,
                this.viewComponentArguments,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        public IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>(string name)
        {
            DictionaryValidator.ValidateStringKeyAndValueOfType<TArgument>(
                ArgumentsName,
                this.viewComponentArguments,
                name,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether view component result will be invoked with the provided arguments.
        /// </summary>
        /// <param name="arguments">Arguments object.</param>
        /// <returns>The same view component test builder.</returns>
        public IAndViewComponentTestBuilder ContainingArguments(object arguments)
            => this.ContainingArguments(new RouteValueDictionary(arguments));

        /// <summary>
        /// Tests whether view component result will be invoked with the provided arguments.
        /// </summary>
        /// <param name="arguments">Argument objects as dictionary.</param>
        /// <returns>The same view component test builder.</returns>
        public IAndViewComponentTestBuilder ContainingArguments(IDictionary<string, object> arguments)
        {
            DictionaryValidator.ValidateValues(
                ArgumentsName,
                this.viewComponentArguments,
                arguments,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining view component result tests.
        /// </summary>
        /// <returns>The same view component test builder.</returns>
        public new IViewComponentTestBuilder AndAlso() => this;
    }
}
