namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.View
{
    using System.Collections.Generic;
    using System.Net;
    using Contracts.ActionResults.View;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="ViewComponentResult"/>.
    /// </summary>
    public class ViewComponentTestBuilder
        : ViewTestBuilder<ViewComponentResult>, IAndViewComponentTestBuilder
    {
        private const string ArgumentsName = "arguments";

        private readonly IDictionary<string, object> viewComponentArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ViewComponentTestBuilder(ControllerTestContext testContext)
            : base(testContext, "view component")
        {
            // Uses internal reflection caching.
            this.viewComponentArguments = new RouteValueDictionary(this.ActionResult.Arguments);
        }

        /// <inheritdoc />
        public new IAndViewComponentTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <inheritdoc />
        public new IAndViewComponentTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            base.WithStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public new IAndViewComponentTestBuilder WithContentType(string contentType)
        {
            base.WithContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public new IAndViewComponentTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType.Value);
        
        /// <inheritdoc />
        public IAndViewComponentTestBuilder ContainingArgumentWithName(string name)
        {
            DictionaryValidator.ValidateStringKey(
                ArgumentsName,
                this.viewComponentArguments,
                name,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndViewComponentTestBuilder ContainingArgument<TArgument>(TArgument argument)
        {
            DictionaryValidator.ValidateValue(
                ArgumentsName,
                this.viewComponentArguments,
                argument,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>()
        {
            DictionaryValidator.ValidateValueOfType<TArgument>(
                ArgumentsName,
                this.viewComponentArguments,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndViewComponentTestBuilder ContainingArgumentOfType<TArgument>(string name)
        {
            DictionaryValidator.ValidateStringKeyAndValueOfType<TArgument>(
                ArgumentsName,
                this.viewComponentArguments,
                name,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndViewComponentTestBuilder ContainingArguments(object arguments)
            => this.ContainingArguments(new RouteValueDictionary(arguments));

        /// <inheritdoc />
        public IAndViewComponentTestBuilder ContainingArguments(IDictionary<string, object> arguments)
        {
            DictionaryValidator.ValidateValues(
                ArgumentsName,
                this.viewComponentArguments,
                arguments,
                this.ThrowNewViewResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public new IViewComponentTestBuilder AndAlso() => this;
    }
}
