namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using System;
    using And;
    using Contracts.And;
    using Exceptions;
    using Utilities.Validators;
    using Microsoft.AspNetCore.Html;
    using System.IO;
    using System.Text;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder HtmlContent()
        {
            InvocationResultValidator
                .ValidateInvocationResultType<IHtmlContent>(this.TestContext, canBeAssignable: true);

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder HtmlContent(string htmlContent)
        {
            var actualContent = this.GetHtmlContentAsString();

            if (htmlContent != actualContent)
            {
                throw ContentViewComponentResultAssertionException.ForEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    htmlContent,
                    actualContent);
            }

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder HtmlContent(Action<string> assertions)
        {
            var actualContent = this.GetHtmlContentAsString();

            assertions(actualContent);

            return new AndTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndTestBuilder HtmlContent(Func<string, bool> predicate)
        {
            var actualContent = this.GetHtmlContentAsString();

            if (!predicate(actualContent))
            {
                throw ContentViewComponentResultAssertionException.ForPredicate(
                    this.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return new AndTestBuilder(this.TestContext);
        }

        private string GetHtmlContentAsString()
        {
            var htmlContentResult = InvocationResultValidator
                .GetInvocationResult<IHtmlContent>(this.TestContext, canBeAssignable: true);

            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                htmlContentResult.WriteTo(stringWriter, this.TestContext.ViewComponentContext.HtmlEncoder);
            }

            return stringBuilder.ToString();
        }
    }
}
