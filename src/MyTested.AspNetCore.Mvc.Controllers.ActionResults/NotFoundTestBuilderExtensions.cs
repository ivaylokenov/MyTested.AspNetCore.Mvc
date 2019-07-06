namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.NotFound;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    /// <summary>
    /// Contains extension methods for <see cref="INotFoundTestBuilder"/>.
    /// </summary>
    public static class NotFoundTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="NotFoundObjectResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="notFoundTestBuilder">
        /// Instance of <see cref="INotFoundTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndNotFoundTestBuilder"/>.</returns>
        public static IAndNotFoundTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this INotFoundTestBuilder notFoundTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => notFoundTestBuilder
                .ContainingOutputFormatterOfType<IAndNotFoundTestBuilder, TOutputFormatter>();
    }
}
