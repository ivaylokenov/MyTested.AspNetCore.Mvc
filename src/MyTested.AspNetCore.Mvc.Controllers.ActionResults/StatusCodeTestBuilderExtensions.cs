namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.StatusCode;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    /// <summary>
    /// Contains extension methods for <see cref="IStatusCodeTestBuilder"/>.
    /// </summary>
    public static class StatusCodeTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="ObjectResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="statusCodeTestBuilder">
        /// Instance of <see cref="IStatusCodeTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndStatusCodeTestBuilder"/>.</returns>
        public static IAndStatusCodeTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this IStatusCodeTestBuilder statusCodeTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => statusCodeTestBuilder
                .ContainingOutputFormatterOfType<IAndStatusCodeTestBuilder, TOutputFormatter>();
    }
}
