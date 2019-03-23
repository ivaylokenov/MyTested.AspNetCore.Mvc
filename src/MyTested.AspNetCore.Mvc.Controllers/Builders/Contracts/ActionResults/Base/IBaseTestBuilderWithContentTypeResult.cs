namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using Contracts.Base;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Base interface for all test builders with content type <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TContentTypeResultTestBuilder">Type of content type result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithContentTypeResult<TContentTypeResultTestBuilder> : IBaseTestBuilderWithActionResult
        where TContentTypeResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same content type <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TContentTypeResultTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> has the
        /// same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TContentTypeResultTestBuilder WithContentType(MediaTypeHeaderValue contentType);
    }
}
