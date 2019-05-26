namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.Content;
    using Builders.Contracts.ActionResults.Content;
    using Exceptions;

    /// <summary>
    /// Contains extension methods for <see cref="IContentTestBuilder"/>.
    /// </summary>
    public static class ContentTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// has the same content as the provided one.
        /// </summary>
        /// <param name="contentTestBuilder">
        /// Instance of <see cref="IContentTestBuilder"/> type.
        /// </param>
        /// <param name="content">Expected content as string.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        public static IAndContentTestBuilder WithContent(
            this IContentTestBuilder contentTestBuilder,
            string content)
        {
            var actualBuilder = (ContentTestBuilder)contentTestBuilder;

            var actualContent = actualBuilder.ActionResult.Content;

            if (content != actualContent)
            {
                throw ContentResultAssertionException.ForEquality(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    content,
                    actualContent);
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// has content passing the given assertions.
        /// </summary>
        /// <param name="contentTestBuilder">
        /// Instance of <see cref="IContentTestBuilder"/> type.
        /// </param>
        /// <param name="assertions">Action containing all assertions for the content.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        public static IAndContentTestBuilder WithContent(
            this IContentTestBuilder contentTestBuilder,
            Action<string> assertions)
        {
            var actualBuilder = (ContentTestBuilder)contentTestBuilder;

            var actualContent = actualBuilder.ActionResult.Content;

            assertions(actualContent);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// has content passing the given predicate.
        /// </summary>
        /// <param name="contentTestBuilder">
        /// Instance of <see cref="IContentTestBuilder"/> type.
        /// </param>
        /// <param name="predicate">Predicate testing the content.</param>
        /// <returns>The same <see cref="IAndContentTestBuilder"/>.</returns>
        public static IAndContentTestBuilder WithContent(
            this IContentTestBuilder contentTestBuilder,
            Func<string, bool> predicate)
        {
            var actualBuilder = (ContentTestBuilder)contentTestBuilder;

            var actualContent = actualBuilder.ActionResult.Content;

            if (!predicate(actualContent))
            {
                throw ContentResultAssertionException.ForPredicate(
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    actualContent);
            }

            return actualBuilder;
        }
    }
}
