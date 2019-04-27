namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.Content;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.Content;
    using Builders.Contracts.Actions;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Contains <see cref="ContentResult"/>
    /// extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderContentResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="ContentResult"/> with expected content.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        public static IAndContentTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            string content)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            var contentResult = InvocationResultValidator.GetInvocationResult<ContentResult>(actualBuilder.TestContext);
            var actualContent = contentResult.Content;

            if (content != contentResult.Content)
            {
                throw ContentResultAssertionException.ForEquality(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    content,
                    actualContent);
            }

            return new ContentTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether <see cref="ContentResult"/> passes the given assertions.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="assertions">Action containing all assertions for the content.</param>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        public static IAndContentTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            Action<string> assertions)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            var contentResult = InvocationResultValidator.GetInvocationResult<ContentResult>(actualBuilder.TestContext);
            var actualContent = contentResult.Content;

            assertions(actualContent);

            return new ContentTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether <see cref="ContentResult"/> passes the given predicate.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>Test builder of <see cref="IAndContentTestBuilder"/> type.</returns>
        public static IAndContentTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder, 
            Func<string, bool> predicate)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            var contentResult = InvocationResultValidator.GetInvocationResult<ContentResult>(actualBuilder.TestContext);
            var actualContent = contentResult.Content;

            if (!predicate(actualContent))
            {
                throw ContentResultAssertionException.ForPredicate(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return new ContentTestBuilder(actualBuilder.TestContext);
        }
    }
}
