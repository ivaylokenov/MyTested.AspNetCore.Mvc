namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using System;
    using Contracts.Base;
    using Exceptions;
    using Utilities.Validators;
    using Microsoft.AspNetCore.Html;
    using System.IO;
    using System.Text;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithComponent HtmlContent()
        {
            InvocationResultValidator.ValidateInvocationResultType<IHtmlContent>(this.TestContext);
            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithComponent HtmlContent(string htmlContent)
        {
            var actualContent = this.GetHtmlContentAsString();

            if (htmlContent != actualContent)
            {
                throw ContentViewComponentResultAssertionException.ForEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    htmlContent,
                    actualContent);
            }

            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithComponent HtmlContent(Action<string> assertions)
        {
            var actualContent = this.GetHtmlContentAsString();

            assertions(actualContent);

            return this;
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithComponent HtmlContent(Func<string, bool> predicate)
        {
            var actualContent = this.GetHtmlContentAsString();

            if (!predicate(actualContent))
            {
                throw ContentViewComponentResultAssertionException.ForPredicate(
                    this.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return this;
        }

        private string GetHtmlContentAsString()
        {
            var htmlContentResult = InvocationResultValidator.GetInvocationResult<IHtmlContent>(this.TestContext);

            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                htmlContentResult.WriteTo(stringWriter, this.TestContext.ViewComponentContext.HtmlEncoder);
            }

            return stringBuilder.ToString();
        }
    }
}
