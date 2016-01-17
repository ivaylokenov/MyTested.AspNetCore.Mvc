namespace MyTested.Mvc.Builders.ActionResults.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Contracts.ActionResults.View;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;
    using Utilities;

    public class ViewComponentTestBuilder
        : ViewTestBuilder<ViewComponentResult>, IAndViewComponentTestBuilder
    {
        private readonly object[] viewComponentArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="viewComponentResult">Result from the tested action.</param>
        public ViewComponentTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            ViewComponentResult viewComponentResult)
            : base(controller, actionName, caughtException, viewComponentResult, "view component result")
        {
            this.viewComponentArguments = this.GetArguments();
        }
        
        /// <summary>
        /// Tests whether view component result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithStatusCode(int statusCode)
        {
            return this.WithStatusCode((HttpStatusCode)statusCode);
        }

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
            return this.WithContentType(new MediaTypeHeaderValue(contentType));
        }

        /// <summary>
        /// Tests whether view component result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same view component test builder.</returns>
        public new IAndViewComponentTestBuilder WithContentType(MediaTypeHeaderValue contentType)
        {
            base.WithContentType(contentType);
            return this;
        }

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

        public IAndViewComponentTestBuilder WithArgument<TArgument>(TArgument argument)
        {
            var sameArgument = this.viewComponentArguments.FirstOrDefault(arg => Reflection.AreDeeplyEqual(argument, arg));

            if (sameArgument == null)
            {
                this.ThrowNewViewResultAssertionException(
                    "with at least one argument",
                    "to be the given one",
                    "none was found");
            }

            return this;
        }

        public IAndViewComponentTestBuilder WithArgumentOfType<TArgument>()
        {
            var expectedType = typeof(TArgument);
            var argumentOfSameType = this.viewComponentArguments.FirstOrDefault(arg => arg.GetType() == expectedType);

            if (argumentOfSameType == null)
            {
                this.ThrowNewViewResultAssertionException(
                    "with at least one argument",
                    $"to be of {expectedType} type",
                    "none was found");
            }

            return this;
        }

        public IAndViewComponentTestBuilder WithArguments(IEnumerable<object> arguments)
        {
            var argumentsList = arguments.ToList();

            var expectedArgumentsCount = argumentsList.Count;
            var actualArgumentsCount = this.viewComponentArguments.Length;

            if (expectedArgumentsCount != actualArgumentsCount)
            {
                this.ThrowNewViewResultAssertionException(
                    "Arguments",
                    $"to have {expectedArgumentsCount} items",
                    $"in fact found {actualArgumentsCount}");
            }

            argumentsList.ForEach(arg => this.WithArgument(arg));
            return this;
        }

        public IAndViewComponentTestBuilder WithArguments(params object[] arguments)
        {
            return this.WithArguments(arguments.AsEnumerable());
        }

        /// <summary>
        /// AndAlso method for better readability when chaining view component result tests.
        /// </summary>
        /// <returns>view component result test builder.</returns>
        public new IViewComponentTestBuilder AndAlso()
        {
            return this;
        }

        private object[] GetArguments()
        {
            var actionResultArguments = this.ActionResult.Arguments;
            var result = actionResultArguments as object[];

            if (result == null)
            {
                this.ThrowNewViewResultAssertionException(
                    "ViewEngine",
                    "to be of array of objects",
                    $"instead received {actionResultArguments.GetName()}");
            }

            return result;
        }
    }
}
