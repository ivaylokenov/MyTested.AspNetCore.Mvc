namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Base interface for all test builders with output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TOutputResultTestBuilder">Type of output result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder>
        : IBaseTestBuilderWithStatusCodeResult<TOutputResultTestBuilder>
        where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingContentType(string contentType);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the content type provided as <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the same content types provided as collection of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of strings.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingContentTypes(params string[] contentTypes);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the same content types provided as collection of <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentTypes">Content types as collection of <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the same content types provided as <see cref="MediaTypeHeaderValue"/> parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as <see cref="MediaTypeHeaderValue"/> parameters.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided <see cref="IOutputFormatter"/>.
        /// </summary>
        /// <param name="outputFormatter">Instance of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of <see cref="IOutputFormatter"/>.</typeparam>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter;

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Collection of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters"><see cref="IOutputFormatter"/> parameters.</param>
        /// <returns>The same output <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TOutputResultTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters);
    }
}
