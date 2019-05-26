namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.Net.Http.Headers;
    using System.Text;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
    /// extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderContentResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// with the same content as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="content">Expected content as string.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string content)
            => shouldReturnTestBuilder
                .Content(content, (MediaTypeHeaderValue)null);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// with the same content and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="content">Expected content as string.</param>
        /// <param name="contentType">Expected content type as string.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string content,
            string contentType)
            => shouldReturnTestBuilder
                .Content(content, MediaTypeHeaderValue.Parse(contentType));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// with the same content and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="content">Expected content as string.</param>
        /// <param name="contentType">Expected content type as string.</param>
        /// <param name="contentEncoding">Expected content encoding as <see cref="Encoding"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string content,
            string contentType,
            Encoding contentEncoding)
        {
            var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);
            mediaTypeHeaderValue.Encoding = contentEncoding ?? mediaTypeHeaderValue.Encoding;

            return shouldReturnTestBuilder.Content(content, mediaTypeHeaderValue);
        }

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>
        /// with the same content and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="content">Expected content as string.</param>
        /// <param name="contentType">Expected content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Content<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string content,
            MediaTypeHeaderValue contentType)
            => shouldReturnTestBuilder
                .Content(result => result
                    .WithContent(content)
                    .WithContentType(contentType));
    }
}
