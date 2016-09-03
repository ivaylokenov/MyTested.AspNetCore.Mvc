namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using System;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Utilities.Validators;
    using Exceptions;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content()
        {
            this.TransformContentResultToString();

            InvocationResultValidator.ValidateInvocationResultType<string>(this.TestContext);

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(string content)
        {
            this.TransformContentResultToString();

            var actualContent = InvocationResultValidator.GetInvocationResult<string>(this.TestContext);

            if (content != actualContent)
            {
                throw ContentViewComponentResultAssertionException.ForEquality(
                    this.TestContext.ExceptionMessagePrefix,
                    content,
                    actualContent);
            }

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(Action<string> assertions)
        {
            this.TransformContentResultToString();

            var actualContent = InvocationResultValidator.GetInvocationResult<string>(this.TestContext);

            assertions(actualContent);

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithViewComponentResult<TInvocationResult> Content(Func<string, bool> predicate)
        {
            this.TransformContentResultToString();

            var actualContent = InvocationResultValidator.GetInvocationResult<string>(this.TestContext);

            if (!predicate(actualContent))
            {
                throw ContentViewComponentResultAssertionException.ForPredicate(
                    this.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return this.NewAndTestBuilderWithViewComponentResult();
        }

        private void TransformContentResultToString()
        {
            if (this.TestContext.MethodResult is ContentViewComponentResult)
            {
                this.TestContext.MethodResult = ((ContentViewComponentResult)this.TestContext.MethodResult).Content;
            }
        }
    }
}
